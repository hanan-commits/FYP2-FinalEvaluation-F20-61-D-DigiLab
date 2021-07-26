using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;


public class CheckQuiz1 : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference DBreference;

    public FirebaseAuth auth;    
    public FirebaseUser User;
    [Header("MakeQuiz")]
    public TMP_InputField UserName;
    public TMP_InputField QuizName;
    public TMP_InputField QuestionField1;
    public TMP_InputField QuestionField2;
    public TMP_InputField QuestionField3;
    public TMP_InputField QuestionField4;
    public TMP_InputField QuestionField5;
    public TMP_InputField AnswerField1;
    public TMP_InputField AnswerField2;
    public TMP_InputField AnswerField3;
    public TMP_InputField AnswerField4;
    public TMP_InputField AnswerField5;
    public TMP_InputField StudentAnswerField1;
    public TMP_InputField StudentAnswerField2;
    public TMP_InputField StudentAnswerField3;
    public TMP_InputField StudentAnswerField4;
    public TMP_InputField StudentAnswerField5;

    string userName;
    string quizName;

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
        quizName = PlayerPrefs.GetString("QuizName");
        
        UserName.text = PlayerPrefs.GetString("studentUserName");
        userName = PlayerPrefs.GetString("studentUsername");
        QuizName.text = quizName;
        
    }
    public void loadDashboard(){
        SceneManager.LoadScene("TeacherDashboard");
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void loadAnswers(){
        StartCoroutine(loadQuizData());
    }
    private IEnumerator loadQuizData()
    {
        //Get the currently logged in user data
       // var DBTask = DBreference.Child("users").Child(User.UserId).Child("quiz").Child(quizName).GetValueAsync();
        
        Debug.Log(quizName);
        quizName = quizName.Trim();
        userName = userName.ToLower();
        Debug.Log(userName);
        string userId = PlayerPrefs.GetString("UserId");
        Debug.Log(userId);
        var DBTask = DBreference.Child("users").Child(userId).Child("quiz").Child(quizName).GetValueAsync();
        
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            QuestionField1.text = "0";
            QuestionField2.text = "0";
            QuestionField3.text = "0";
            QuestionField4.text = "0";
            QuestionField5.text = "0";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            QuestionField1.text = snapshot.Child("question1").Value.ToString();
            QuestionField2.text = snapshot.Child("question2").Value.ToString();
            QuestionField3.text = snapshot.Child("question3").Value.ToString();
            QuestionField4.text = snapshot.Child("question4").Value.ToString();
            QuestionField5.text = snapshot.Child("question5").Value.ToString();

            AnswerField1.text = snapshot.Child("Actual answer1").Value.ToString();
            AnswerField2.text = snapshot.Child("Actual answer2").Value.ToString();
            AnswerField3.text = snapshot.Child("Actual answer3").Value.ToString();
            AnswerField4.text = snapshot.Child("Actual answer3").Value.ToString();
            AnswerField5.text = snapshot.Child("Actual answer5").Value.ToString();

            StudentAnswerField1.text = snapshot.Child("Student answer1").Value.ToString();
            StudentAnswerField2.text = snapshot.Child("Student answer2").Value.ToString();
            StudentAnswerField3.text = snapshot.Child("Student answer3").Value.ToString();
            StudentAnswerField4.text = snapshot.Child("Student answer4").Value.ToString();
            StudentAnswerField5.text = snapshot.Child("Student answer5").Value.ToString();

        }
    }   
}
