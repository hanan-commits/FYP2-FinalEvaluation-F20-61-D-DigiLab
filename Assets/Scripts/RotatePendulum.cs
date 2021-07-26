using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatePendulum : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider rotateSlider;
    public float rotMinValue;
    public float rotMaxValue;
    void Start()
    {

        rotateSlider = GameObject.Find("RotationSlider").GetComponent<Slider>();
        rotateSlider.minValue = rotMinValue;
        rotateSlider.maxValue = rotMaxValue;

        rotateSlider.onValueChanged.AddListener(RotateSliderUpdate);
        
    }

    void RotateSliderUpdate(float value)
    {
        transform.localEulerAngles = new Vector3(0, value, 0);
    }

}
