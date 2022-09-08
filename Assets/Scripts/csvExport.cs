using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;


public class csvExport : MonoBehaviour
{
    private string SavePath;
    private StreamWriter sw;
    // private DateTime dt;
    // private sw 
    private void Start()
    {
        SavePath = Application.persistentDataPath + "/saveData.csv";
        sw = new StreamWriter(SavePath, false);
        // ヘッダー出力
        string[] s1 = { "StartRec", DateTime.Now.ToString("hh:mm") };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        // -----記録終了・保存----- //
        // ヘッダー出力
        string[] s11 = { "StopRec", DateTime.Now.ToString("hh:mm") };
        string s22 = string.Join(",", s11);
        sw.WriteLine(s22);
        // StreamWriterを閉じる
        sw.Close();
    }

    private void Update()
    {
    }
}
