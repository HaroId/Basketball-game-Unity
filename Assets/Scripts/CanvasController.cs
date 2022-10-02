using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Image imgSelector;
    public Slider sliderbar;

    public void changePickableBallColor(bool isSelect){
        if(isSelect){
            imgSelector.color = Color.green;
        }
        else
        {
            imgSelector.color = Color.white;
        }
    }

    public void OcultarCursor(bool ocultar){
        imgSelector.enabled = !ocultar;
    }

    public void ActivarSlider(bool activar){
        sliderbar.gameObject.SetActive(activar);
    }

    public void SetValueBar(float vld){
        sliderbar.value = vld;
    }
}
