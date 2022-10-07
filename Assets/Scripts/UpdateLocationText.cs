using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using SensingData;

public class UpdateLocationText : MonoBehaviour
{
    public Text GPSStatus;
    public Text latitudeValue;
    public Text longitudeValue;
    public Text altitudeValue;
    public Text horizontalAccuracyValue;
    public Text timeStampValue;
    public Text accelerationValueX;
    public Text accelerationValueY;
    public Text accelerationValueZ;
    public Text gyroValueX;
    public Text gyroValueY;
    public Text gyroValueZ;
    public Text compasValue;
    public Text recordCount;
    public GameObject gyroCube;
    public GameObject accelCube;
    public Location location;

    public string fileName = "sensingData";
    private string timeStamp;
    private static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    // private long startTime  = 0;
    private float time  = 0;
    private float timeOffset  = 0;

    public bool record;

    private Gyroscope gyroVal;
    private int cnt = 0;

    private SensingData data;
    private GpsData gpsData_tmp = new GpsData();
    // private int MAXVAL = 500;

    private void Awake()
    {
        Input.gyro.enabled = true;
        Input.compass.enabled = true;
        this.data = new SensingData();
        this.data.location = new List<GpsData>();
        this.data.sensorData = new List<SensorData>();
        this.timeOffset = Time.deltaTime;
    
        //起動時のUnix時間取得
        // this.startTime  = GetUnixTime(DateTime.Now);
    }
    private void Update()
    {
        this.time += Time.deltaTime - this.timeOffset;
        // location.text = $"緯度: {Location.Instance.latitude}\n経度: {Location.Instance.longitude}\n高度: {Location.Instance.altitude}\n\nCount: {Location.Instance.gps_count}\nMessage:\n{Location.Instance.message}";
        GPSStatus.text = Location.Instance.message.ToString();  
        timeStampValue.text = Location.Instance.gps_count.ToString();
        //GPS Json用
        this.gpsData_tmp.latitude = Location.Instance.latitude;
        latitudeValue.text = this.gpsData_tmp.latitude.ToString();

        this.gpsData_tmp.longitude = Location.Instance.longitude;
        longitudeValue.text = this.gpsData_tmp.longitude.ToString();

        this.gpsData_tmp.altitude = Location.Instance.altitude;
        altitudeValue.text = this.gpsData_tmp.altitude.ToString();


        // 加速度センサの値を取得
        Vector3 accelerationVal = Input.acceleration;
        gyroVal = Input.gyro;
        //gyroから求める角度
        gyroCube.transform.rotation = gyroVal.attitude;
        //accelから求める角度
        Vector3 newAccelRotation = calcurateRotation(accelerationVal);
        Quaternion newQuaternion = Quaternion.Euler(newAccelRotation);
        accelCube.transform.rotation = Quaternion.Euler(newAccelRotation);

        //センシングデータ
        SensorData sensorData_tmp = new SensorData();
        sensorData_tmp.accelerationVal = accelerationVal;
        // sensorData_tmp.gyroVal = new Vector3(gyroVal.rotationRate.x, gyroVal.rotationRate.y, gyroVal.rotationRate.z);
        sensorData_tmp.gyroVal = new Vector3(gyroVal.rotationRateUnbiased.x, gyroVal.rotationRateUnbiased.y, gyroVal.rotationRateUnbiased.z);
        sensorData_tmp.gyroQuaternion = getQuaternionParam(gyroVal.attitude);
        sensorData_tmp.accelQuaternion = getQuaternionParam(newQuaternion);
        sensorData_tmp.accelRotation = newAccelRotation;

        accelerationValueX.text = accelerationVal.x.ToString();
        accelerationValueY.text = accelerationVal.y.ToString();
        accelerationValueZ.text = accelerationVal.z.ToString();

        gyroValueX.text = sensorData_tmp.gyroVal.x.ToString();
        gyroValueY.text = sensorData_tmp.gyroVal.y.ToString();
        gyroValueZ.text = sensorData_tmp.gyroVal.z.ToString();

        sensorData_tmp.compasVal = Input.compass.magneticHeading;
        sensorData_tmp.geomagneticVal = Input.compass.rawVector;
        compasValue.text = sensorData_tmp.compasVal.ToString();

        //時間(s)格納
        sensorData_tmp.secTime = time;

        Debug.Log(sensorData_tmp.gyroVal.x);
        //書き出し用Json作成
        if(this.record){
            cnt++;
            this.recordCount.text = cnt.ToString();
            this.data.sensorData.Add(sensorData_tmp);
            // this.data.location[0] = this.gpsData_tmp;
            // this.data.sensorData[cnt] = sensorData_tmp;
        }
    }

    public void outputData(){
        if(this.record){
            //Locationは一点だけでOK
            // string timeStamp = date.ToString("yyyy-MM-dd-HH-mm-ss");
            string outFileName = "/" + this.fileName + "_" + this.timeStamp + ".json" ;
            this.data.location.Add(this.gpsData_tmp);
            var json = JsonUtility.ToJson(this.data);
            string SavePath = Application.persistentDataPath + outFileName;
            File.WriteAllText(SavePath, json);
            this.record = false;
        }
    }
    public void startLog(){
        this.record = true;
        this.timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
    }
    // DateTimeからUnixTimeへ変換
    private long GetUnixTime(DateTime dateTime)
    {
        return (long)(dateTime - UnixEpoch).TotalSeconds;
    }

    private Vector3 calcurateRotation(Vector3 accel){
        Vector3 newRotation = new Vector3(
            Mathf.Atan2(accel.x, accel.z) / Mathf.PI * 180,
            0, 
            Mathf.Atan2(accel.y, accel.z) / Mathf.PI * 180
        );
        return newRotation;
    }
    private Vector4 getQuaternionParam(Quaternion quaternion){
        Vector4 newRotation = new Vector4(
            quaternion.x,
            quaternion.y, 
            quaternion.z,
            quaternion.w
        );
        return newRotation;
    }
}
