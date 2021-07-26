using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems; 
using UnityEngine.UI;
using System;
[RequireComponent(typeof(ARRaycastManager))]



public class ScaleManager : MonoBehaviour
{
    public GameObject placedPrefab;
    private Vector3 scaleChange = new Vector3(0f, 0.02f, 0f);
    private bool doScale = false;
    public Text ScalerText ;

    private double g = 9.86;
    private double pi = 3.142857;
    // Start is called before the first frame update
    void Start()
    {
        ScalerText.text = "Rope Length: ";
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            var btnTransform = placedPrefab.transform;    
            var Rope = btnTransform.GetChild(4).GetChild(0);
            Rope.transform.localScale += scaleChange;
            var scale = Rope.transform.localScale.y;
            ScalerText.text += scale.ToString();
            doScale = false;
            double lenght = Convert.ToDouble(scale);
            double results =getResults(lenght);
            ScalerText.text += ": " + "The Time Period Is: " + results.ToString();
        }   
     
    }

    double getResults(double lenght)
    {
        double timePeriod = (2 * pi)* Mathf.Sqrt((float)lenght / (float)g);
        return timePeriod;
    }
}
