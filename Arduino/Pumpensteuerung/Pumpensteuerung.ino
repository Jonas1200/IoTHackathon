//#define debug //Comment out if no debug
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>

//Wifi Communication
const char* ssid = "Coredy E300";
const char* password = "<Insert Password here>";
const char* hashValue= "<Insert HASH here>";
const char* host = "192.168.10.118";
const int httpPort = 50000;

//Sensor Values
int sensorID = 1;
int inputHumidityPort = 2;
int outputPump = 5;
int outputLED = 0;




void postErrorMessage(int sensor, int errorType, String message){
  String url = "/Error/InsertError";
  WiFiClient client = connectWifi();
  String strValue("sensorId=");
  strValue += String(sensor);
  strValue += String("&errorType=");
  strValue += String(errorType);
  strValue += String("&message=");
  strValue += String(message);
  strValue += getHashString();
  
  
  #ifdef debug
    Serial.println(strValue);
    Serial.println(strValue.length());
  #endif
  
  client.print(String("POST ") + url + " HTTP/1.1\r\n" +
               "Host: " + host + "\r\n" +
               "Content-Type: application/x-www-form-urlencoded\r\n" +               
               "Content-Length: " + strValue.length() + "\r\n" +
               "\r\n" +
               strValue
               );
}


void postSensorValue(int sensorType, int sensor, int value){
  String url = "/SensorValue/InsertValue";
  WiFiClient client = connectWifi();
  String strValue("sensorType=");
  strValue += String(sensorType);
  strValue += String("&sensorId=");
  strValue += String(sensor);
  strValue += String("&value=");
  strValue += String(value);
  strValue += getHashString();
  
  #ifdef debug
    Serial.println(strValue);
    Serial.println(strValue.length());
  #endif
  
  client.print(String("POST ") + url + " HTTP/1.1\r\n" +
               "Host: " + host + "\r\n" +
               "Content-Type: application/x-www-form-urlencoded\r\n" +               
               "Content-Length: " + strValue.length() + "\r\n" +
               "\r\n" +
               strValue
               );
}

// the setup function runs once when you press reset or power the board
void setup() {
  // initialize digital pin LED_BUILTIN as an output.
  Serial.begin(115200);
  pinMode(inputHumidityPort, INPUT);
  pinMode(outputPump, OUTPUT);
  pinMode(outputLED, OUTPUT);
  connectWifi();
}

// the loop function runs over and over again forever
void loop() {
  int Humidity = digitalRead(inputHumidityPort);
  Serial.println(Humidity);
  if(Humidity == 1){
    bewaessern();
  }
  // print out the state of the button:

  delay(1000);        // delay in between reads for stability
}

void bewaessern(){
  digitalWrite(outputLED, LOW);
  digitalWrite(outputPump, HIGH);
  delay(11000);
  digitalWrite(outputLED, HIGH);
  digitalWrite(outputPump, LOW);
  postSensorValue(0,sensorID,HIGH);
  delay(15000);
  if(digitalRead(inputHumidityPort)){
    String errorMessage = String("Wasserstand niedrig. Bitte Wasser in Sensor " + String(sensorID) + " nachfüllen.");
    postErrorMessage(sensorID, 2, errorMessage);
    while(true){
      delay(1000);
    }
  }
}

WiFiClient connectWifi(){
  #ifdef debug
    Serial.begin(115200);
    Serial.println();
    Serial.print("connecting to ");
    Serial.println(ssid);
  #endif
  
  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    #ifdef debug
      Serial.print(".");
    #endif
  }
  #ifdef debug
    Serial.println("");
    Serial.println("WiFi connected");
    Serial.println("IP address: ");
    Serial.println(WiFi.localIP());
  #endif

  
  WiFiClient client;
  Serial.print("connecting to ");
  Serial.println(host);
  if (!client.connect(host, httpPort)) {
    #ifdef debug
      Serial.println("connection failed");
    #endif
    return client;
  }
}

String getHashString(){
    return String("&hash=" + hashValue);
}
