﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MQTTnet.Client;
using System.Threading.Tasks;
using System.Windows;

namespace SmartHomeMonitoringApp.Logics
{
    public class Commons
    {
        // Window에 MQTT Broker가 설치되어 있으므로 가능한 아이피, 호스트
        // >> localhost, 127.0.0.1, cmd > ipconfig > 본인 IP 주소
        public static string BROKERHOST { get; set; } = "192.168.5.2"; // Window Mqtt Broker Mosquitto IP
        public static string MQTTTOPIC { get; set; } = "pknu/data/"; // 대소문자 구분, 마지막 '/'도 중요!
        public static string DBCONNSTRING { get; set; } = "Data Source=127.0.0.1;" +
                                                          "Initial Catalog=EMS;" +
                                                          "Persist Security Info=True;" +
                                                          "User ID=sa;" +
                                                          "Encrypt=False;" +
                                                          "Password=mssql_p@ss";
        public static IMqttClient MQTT_CLIENT { get; set; } // 전체 프로젝트에서 다 사용할 공용 Mqtt 클라이언트
    }
}
