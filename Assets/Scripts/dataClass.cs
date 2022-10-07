using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//データクラス
[Serializable]
public class SensingData{
    public List<GpsData> location;
    public List<SensorData> sensorData;
}
[Serializable]
public class GpsData{
    public float latitude;
    public float longitude;
    public float altitude;
}
[Serializable]
public class SensorData{
    public float     secTime;
    public Vector3  accelerationVal;
    public Vector3  gyroVal;
    public Vector3  geomagneticVal;
    public Vector4  gyroQuaternion;
    public Vector3  accelRotation;
    public Vector4  accelQuaternion;
    public float    compasVal;
}
//FPS設定するhttp://unityleaning.blog.fc2.com/blog-entry-2.html
