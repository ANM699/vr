using HVRCORE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        iTween.ColorFrom(gameObject, Color.red, 2.0f);
    }

    void Update()
    {
    
        Quaternion rotation = HVRLayoutCore.m_CamCtrObj.transform.rotation; //获取相机旋转信息
        transform.rotation = rotation;

    }
}                             
