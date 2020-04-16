using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{

    public GameObject prefabTab;
    public GameObject prefabItemPanel;
    public Transform tabPanel;//挂载tab
    public Transform mainPanel;//挂载itemPanel
    private GameObject[] tabs;
    private GameObject[] itemPanels;

    void Awake()
    {
        UnityNetworkManager.Instance.Get(Global.CATEGORYLIST, handleReqTabs);
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
            itemPanels = GameObject.FindGameObjectsWithTag("ItemPanel");

            foreach (GameObject tab in tabs)
            {
                TabEventSystem tabEvent = tab.GetComponent<TabEventSystem>();
                tabEvent.tabs = tabs;
                tabEvent.itemPanels = itemPanels;
                //初始化时默认选择第一项
                if (tab.transform.GetSiblingIndex() == 0)
                {
                    tabEvent.onPointerClick(tab);
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
        //HVREventListener.Get(go).onClick = onPointerClick;
        Text txt = go.GetComponentInChildren<Text>();
        if (txt != null)
        {
            txt.text = tab["name"];
        }

        //同时创建对应的itemPanel
        GameObject goItemPanel = Instantiate(prefabItemPanel, mainPanel, false);
        goItemPanel.tag = "ItemPanel";
        goItemPanel.name = tab["cid"];

        //tab跟itemPanel做关联
        TabEventSystem tabEvent = go.GetComponent<TabEventSystem>();
        tabEvent.itemPanel = goItemPanel;

    }

}
