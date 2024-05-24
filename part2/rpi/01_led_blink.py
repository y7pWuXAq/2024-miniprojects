# 01_led_blink.py
# Red led

import RPi.GPIO as GPIO
import time

# RGB LED PinNumber seetting
red_pin = 4
blue_pin = 5 
green_pin = 6

GPIO.setmode(GPIO.BCM) # GPIO.BOARD(1~40) 사용 가능하나 잘 사용 하지 않음
GPIO.setup(red_pin, GPIO.OUT) # 4번 Pin으로 출력
GPIO.setup(blue_pin, GPIO.OUT) # 5번 Pin으로 출력
GPIO.setup(green_pin, GPIO.OUT) # 6번 Pin으로 출력

try :
    while (True) :
        GPIO.output(red_pin, False)
        GPIO.output(blue_pin, False)
        GPIO.output(green_pin, False)
        time.sleep(1) # 1초가 기본 // RED!

        # GPIO.output(red_pin, False)
        # GPIO.output(blue_pin, False)
        # GPIO.output(green_pin, True)
        # time.sleep(0.5) # // GREEN!

        # GPIO.output(red_pin, False)
        # GPIO.output(blue_pin, True)
        # GPIO.output(green_pin, False)
        # time.sleep(0.5) # // BLUE!

        GPIO.output(red_pin, True)
        GPIO.output(blue_pin, True)
        GPIO.output(green_pin, True)
        time.sleep(1) # // WHITE
        
except KeyboardInterrupt:
    GPIO.cleanup()