using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems; 
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaceObjects : MonoBehaviour
{
     public GameObject gameObjectToInstantiate;

     private GameObject spawnedObject;
     private ARRaycastManager _arRaycastManager;
     private Vector2 touchPosition;
     public Canvas canvas;
     private Quaternion rot = new Quaternion (0,-180,0,1);
     public Button openButton;
     public Button closedButton;
     public Button getResults;
     public Button lockButton;
     public TextMeshProUGUI buttonText;
     public Text SrNo;
     public Text Enteries;
     private int count = 1;
     private bool isLocked = false;

     static List<ARRaycastHit> hits = new List<ARRaycastHit>();

     private void Awake()
     {
          _arRaycastManager = GetComponent<ARRaycastManager>();
     }
     
     void Start(){
          openButton.onClick.AddListener(OpenJaw);
          closedButton.onClick.AddListener(CloseJaw);   
          getResults.onClick.AddListener(showResults);
          lockButton.onClick.AddListener(lockModel);
     }

     void lockModel(){
          if(isLocked){
               buttonText.text = "Lock";
               isLocked = false;
          }
          else{
               buttonText.text = "Unlock";
               isLocked = true;
          }
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
          if(!isLocked){
               var prefabTransform = spawnedObject.transform;
               var jawPos = prefabTransform.GetChild(0);
               jawPos.position = jawPos.position + new Vector3 ((float) 0.005,0,0);
          }

     }
     public void CloseJaw(){
          if(!isLocked){
               var prefabTransform = spawnedObject.transform;
               var jawPos = prefabTransform.GetChild(0);
               jawPos.position += new Vector3 ((float)-0.005,0,0);
          }
     }
     public void showResults(){
          var prefabTransform = spawnedObject.transform;
          var cylinder = prefabTransform.GetChild(2);         
          var lenghtCylinder =cylinder.transform.localScale.y;
          Enteries.text += lenghtCylinder.ToString("f3") + "\n";
          SrNo.text += count.ToString() +"\n";
          count += 1;

     }
}
