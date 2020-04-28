using HVRCORE;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Use this for initialization

    //public Canvas canvas;
    public Slider slider;
    private Vector3 pos;
    private bool onece = true;
    private RectTransform rtSliderBtn;
    private Vector3 offset;
    private float initX;//滑块初始位置X
    private float newX;
    private float sliderWidth; //整个Slider宽度
    private float scaleX;//Slider宽度（X方向）缩放
    void Start()
    {
        //iTween.ColorFrom(gameObject, Color.red, 2.0f);
        rtSliderBtn = this.GetComponent<RectTransform>();
        RectTransform rtSlider = slider.GetComponent<RectTransform>();
        //RectTransform rtCanvas = canvas.GetComponent<RectTransform>();

        scaleX = rtSlider.lossyScale.x;
        sliderWidth = rtSlider.rect.width;
        initX = rtSliderBtn.position.x;
        
        HVREventListener.Get(transform.gameObject).onDrag = onPointerDrag;
    }

    private void onPointerDrag(GameObject go, PointerEventData eventData)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rtSliderBtn, eventData.position, eventData.pressEventCamera, out pos))//将屏幕空间上的点eventData.position转换为位于给定RectTransform平面上的世界空间中的位置pos。cam参数应该是与屏幕点相关的相机。对于Canvas设置为“Screen Space - Overlay mode”模式的情况，cam参数应该为null。
            {
                if (onece)
                {
                    onece = false;
                    offset = pos - rtSliderBtn.position;
                }
                newX = (pos - offset).x;
                float value = (newX - initX) * (slider.maxValue - slider.minValue) / (sliderWidth * scaleX) + slider.minValue;
                slider.value = value;

            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rtSliderBtn, Input.mousePosition, Camera.allCameras[1], out pos))//将屏幕空间上的点eventData.position转换为位于给定RectTransform平面上的世界空间中的位置pos。cam参数应该是与屏幕点相关的相机。对于Canvas设置为“Screen Space - Overlay mode”模式的情况，cam参数应该为null。
            {
                if (onece)
                {
                    onece = false;
                    offset = pos - rtSliderBtn.position;
                }
                newX =(pos - offset).x;
                float value = (newX - initX) * (slider.maxValue - slider.minValue) / (sliderWidth * scaleX) + slider.minValue;
                slider.value = value;
            }
        }
    }

    private void onPoninterDrop(GameObject go, PointerEventData eventData)
    {
        if (go == transform.gameObject)
        {
            onece = true;
        }
    }

}
