using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRender : MonoBehaviour {

    public string text;
    public Texture texture;
    public int id;
    public HVREventListener.VoidDelegate onPointerClick;

    private GameObject go;
    // Use this for initialization
    void Start () {
        go = transform.gameObject;
        HVREventListener.Get(go).onEnter = onPointEnter;
        HVREventListener.Get(go).onExit = onPointExit;
        HVREventListener.Get(go).onClick = onPointerClick;
        Text txt = go.GetComponentInChildren<Text>();
        if (txt != null && !string.IsNullOrEmpty(text))
        {
            txt.text = text;
        }
        RawImage rimg = go.GetComponentInChildren<RawImage>();
        if (rimg != null && texture!=null)
        {
            rimg.texture = texture;
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

    //private void onPointerClick(GameObject go)
    //{
    //    SceneManager.LoadScene("VideoScene");
    //}
}
