using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP;
using Newtonsoft.Json;
using UnityEngine;

public class HttpController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var http = new HttpEx("http://127.0.0.1:5000/api/state", HTTPMethods.Put);
        var data = new Dictionary<string, object>
        {
            {"idt_state", "1"},
            {"date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
            {"state", 4}
        };
        http.Response += (s) => { Debug.Log($"Controller Message {s.DataAsText}");};
        http.SendToJsonData(data);
    }
}
