using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalePrefab : MonoBehaviour
{
    private Slider scaleSlider;
    public float scaleMinValue;
    public float scaleMaxValue;

    // Start is called before the first frame update
    void Start()
    {
        scaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();
        scaleSlider.minValue = scaleMinValue;
        scaleSlider.maxValue = scaleMaxValue;

        scaleSlider.onValueChanged.AddListener(ScaleSliderUpdate);
        
    }
    void ScaleSliderUpdate(float value)
    {
        transform.localScale += new Vector3(0, value, 0);
    }


}
