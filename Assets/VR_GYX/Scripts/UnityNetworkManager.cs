using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnityNetworkManager : MonoBehaviour
{

    private static UnityNetworkManager instance;

    public static UnityNetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject NetworkManager = new GameObject("UnityNetworkManager");
                instance = NetworkManager.AddComponent<UnityNetworkManager>();
            }
            return instance;
        }
    }

    /// <summary>
    /// GET请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="action">请求发起后处理回调结果的委托</param>
    public void Get(string url, Action<bool, string> action)
    {
        StartCoroutine(_Get(url, action));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url">图片地址</param>
    /// <param name="action">请求发起后处理回调结果的委托，处理请求结果的图片</param>
    public void GetTexture(string url, Action<bool, string, Texture2D> action)
    {
        StartCoroutine(_GetTexture(url, action));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url">图片地址</param>
    /// <param name="t">传入的其他额数据</param>
    /// <param name="action">请求发起后处理回调结果的委托，处理请求结果的图片，并设置到传入的GameObject上</param>
    public void GetTexture<T>(string url, T t, Action<bool, string, Texture2D, T> action)
    {
        StartCoroutine(_GetTexture(url, t, action));
    }


    /// <summary>
    /// POST请求
    /// </summary>
    /// <param name="serverURL">服务器请求目标地址</param>
    /// <param name="lstformData">form表单参数</param>
    /// <param name="action">处理返回结果的委托，处理请求对象</param>
    public void Post(string serverURL, List<IMultipartFormSection> lstformData, Action<bool, string> action)
    {
        StartCoroutine(_Post(serverURL, lstformData, action));
    }

    IEnumerator _Get(string url, Action<bool, string> action)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        {
            yield return uwr.SendWebRequest();
            string text = "";
            if (!(uwr.isNetworkError || uwr.isHttpError))
            {
                text = uwr.downloadHandler.text;
            }
            else
            {
                text = uwr.error;
            }
            if (action != null)
            {
                action.Invoke(uwr.isHttpError, text);
            }
        }
    }



    IEnumerator _GetTexture(string url, Action<bool, string, Texture2D> action)
    {
        using (UnityWebRequest uwr = new UnityWebRequest(url))
        {
            DownloadHandlerTexture downloadTexture = new DownloadHandlerTexture(true);
            uwr.downloadHandler = downloadTexture;
            yield return uwr.SendWebRequest();
            Texture2D texture = null;
            string text = null;
            if (!(uwr.isNetworkError || uwr.isHttpError))
            {
                texture = downloadTexture.texture;
            }
            else
            {
                text = uwr.error;
            }
            if (action != null)
            {
                action.Invoke((uwr.isNetworkError || uwr.isHttpError), text, texture);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name=t">传入的其他额外信息</param>
    /// <param name="action"></param>
    /// <returns></returns>
    IEnumerator _GetTexture<T>(string url, T t, Action<bool, string, Texture2D, T> action)
    {
        using (UnityWebRequest uwr = new UnityWebRequest(url))
        {
            DownloadHandlerTexture downloadTexture = new DownloadHandlerTexture(true);
            uwr.downloadHandler = downloadTexture;
            yield return uwr.SendWebRequest();
            Texture2D texture = null;
            string text = null;
            if (!(uwr.isNetworkError || uwr.isHttpError))
            {
                texture = downloadTexture.texture;
            }
            else
            {
                text = uwr.error;
            }
            if (action != null)
            {
                action.Invoke((uwr.isNetworkError || uwr.isHttpError), text, texture, t);
            }
        }
    }

    IEnumerator _Post(string url, List<IMultipartFormSection> lstformData, Action<bool, string> action)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Post(url, lstformData))
        {
            yield return uwr.SendWebRequest();
            string text = "";
            if (!(uwr.isNetworkError || uwr.isHttpError))
            {
                text = uwr.downloadHandler.text;
            }
            else
            {
                text = uwr.error;
            }
            if (action != null)
            {
                action.Invoke(uwr.isHttpError, text);
            }
        }
    }
}
