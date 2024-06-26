## 2024-miniprojects
IoT 개발자과정 미니 프로젝트 리포지토리


### DAY 01

- 조별 자리배치
- IoT 프로젝트 개요

    ![IoT프로젝트](https://raw.githubusercontent.com/y7pWuXAq/2024-miniprojects/main/images/mp001.png)

    - 1. IoT기기 구성 : 아두이노, 라즈베리파이 etc... IoT 장비들과 연결
    - 2. 서버구성 : IoT기기와 통신, DB 구성, 데이터 수집 앱 개발
    - 3. 모니터링 구성 : 실시간 모니터링, 제어앱

- 조별 미니프로젝트
    - 5월 28일까지 (40시간)
    - 구체적으로 어떤 디바이스 구성, 데이터 수집, 모니터링 계획
    - 8월말 정도에 끝나는프로젝트 일정 계획
    - **요구사항 리스트, 기능명세, UI/UX 디자인, DB설계 문서 하나**로 통합
    - 8월 28일 오후 각 조별로 프레젠테이션 준비 조별로 10분 내외 발표
    - 요구사항 리스트 문서전달
    - 기능명세 문서
    - DB설계 ERD 또는 SSMS 물리적 DB설계
    - UX/UI 디자인 16일(목) 내용 전달


### DAY 02

- 미니프로젝트
    - 프로젝트 문서
    - Notion 팀 프로젝트 템플릿 활용

    - UI/UX 디자인 툴 설명
        - https://ovenapp.io/ 카카오
        - https://www.figma.com/ 피그마
        - https://app.moqups.com/ 목업 디자인 사이트

    - 프리젠테이션
        - https://www.miricanvas.com/ko 미리캔버스 활용 추천

    - 라즈베리파이(RPi) 리셋팅, 네트워크 설정, VNC(OS UI 작업)

- 사마트홈 연동 클래스 미니프로젝트
    - 1. 요구사항 정의, 기능명세, 일정정리
    - 2. UI/UX 디자인
    - 3. DB설계
    - 4. RPi 세팅(Network)
    - 5. RPi GPIO에 IoT 디바이스 연결(카메라, HDT(온습도센서), RGB LED ...)
    - 6. RPi 데이터를 전송하는 파이썬 프로그래밍
    - 7. PC(Server)에서 데이터를 수신하는 C# 프로그래밍
    - 8. 모니터링 앱 개발(수신 및 송신)


### DAY 03

- 미니프로젝트
    - 실무 프로젝트 문서
    - 조별로 진행

- 라즈베리파이 세팅
    - RPi 기본 구성 : RPi + MicroSD + Power
    - RPi 기본 세팅
        - [x] 최신 업그레이드
        - [x] 한글화
        - [x] 키보드 변경
        - [x] 화면사이즈 변경(RealVNC)
        - [x] Pi Apps 앱설치 도우미 앱
        - [x] Github Desktop, VS Code
        - [x] 네트워크 확인
        - [ ] RealVNC Server 자동실행 설정

- 스마트홈 연동 클래스 미니프로젝트
    - RPi 세팅 진행


### DAY 04

- 라즈베리파이 IoT장비 설치
    - [x] 라즈베리파이 카메라 연결
        - sudo libcamera-hello -t 0 : 카메라 테스트!
    - [x] GPIO HAT
    - [x] 브레드보드와 연결
    - [ ] DHT11 센서 연결
    - [x] RGB LED 모듈 연결
        - V : 5V 연결
        - R : GPI04 연결
        - B : GPI05 연결  
        - G : GPI06 연결
    - 서브모터 연결


### DAY 05

- 라즈베리파이 IoT장비 설치
    - [x] DHT11 센서
        - GND : GND 센서 8개중 아무곳이나 연결
        - VCC : 5V 연결
        - S :  GPIO18 연결

- 미니 프로젝트
    - 팀별 구매목록 작성
    - 프로젝트 결정사항 공유
    - 발표자료 준비


### DAY 06

- [x] 네트워크 대공사!
    - 개인공유기 연결, PC, 라즈베리파이 네트워크 재연결

- 스마트홈 연동 클래스 미니프로젝트
    - 온습도 센서, RGB LED
    - RPi <-> Windows 통신(MQTT)
    - WPF 모니터링 앱

- IoT 기기간 통신방법
    - Modbus : 시리얼통신으로 데이터 전송 (구식!)
    - OPC UA : Modbus 통신 방법의 불편한 점을 개선한 방식(복잡함!)
    - MQTT : 가장 편리! AWS IoT, Azure IoT 클라우드 산업계 표준 방식으로 사용

- MQTT 통신
    - [x] Mosquitto Broker 설치
        - mosquitto.conf 편집 : listener 1883 0.0.0.0, allow_anonymous true
        - 방화벽 인바운드 열기
    - [x] RPi : paho-mqtt 패키지 설치, 송신(publisher)
    - [ ] Windows/C# : M2MQTT, MQTT.NET Nuget 패키지 설치, 수신(subcriber)
        - M2MQTT : 가볍게 사용 가능, 업데이트 안됨
        - MQTT.NET : MS에서 제작, 무거움


### DAY 09

- 스마트홈 연동 클래스 미니프로젝트
    - [x] WPF MQTT데이터 DB로 저장
    - [x] MQTT데이터 실시간 모니터링
    - [ ] MQTT로 RPi 제어(LED제어)
    - [ ] WPF MQTT데이터 히스토리 확인


### DAY 10

- 스마트홈 연동 클래스 미니프로젝트 마무리
    - [x] WPF MQTT데이터 DB로 저장
    - [x] MQTT데이터 실시간 모니터링
    - [x] MQTT로 RPi 제어(LED제어)
    - [x] WPF MQTT데이터 히스토리 확인
        - LiveChart2는 차후에 다시, 현재 OxyPlot 차트로 대체

    - 실행결과

        - DB 모니터링

        ![DB 모니터링 화면](https://raw.githubusercontent.com/y7pWuXAq/2024-miniprojects/main/images/mp002.png)


        - 실시간 모니터링

        ![실시간 모니터링 화면](https://raw.githubusercontent.com/y7pWuXAq/2024-miniprojects/main/images/mp003.png)

        - 시각화 모니터링
            
        ![시각화 모니터링 화면](https://raw.githubusercontent.com/y7pWuXAq/2024-miniprojects/main/images/mp004.png)


- 조별 미니프로젝트 발표
