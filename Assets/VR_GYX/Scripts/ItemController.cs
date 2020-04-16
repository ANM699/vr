using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public GameObject mPrefab;

    public void init()
    {
        string url = string.Format("{0}?cid={1}&len=8&p=1", Global.PROGRAMLIST, transform.gameObject.name);
        UnityNetworkManager.Instance.Get(url, handleRequest);
    }

    private void handleRequest(bool isError, string data)
    {
        if (!isError)
        {
            var jsonData = JSON.Parse(data);
            var items = jsonData["list"];
            for (int i = 0; i < items.Count; i++)
            {
                //items[i]["index"] =i.ToString();
                //创建item
                CreateItem(items[i]);
            }
        }
    }

    private void CreateItem(JSONNode item)
    {
        string imgUrl = item["img1"];
        GameObject go = Instantiate(mPrefab,  transform, false);
        go.name = item["vid"];
        Text txt = go.GetComponentInChildren<Text>();
        txt.text = item["title"];
        //请求并设置图片
        UnityNetworkManager.Instance.GetTexture(imgUrl, go, handleReqImage);
    }

    /// <summary>
    /// 处理请求图片的委托回调
    /// </summary>
    /// <param name="isError">请求是否出错</param>
    /// <param name="data">如果出错返回的错误信息，否则为null</param>
    /// <param name="texture">请求成功返回的结果</param>
    /// <param name="item">传入item其他信息</param>
    private void handleReqImage(bool isError, string data, Texture2D texture, GameObject go)
    {
        RawImage rimg = go.GetComponentInChildren<RawImage>();
        //txt.text = item["title"];
        if (texture != null && !isError)
        {
            rimg.texture = texture;
            //img.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
