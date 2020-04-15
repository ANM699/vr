using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{

    public GameObject prefabTab;
    public GameObject prefabItemPanel;
    public Transform tabPanel;//挂载tab
    public Transform mainPanel;//挂载itemPanel
    private GameObject[] tabs;

    private static readonly string URL = "https://bestvapi.bestv.cn/api/category_list?app=default&cid=386&channel_id=54a26d41-a0d2-405a-b01a-280f549d8b1e";
    // Use this for initialization
    void Awake()
    {
        UnityNetworkManager.Instance.Get(URL, handleReqTabs);
    }

    private void handleReqTabs(bool isError, string data)
    {
        if (!isError)
        {
            var jsonData = JSON.Parse(data).AsArray;
            for (int i = 0; i < jsonData.Count; i++)
            {
                //创建item
                CreateTabAndItemPanel(jsonData[i]);
            }
            tabs = GameObject.FindGameObjectsWithTag("Tab");
            //GameObject[] itemPanels = GameObject.FindGameObjectsWithTag("ItemPanel");
            foreach (GameObject tab in tabs)
            {
                //初始化时默认选择第一项
                if (tab.transform.GetSiblingIndex() == 0)
                {
                    ItemController itemCtrl = GetComponent<ItemController>();
                    //itemCtrl.itemPanel = itemPanels[0].transform;
                    onPointerClick(tab);
                }
            }
        }
    }

    private void CreateTabAndItemPanel(JSONNode tab)
    {
        //创建左侧tab
        GameObject go = Instantiate(prefabTab, tabPanel, false);
        go.tag = "Tab";
        go.name = tab["cid"];
        //HVREventListener.Get(go).onEnter = onPointEnter;
        //HVREventListener.Get(go).onExit = onPointExit;
        HVREventListener.Get(go).onClick = onPointerClick;
        Text txt = go.GetComponentInChildren<Text>();
        if (txt != null)
        {
            txt.text = tab["name"];
        }

        //同时创建对应的itemPanel
        GameObject goItemPanel = Instantiate(prefabItemPanel, mainPanel, false);
        go.tag = "ItemPanel";
        goItemPanel.name = tab["cid"];

    }

    private void onPointerClick(GameObject go)
    {
        animate(go);
        string programListUrl = string.Format("https://bestvapi.bestv.cn/api/program_list?cid={0}&len=8&p=1", go.name);
        UnityNetworkManager.Instance.Get(programListUrl, handleReqList);

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

    private void handleReqList(bool isError, string data)
    {
        if (!isError)
        {
            var jsonData = JSON.Parse(data);

        }
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
