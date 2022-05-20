using System;
using System.Text;
using BestHTTP;
using Newtonsoft.Json;
using UnityEngine;

public class HttpEx
{
    public Action<HTTPResponse> Response { get; set; }
    public HTTPRequest Request { get; }
    
    public HttpEx(string url, HTTPMethods methods = HTTPMethods.Get, bool isKeepAlive = false)
    {
        Request = new HTTPRequest(new Uri(url), methods, isKeepAlive, OnRequestFinished);
    }

    public void Send()
    {
        Request?.Send();
    }

    public void SendToJsonData<T>(T data)
    {
        if (Request == null) return;
        var jsonData = JsonConvert.SerializeObject(data);
        Request.RawData = Encoding.UTF8.GetBytes(jsonData);
        Request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        Request.Send();
    }
    
    private void OnRequestFinished(HTTPRequest originalRequest, HTTPResponse response)
    {
        switch (originalRequest.State)
        {
            // The request finished without any problem.
            case HTTPRequestStates.Finished:
                if (response.IsSuccess)
                {
                    Debug.Log("Request Success!");
                }
                else
                {
                    Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                        response.StatusCode,
                        response.Message,
                        response.DataAsText));
                }
                break;
            case HTTPRequestStates.Error:
                Debug.LogError("Request Finished with Error! " + (originalRequest.Exception != null ? (originalRequest.Exception.Message + "\n" + originalRequest.Exception.StackTrace) : "No Exception"));
                break;
            case HTTPRequestStates.Aborted:
                Debug.LogWarning("Request Aborted!");
                break;
            case HTTPRequestStates.ConnectionTimedOut:
                Debug.LogError("Connection Timed Out!");
                break;
            case HTTPRequestStates.TimedOut:
                Debug.LogError("Processing the request Timed Out!");
                break;
            case HTTPRequestStates.Initial:
                Debug.Log("Initial status of a request. No callback will be called with this status.");
                break;
            case HTTPRequestStates.Queued:
                Debug.Log("The request queued for processing.");
                break;
            case HTTPRequestStates.Processing:
                Debug.Log("Processing of the request started. In this state the client will send the request, and parse the response. No callback will be called with this status.");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        if (Response != null)
            Response(response);
    }
}
