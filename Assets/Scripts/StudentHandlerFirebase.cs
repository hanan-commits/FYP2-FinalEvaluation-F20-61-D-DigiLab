using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CoroutineWithData {
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;
    public CoroutineWithData(MonoBehaviour owner, IEnumerator target) {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }
    private IEnumerator Run() {
        while(target.MoveNext()) {
            result = target.Current;
            yield return result;
        }
    }
}
public class StudentHandlerFirebase : MonoBehaviour
{
    public Text students;
    public DependencyStatus dependencyStatus;
    public DatabaseReference DBreference;

    public FirebaseAuth auth;    
    public FirebaseUser User;
    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
        StartCoroutine(begin());
        Debug.Log("Magic Starting");


    }
    public string stringToEdit = "Hello World\nI've got 2 lines...";

    void OnGUI()
    {
        // Make a multiline text area that modifies stringToEdit.
        stringToEdit = GUI.TextArea(new Rect(600, 100, 200, 100), stringToEdit, 200);
    }
    private IEnumerator begin(){
        List<string> array = new List<string>();
        CoroutineWithData cd = new CoroutineWithData(this, getData( array) );
        yield return cd.coroutine;
        var idList = cd.result; 
        System.Collections.Generic.List<string> listID = (System.Collections.Generic.List<string>)cd.result;
        Debug.Log("result is " + array.Capacity);

    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    private  IEnumerator getData(List<string> array){

        var DBTask = FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWith(task => {
        if (task.IsFaulted) 
        {
            // Handle the error...
        }
        else if (task.IsCompleted) 
        {
            DataSnapshot levelSnapshot = task.Result;
            
            foreach(var userId in levelSnapshot.Children) // users
            {
//                Debug.LogFormat("Key = {0}", userId.Key);  // "Key = rules"
                array.Add(userId.Key);
                //students.text = userId.Key;
               // Debug.Log("Added");
//                foreach(var levels in rules.Children)         //levels
//                {
//                    Debug.LogFormat("Key = {0}", levels.Key); //"Key = levelNumber"
//                   foreach(var levelNumber in levels.Child) // levelNumber
 //                    {
  //                   //Debug.Log("BEGIN");
   //                  Debug.LogFormat("Key = {0}, Value = {0}", levelNumber.Key, levelNumber.Value.ToString()); //"oneStarTime" : 0,"twoStarTime" : 30,"threeStarTime" : 45
                     //Debug.Log("END");
   //                  } // levelNumbers
    //            }  // levels
            } //rules
            //Debug.Log(array[1]);
            students.text = array[0] + "\n" + array[1];
    
        }
    
    });
       // students.text = array[0];
        yield return array;
        
    }
    public void loadData(List<string> array)
    {
        //Get the currently logged in user data
       // var DBTask = DBreference.Child("users").Child(User.UserId).Child("quiz").Child(quizName).GetValueAsync();

        FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWith(task => {
        if (task.IsFaulted) 
        {
            // Handle the error...
        }
        else if (task.IsCompleted) 
        {
            DataSnapshot levelSnapshot = task.Result;
            
            foreach(var userId in levelSnapshot.Children) // users
            {
                Debug.LogFormat("Key = {0}", userId.Key);  // "Key = rules"
                array.Add(userId.Key);
               // students.text = userId.Key.ToString() + "\n"; 
//                foreach(var levels in rules.Children)         //levels
//                {
//                    Debug.LogFormat("Key = {0}", levels.Key); //"Key = levelNumber"
//                   foreach(var levelNumber in levels.Child) // levelNumber
 //                    {
  //                   //Debug.Log("BEGIN");
   //                  Debug.LogFormat("Key = {0}, Value = {0}", levelNumber.Key, levelNumber.Value.ToString()); //"oneStarTime" : 0,"twoStarTime" : 30,"threeStarTime" : 45
                     //Debug.Log("END");
   //                  } // levelNumbers
    //            }  // levels
            } //rules
            
        }
    });
    }
}
