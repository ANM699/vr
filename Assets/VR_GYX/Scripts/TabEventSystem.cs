using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabEventSystem : MonoBehaviour
{

    public GameObject[] tabs;
    public GameObject itemPanel;//对应的itemPanel
    public GameObject[] itemPanels;
    void Awake()
    {
        HVREventListener.Get(transform.gameObject).onClick = onPointerClick;
    }

    public void onPointerClick(GameObject go)
    {
        animate(go);
        ItemController itemController = itemPanel.GetComponent<ItemController>();
        Global.curItemController = itemController;
        if (itemPanel.transform.childCount == 0)
        {
            //对应的itemPanel加载第一页内容          
            itemController.init();
        }
        foreach (GameObject itemPanel in itemPanels)
        {
            bool b = this.itemPanel == itemPanel ? true : false;
            itemPanel.SetActive(b);       
        } 
    }

    private void handleReqList(bool isError, string data)
    {
        if (!isError)
        {
            var jsonData = JSON.Parse(data);

        }
    }

    private void onPointEnter(GameObject go)
    {
        iTween.ScaleTo(go, iTween.Hash(
              "scale", new Vector3(1.1f, 1.1f, 1.1f),
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

    //选择tab时执行的动画
    private void animate(GameObject selectedTab)
    {
        Color selectedColor = new Color(0.8f, 0.077f, 0.147f, 0.7f);
        Color normalColor = new Color(0.0f, 0.0f, 0.0f, 0.7f);

        foreach (GameObject tab in tabs)
        {
            if (selectedTab.Equals(tab))
            {
                HVREventListener.Get(tab).onEnter = null;
                HVREventListener.Get(tab).onExit = null;

                iTween.ScaleTo(tab, iTween.Hash(
                    "scale", new Vector3(1.2f, 1.2f, 1.2f),
                    "loopType", iTween.LoopType.none
                    ));
                iTween.ColorTo(tab, selectedColor, 0.03f);
            }
            else
            {
                HVREventListener.Get(tab).onEnter = onPointEnter;
                HVREventListener.Get(tab).onExit = onPointExit;

                iTween.ScaleTo(tab, iTween.Hash(
                    "scale", new Vector3(1f, 1f, 1f),
                    "loopType", iTween.LoopType.none
                    ));
                iTween.ColorTo(tab, normalColor, 0.03f);
            }
        }
    }
}
