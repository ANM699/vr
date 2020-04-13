using HVRCORE;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    private int index = 0;
    public MediaPlayer PlayingPlayer;

    // Use this for initialization
    void Start()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("menu");
        foreach (GameObject go in gos)
        {
            HVREventListener.Get(go).onClick = onPointerClick;
        }
        //HVREventListener.Get(transform.gameObject).onClick = onPointerClick;
    }
    private void onPointerClick(GameObject go)
    {
        string goName = go.name;
        if (goName == "LoadSkybox")
        {
            loadSkybox();
        }
        else if (goName == "LoadScene")
        {
            SceneManager.LoadScene("HomeScene");
        }
        else if (goName == "Play")
        {
            if (PlayingPlayer)
            {
                PlayingPlayer.Control.Play();
            }
        }
        else if (goName == "Pause")
        {
            if (PlayingPlayer)
            {
                PlayingPlayer.Control.Pause();
            }
        }
    }

    private void loadSkybox()
    {
        Object[] skyboxmats = Resources.LoadAll("Materials/Skybox") as UnityEngine.Object[];
        //Material skyboxmat = Resources.Load("Materials/Skybox/SeasideVilla") as Material;
        if (skyboxmats != null)
        {
            int len = skyboxmats.Length;
            bool ret = HVRCamCore.UseSkyBox(true, skyboxmats[index] as Material);
            if (ret)
            {
                index = ++index % len;
                Debug.Log("Materials load success!");
            }
            else
            {
                Debug.Log("Materials load failed!");
            }
        }
        else { Debug.Log("material not loaded"); }
    }
}
