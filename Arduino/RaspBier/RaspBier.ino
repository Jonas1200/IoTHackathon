//#define debug //Comment out if no debug
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>

const char* ssid = "Coredy E300";
const char* password = "<Insert Password here>";
const char* host = "192.168.10.118";
String url = "/SensorValue/InsertValue";
const int httpPort = 50000;

int sensorID = 2;


void setup() {
  
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

  int value = analogRead(0); //connect sensor to Analog 0    
  String strValue("sensorType=0&sensorId=");
  strValue += String(sensorID);
  strValue += String("&value=");
  strValue += String(value);
  
  #ifdef debug
    Serial.println(strValue);
    Serial.println(strValue.length());
  #endif
  
  WiFiClient client;
  Serial.print("connecting to ");
  Serial.println(host);
  if (!client.connect(host, httpPort)) {
    #ifdef debug
      Serial.println("connection failed");
    #endif
    return;
  }

  

  client.print(String("POST ") + url + " HTTP/1.1\r\n" +
               "Host: " + host + "\r\n" +
               "Content-Type: application/x-www-form-urlencoded\r\n" +               
               "Content-Length: " + strValue.length() + "\r\n" +
               "\r\n" +
               strValue
               );

  #ifdef debug
    Serial.println("request sent");
  #endif
  
  while (client.connected()) {
    String line = client.readStringUntil('\n');
    if (line == "\r") {
      #ifdef debug
        Serial.println("headers received");
      #endif
      break;
    }
  }
  #ifdef debug
    String line = client.readStringUntil('\n');
    Serial.println("reply was:");
    Serial.println("==========");
    Serial.println(line);
    Serial.println("==========");
    Serial.println("closing connection");
  #endif
}

void loop() {                
  //ESP.deepSleep(3600000000, WAKE_RF_DEFAULT); // Sleep for 60 Minutes
  delay(100);
}
