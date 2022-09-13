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
    public Text recordCount;
    public GameObject cube;
    public Location location;
    public bool record;

    private Gyroscope gyroVal;
    private int cnt = 0;

    private SensingData data;
    private GpsData gpsData_tmp = new GpsData();
    // private int MAXVAL = 500;

    private void Awake()
    {
        Input.gyro.enabled = true;
        this.data = new SensingData();
        this.data.location = new List<GpsData>();
        this.data.sensorData = new List<SensorData>();
        // this.data.location = new GpsData[1];
        // this.data.sensorData = new SensorData[MAXVAL];
    }
    private void Update()
    {
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
        cube.transform.rotation = gyroVal.attitude;

        //センシングデータ
        SensorData sensorData_tmp = new SensorData();
        sensorData_tmp.accelerationVal = accelerationVal;
        // sensorData_tmp.gyroVal = new Vector3(gyroVal.rotationRate.x, gyroVal.rotationRate.y, gyroVal.rotationRate.z);
        sensorData_tmp.gyroVal = new Vector3(gyroVal.rotationRateUnbiased.x, gyroVal.rotationRateUnbiased.y, gyroVal.rotationRateUnbiased.z);

        accelerationValueX.text = accelerationVal.x.ToString();
        accelerationValueY.text = accelerationVal.y.ToString();
        accelerationValueZ.text = accelerationVal.z.ToString();

        gyroValueX.text = sensorData_tmp.gyroVal.x.ToString();
        gyroValueY.text = sensorData_tmp.gyroVal.y.ToString();
        gyroValueZ.text = sensorData_tmp.gyroVal.z.ToString();

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
            this.data.location.Add(this.gpsData_tmp);
            var json = JsonUtility.ToJson(this.data);
            string SavePath = Application.persistentDataPath + "/sensingData.json";
            File.WriteAllText(SavePath, json);
            this.record = false;
        }
    }
    public void startLog(){
        this.record = true;
    }
}
