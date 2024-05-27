using MahApps.Metro.Controls;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using SmartHomeMonitoringApp.Logics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartHomeMonitoringApp.Views
{
    /// <summary>
    /// DataBaseControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DataBaseControl : UserControl
    {
        // 변수 또는 속성 선언
        public Boolean IsConnected { get; set; }
        Thread MqttThread { get; set; } // 없으면 UI 컨트롤과 충돌이 나서 Log를 못찍음(응답없음 뜸!)
        int MaxCount { get; set; } = 10; // MQTT로그 과적으로인한 속도 저하 방지

        public DataBaseControl()
        {
            InitializeComponent();
        }

        // UserControl 화면 로드 된 이후 초기화
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TxtBrokerUrl.Text = Commons.BROKERHOST;
            TxtMqttTopic.Text = Commons.MQTTTOPIC;
            TxtConnString.Text = Commons.DBCONNSTRING;

            IsConnected = false;
            BtnConnect.IsChecked = false;
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            ConnectSystem(); // DB, MQTT 연결하는 메서드
        }

        private async void ConnectSystem()
        {
            if (IsConnected == false)
            {
                // 한번도 접속을 안했으면 모두연결
                var mqttFactory = new MqttFactory();
                Commons.MQTT_CLIENT = mqttFactory.CreateMqttClient();

                // MQTT Broker IP 연결 할 수 있는 속성
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(TxtBrokerUrl.Text).Build();

                await Commons.MQTT_CLIENT.ConnectAsync(mqttClientOptions, CancellationToken.None); // MQTT 연결
                Commons.MQTT_CLIENT.ApplicationMessageReceivedAsync += MQTT_CLIENT_ApplicationMessageReceivedAsync;

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder().WithTopicFilter(
                    f =>
                    {
                        f.WithTopic(Commons.MQTTTOPIC);
                    }).Build();

                await Commons.MQTT_CLIENT.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                IsConnected = true;
                BtnConnect.IsChecked = true;
                BtnConnect.Content = "MQTT 연결중";
            } else
            {
                // 연결 후 연결 끊기
                if (Commons.MQTT_CLIENT.IsConnected)
                {
                    Commons.MQTT_CLIENT.ApplicationMessageReceivedAsync -= MQTT_CLIENT_ApplicationMessageReceivedAsync;
                    await Commons.MQTT_CLIENT.DisconnectAsync();

                    IsConnected = false;
                    BtnConnect.IsChecked = false;
                    BtnConnect.Content = "Connect";
                }
            }
        }

        private Task MQTT_CLIENT_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            var payload = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);
            // Debug.WriteLine(payload);
            UpdateLog(payload); // TextBox에 추가
            InsertData(payload); // DB에 저장

            return Task.CompletedTask; // Async에서 Task값을 넘겨주려면 이 방법을 사용해야 함
        }

        private void InsertData(string payload)
        {
            this.Invoke(() =>
            {
                // 딕셔너리형태의 Json으로 변환되어서 데이터가 넘어옴
                var currValue = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
                // Debug.WriteLine("InsertData : " + currValue["CURR_DT"]);
                // currValue["DEV_ID"], currValue["TYPE"], currValue["VALUE"]
                if (currValue != null)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(TxtConnString.Text))
                        {
                            conn.Open();
                            var insQuery = @"INSERT INTO [dbo].[SmartHomeData]
                                                        ([DEV_ID]
                                                        ,[CURR_DT]
                                                        ,[TEMP]
                                                        ,[HUMID])
                                                  VALUES
                                                        (@DEV_ID
                                                        ,@CURR_DT
                                                        ,@TEMP
                                                        ,@HUMID)";

                            SqlCommand cmd = new SqlCommand(insQuery, conn);
                            cmd.Parameters.AddWithValue("@DEV_ID", currValue["DEV_ID"]);
                            cmd.Parameters.AddWithValue("@CURR_DT", currValue["CURR_DT"]); // string > DateTime 자동변환
                            //cmd.Parameters.AddWithValue("@TYPE", currValue["TYPE"]);

                            var splitValue = currValue["VALUE"].Split('|');
                            cmd.Parameters.AddWithValue("@TEMP", splitValue[0]); // splitValue[0] = 온도(Temp)
                            cmd.Parameters.AddWithValue("@HUMID", splitValue[1]); // splitValue[1] = 습도(Humid)

                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                UpdateLog(" >> DB INSERT SUCCEED.");
                            }
                            else
                            {
                                UpdateLog(" >> DB INSERT fAILED.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        UpdateLog($"DB ERROR!! : {ex.Message}");
                    }
                }
            });
        }

        private void UpdateLog(string payload)
        {
            this.Invoke(() =>
            {
                TxtLog.Text += $"{payload}\n";
                TxtLog.ScrollToEnd(); // 스크롤이 생기기 시작하면 제일 아래로 포커스
            });
        }
    }
}