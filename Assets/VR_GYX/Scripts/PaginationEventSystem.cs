using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PaginationEventSystem : MonoBehaviour {

    //public static int curPage=0;
    //public static int totalPage = 0;

    void Awake()
    {
        foreach (Transform tf in transform)
        {
            HVREventListener.Get(tf.gameObject).onEnter = onPointEnter;
            HVREventListener.Get(tf.gameObject).onExit = onPointExit;
            HVREventListener.Get(tf.gameObject).onClick = onPointerClick;
        }
    }

    public void onPointerClick(GameObject go)
    {
        if (go.name=="PageUp")
        {
            Global.curItemController.pageUp();
        }
        else if (go.name == "PageDown")
        {
                Global.curItemController.pageDown();
        }
    }

    private void onPointEnter(GameObject go)
    {
        Color hoverColor = new Color(0.8f, 0.077f, 0.147f, 1.0f);
        
        iTween.ScaleTo(go, iTween.Hash(
              "scale", new Vector3(1.05f, 1.05f, 1.05f),
            "loopType", iTween.LoopType.none
        ));
        iTween.ColorTo(go, hoverColor, 0.03f);
    }

    private void onPointExit(GameObject go)
    {
        Color normalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        iTween.ScaleTo(go, iTween.Hash(
            "scale", new Vector3(1f, 1f, 1f),
            "loopType", iTween.LoopType.none
        ));
        iTween.ColorTo(go, normalColor, 0.03f);
    }
}
