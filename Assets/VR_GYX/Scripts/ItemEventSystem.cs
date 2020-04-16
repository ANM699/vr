using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemEventSystem : MonoBehaviour
{

    void Awake()
    {
        HVREventListener.Get(transform.gameObject).onEnter = onPointEnter;
        HVREventListener.Get(transform.gameObject).onExit = onPointExit;
        HVREventListener.Get(transform.gameObject).onClick = onPointerClick;
    }

    public void onPointerClick(GameObject go)
    {
        Global.VidToPlay = go.name;
        SceneManager.LoadScene("VideoScene");

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
}
