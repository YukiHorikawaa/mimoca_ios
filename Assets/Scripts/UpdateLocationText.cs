using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLocationText : MonoBehaviour
{
    public Text GPSStatus;
    public Text latitudeValue;
    public Text longitudeValue;
    public Text altitudeValue;
    public Text horizontalAccuracyValue;
    public Text timeStampValue;
    public Text location;
    public Text accelerationValueX;
    public Text accelerationValueY;
    public Text accelerationValueZ;
    public Text gyroValueX;
    public Text gyroValueY;
    public Text gyroValueZ;
    public GameObject cube;
    private Gyroscope gyroVal;
    private void Awake()
    {
        Input.gyro.enabled = true;
    }
    private void Update()
    {
        // location.text = $"緯度: {Location.Instance.latitude}\n経度: {Location.Instance.longitude}\n高度: {Location.Instance.altitude}\n\nCount: {Location.Instance.gps_count}\nMessage:\n{Location.Instance.message}";
    
        GPSStatus.text = Location.Instance.message.ToString(); 
        latitudeValue.text = Location.Instance.latitude.ToString();
        longitudeValue.text = Location.Instance.longitude.ToString();
        altitudeValue.text = Location.Instance.altitude.ToString();
        // horizontalAccuracyValue.text = Location.Instance.horizontalAccuracy.ToString();
        timeStampValue.text = Location.Instance.gps_count.ToString();

        // 加速度センサの値を取得
        Vector3 accelerationVal = Input.acceleration;
        gyroVal = Input.gyro;
        cube.transform.rotation = gyroVal.attitude;
        //x,y,zそれぞれの値を出力する
        Debug.Log("x:" + accelerationVal.x + "y:" + accelerationVal.y + "z:" + accelerationVal.z);

        accelerationValueX.text = accelerationVal.x.ToString();
        accelerationValueY.text = accelerationVal.y.ToString();
        accelerationValueZ.text = accelerationVal.z.ToString();
        // gyroValueX.text = gyroVal.x.ToString();
        // gyroValueY.text = gyroVal.y.ToString();
        // gyroValueZ.text = gyroVal.z.ToString();
    }
}
