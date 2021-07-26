using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems; 
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]

public class ScrewGaugeController : MonoBehaviour
{
     public GameObject gameObjectToInstantiate;

     private GameObject spawnedObject;
     private ARRaycastManager _arRaycastManager;
     private Vector2 touchPosition;
     public Canvas canvas;
     private Quaternion rot = new Quaternion (0,-90,0,1);
    public Button openButton;
    public Button closedButton;
    public Button getResults;
    public Text SrNo;
    public Text Enteries;
    private int count = 1;

     static List<ARRaycastHit> hits = new List<ARRaycastHit>();

     private void Awake()
     {
          _arRaycastManager = GetComponent<ARRaycastManager>();
     }
     
     void Start(){
        openButton.onClick.AddListener(OpenJaw);
        closedButton.onClick.AddListener(CloseJaw);   
        getResults.onClick.AddListener(showResults);
     }

     bool TryGetTouchPosition(out Vector2 touchPosition)
     {
          if(Input.touchCount > 0)
          {
               touchPosition = Input.GetTouch(0).position;
               return true;
          }
          touchPosition = default;
          return false;
     }
     void Update()
     {
          if(!TryGetTouchPosition(out Vector2 touchPosition))
          {
               return;
          }
          if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
          {
               var hitPose = hits[0].pose;

               if(spawnedObject == null)
               {
                    spawnedObject = Instantiate(gameObjectToInstantiate, hitPose.position, rot);
                    canvas.gameObject.SetActive(true);
               }
               else
               {
                   // spawnedObject.transform.position = hitPose.position;
                    canvas.gameObject.SetActive(false);
               }
          }
     }
     public void OpenJaw(){
        var prefabTransform = spawnedObject.transform;
        var jawPos = prefabTransform.GetChild(8);
        jawPos.position = jawPos.position + new Vector3 ((float) 0,0,(float)0.005);

    }
    public void CloseJaw(){
        var prefabTransform = spawnedObject.transform;
        var jawPos = prefabTransform.GetChild(8);
        jawPos.position += new Vector3 ((float)0,0,(float)-0.005);
    }
    public void showResults(){
        var prefabTransform = spawnedObject.transform;
        var cylinder = prefabTransform.GetChild(9);         
          var lenghtCylinder =cylinder.transform.localScale.y;
          Enteries.text += lenghtCylinder.ToString("f5") + "\n";
          SrNo.text += count.ToString() +"\n";
          count += 1;

    }
}
