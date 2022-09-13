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
    public Vector3 accelerationVal;
    public Vector3 gyroVal;
}
