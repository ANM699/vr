using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    //private Scene videoScene;
    public GameObject mPrefab;
    private static readonly string URL = "https://bestvapi.bestv.cn/smgbb/api/home";
    private Sprite sprite;

    void Awake()
    {
        UnityNetworkManager.Instance.Get(URL, handleRequest);
        //GameObject[] gos = GameObject.FindGameObjectsWithTag("item");
        //foreach (GameObject go in gos)
        //{
        //    HVREventListener.Get(go).onEnter = onPointEnter;
        //    HVREventListener.Get(go).onExit = onPointExit;
        //    HVREventListener.Get(go).onClick = onPointerClick;
        //}
    }

    private void handleRequest(bool isError, string data)
    {
        if (!isError)
        {
            var jsonData = JSON.Parse(data);
            var items = jsonData["block"][0]["items"];
            for (int i = 0; i < items.Count; i++)
            {
                items[i]["index"] =i.ToString();
                //创建item
                CreateItem(items[i]);
            }
        }
    }

    private void CreateItem(JSONNode item)
    {
        string imgUrl = item["img"];
        //请求并设置图片
        UnityNetworkManager.Instance.GetTexture(imgUrl, item, handleReqImage);
    }

    /// <summary>
    /// 处理请求图片的委托回调
    /// </summary>
    /// <param name="isError">请求是否出错</param>
    /// <param name="data">如果出错返回的错误信息，否则为null</param>
    /// <param name="texture">请求成功返回的结果</param>
    /// <param name="item">传入item其他信息</param>
    private void handleReqImage(bool isError, string data, Texture2D texture, JSONNode item)
    {
        GameObject go = Instantiate(mPrefab, transform, false);
        go.name = "item" + item["index"];
        HVREventListener.Get(go).onEnter = onPointEnter;
        HVREventListener.Get(go).onExit = onPointExit;
        HVREventListener.Get(go).onClick = onPointerClick;
        Text txt = go.GetComponentInChildren<Text>();
        //Image img = go.GetComponentInChildren<Image>();
        RawImage rimg = go.GetComponentInChildren<RawImage>();
        txt.text = item["title"];
        if (texture != null && !isError)
        {
            
            rimg.texture = texture;
            //img.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }

    private void onPointEnter(GameObject go)
    {
        iTween.ScaleTo(go, iTween.Hash(
              "scale", new Vector3(1.05f, 1.05f, 1.05f),
            "loopType", iTween.LoopType.none
        ));
    }

    private void onPointExit(GameObject go)
    {
        iTween.ScaleTo(go, iTween.Hash(
            "scale", new Vector3(1f, 1f, 1f),
            "loopType", iTween.LoopType.none
        ));
    }

    private void onPointerClick(GameObject go)
    {
        SceneManager.LoadScene("VideoScene");
    }
}
