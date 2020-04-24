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
    public int curPage = 0;
    public int totalPage = 0;

    public void init()
    {
        reqNextPage();
    }

    private void handleRequest(bool isError, string data)
    {
        if (!isError)
        {
            var jsonData = JSON.Parse(data);
            var items = jsonData["list"];
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    //items[i]["index"] =i.ToString();
                    //创建item
                    CreateItem(items[i]);
                }
                //隐藏当前页
                showPage(curPage, false);
                curPage++;
                totalPage++;
            }
        }
    }

    private void CreateItem(JSONNode item)
    {
        string imgUrl = item["img1"];
        GameObject go = Instantiate(mPrefab, transform, false);
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

    public void pageDown()
    {
        if (curPage < totalPage)
        {
            //直接翻下一页
            showPage(curPage, false);//隐藏当前页
            showPage(++curPage, true);//显示下一页
        }
        else
        {
            //请求下一页
            reqNextPage();
        }
    }

    public void pageUp()
    {
        if (curPage > 1)
        {
            showPage(curPage, false);//隐藏当前页
            showPage(--curPage, true);//显示上一页
        }
    }


    private void reqNextPage()
    {
        string url = string.Format("{0}?cid={1}&len=8&p={2}", Global.PROGRAMLIST, transform.gameObject.name, curPage + 1);
        UnityNetworkManager.Instance.Get(url, handleRequest);
    }

    /// <summary>
    /// 设置页面显示/隐藏
    /// </summary>
    /// <param name="PageNo">要设置页面的页码</param>
    /// <param name="show">true:显示页面；false:隐藏页面</param>
    private void showPage(int PageNo, bool show)
    {
        foreach (Transform trans in transform)
        {
            float f = trans.GetSiblingIndex() / 8;
            if (Math.Floor(f) + 1 == PageNo)
            {
                trans.gameObject.SetActive(show);
            }
        }
    }
}
