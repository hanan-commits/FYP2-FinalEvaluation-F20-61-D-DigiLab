using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ResultManager : MonoBehaviour
{
    public Text ScalerText ;
    public Text Frequency;
    public Text Length;
    public GameObject placedPrefab;

    private GameObject information;
    private double g = 9.86;
    private double pi = 3.142857;
    private bool scaleChanged = false;
    public Button scaleRopeBtn;
    private Vector3 scaleFactor = new Vector3(0f, 0.02f, 0f);
    
    // Start is called before the first frame update
    void Start()
    {
        scaleRopeBtn.onClick.AddListener(scaleRopeFtn);

        
        ScalerText.text = "Time Period \n ";
        Frequency.text =  "Frequency \n ";
        Length.text = "Length \n";
        information = placedPrefab;

      /*  var btnTransform = placedPrefab.transform;    
        var Rope = btnTransform.GetChild(4);
        var scale = Rope.transform.localScale.y;

        ScalerText.text += scale.ToString("f2");

        double lenght = Convert.ToDouble(scale);
        double results =getResults(lenght);
        double freq = 1 / results;
        ScalerText.text += "Time Period: " + results.ToString("f2");
        Frequency.text += freq.ToString("f2") +"\n";*/
    }

    public void scaleRopeFtn(){
        var btnTransform = placedPrefab.transform;    
        var Rope = btnTransform.GetChild(4);
        Rope.transform.localScale += scaleFactor;
        var scale = Rope.transform.localScale.y;
        double lenght = Convert.ToDouble(scale);
        double results =getResults(lenght);
        double freq = 1 / results;
        ScalerText.text += "\n" + results.ToString("f2");
        Frequency.text += "\n" +freq.ToString("f4") ;
        Length.text += "\n" +lenght.ToString("f2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    double getResults(double lenght)
    {
        double timePeriod = (2 * pi)* Mathf.Sqrt((float)lenght / (float)g);
        return timePeriod;
    }
    void OnDestroy(){
        placedPrefab = information;
    }
}
