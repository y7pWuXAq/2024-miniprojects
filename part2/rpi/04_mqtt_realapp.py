# 04_mqtt_realapp.py
# 온습도센서데이터 통신, RGB LED Setting
# MQTT -> json transfer

import paho.mqtt.client as mqtt
import RPi.GPIO as GPIO
import adafruit_dht
import board
import time
import datetime as dt
import json

red_pin = 4
green_pin = 6
dht_pin = 18

dev_id = 'PKNU79'
loop_num = 0


# 초기화 시작
def onConnect(client, userdata, flags, reason_code, properties) :
    print(f'연결성공 : {reason_code}')
    client.subscribe('pknu/rcv/')
    
def onMessage(client, userdata, msg) :
    print(f'{msg.topic} + {msg.payload}')

GPIO.cleanup()
GPIO.setmode(GPIO.BCM)
GPIO.setup(red_pin, GPIO.OUT)
GPIO.setup(green_pin, GPIO.OUT) ## LED를 켜는 것
GPIO.setup(dht_pin, GPIO.IN) ## 온습도 값을 RPi에서 받는 것
dhtDevice = adafruit_dht.DHT11(board.D18) # semsor_pin 변수 사용 안됨!! 주의!!
## 초기화 끝


## 실행 시작
mqttc = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2) # 2023.9 이후 버전업
mqttc.on_connect = onConnect # mqtt 접속시 이벤트
mqttc.on_message = onMessage # 메세지 전송시 이벤트

# 192.168.5.2 본인 윈도우 IP
mqttc.connect('192.168.5.2', 1883, 60)

mqttc.loop_start()
while True :
    time.sleep(2) # DH11 2초 간격일 때 데이터 갱신이 잘 됨
    
    try :
        # 온습도 값을 받아서 MQTT로 전송
        temp = dhtDevice.temperature
        humd = dhtDevice.humidity
        print(f'{loop_num} - temp : {temp} / humid : {humd}')

        origin_data = { 'DEV_ID' : dev_id,
                        'CURR_DT' : dt.datetime.now().strftime('%Y-%m-%d %H:%M:%S'), #년월일 시분초
                        'TYPE' : 'TEMPHUMID',
                        'VALUE' : f'{temp} | {humd}'
                      } # dictionary data
        pup_data = json.dumps(origin_data, ensure_ascii=False)

        mqttc.publish('pknu/data/', pup_data)
        loop_num += 1

    except RuntimeError as ex :
        print(ex.args[0])
        
    except KeyboardInterrupt :
        break

mqttc.loop_stop()
dhtDevice.exit()
## 실행 끝