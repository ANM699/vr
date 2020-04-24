using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global : MonoBehaviour
{
    public static readonly string CATEGORYLIST = "https://bestvapi.bestv.cn/api/category_list?app=default&cid=386&channel_id=54a26d41-a0d2-405a-b01a-280f549d8b1e";
    public static readonly string PROGRAMLIST = "https://bestvapi.bestv.cn/api/program_list";

    public static string VidToPlay;
    public static ItemController curItemController;//当前选中的Tab所对应的ItemPanel中的ItemController组件
}
