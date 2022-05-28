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
        ApiCallPostState();
        ApiCallGetState();
        ApiCallPutState();
        ApiCallDeleteState();
    }

    private void ApiCallPutState()
    {
        var http = new HttpEx("http://127.0.0.1:5000/api/state", HTTPMethods.Put);
        var data = new Dictionary<string, object>
        {
            {"idt_state", "1"},
            {"date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
            {"state", 4}
        };
        http.SendToJsonData(data);
    }

    private void ApiCallDeleteState()
    {
        var http = new HttpEx("http://127.0.0.1:5000/api/state", HTTPMethods.Delete);
        var data = new Dictionary<string, object>
        {
            {"idt_state", "1"},
        };
        http.SendToJsonData(data); 
    }

    private void ApiCallGetState()
    {
        var http = new HttpEx("http://127.0.0.1:5000/api/state");
        http.Response += (s) => { Debug.Log($"Controller Message {s.DataAsText}");};
        http.Send();
    }

    private void ApiCallPostState()
    {
        var http = new HttpEx("http://127.0.0.1:5000/api/state", HTTPMethods.Post);
        var data = new Dictionary<string, object>
        {
            {"idt_state", "1"},
            {"date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
            {"state", 4}
        };
        http.SendToJsonData(data);
    }
}
