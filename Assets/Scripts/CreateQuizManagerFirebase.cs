using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;



public class CreateQuizManagerFirebase : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference DBreference;

    public FirebaseAuth auth;    
    public FirebaseUser User;
    [Header("MakeQuiz")]
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
    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void RegisterButton()
    {
        Debug.Log("Making Arrays");
        string [] questions = new string[5] {QuestionField1.text,QuestionField2.text,QuestionField3.text,QuestionField4.text,QuestionField5.text}; 
        string [] answers = new string [5] {AnswerField1.text,AnswerField2.text,AnswerField3.text,AnswerField4.text,AnswerField5.text};
        Debug.Log("Starting Coroutine");
        for (int i = 0; i < questions.Length; ++i){
            StartCoroutine(UpdateQuestionsDatabase(questions, i));
            StartCoroutine(UpdateAnswersDatabase(answers, i));
        }
        Debug.Log(" Coroutine Ended");
        
        SceneManager.LoadScene("TeacherDashboard");
    }

    private IEnumerator UpdateQuestionsDatabase(string [] questions,int i)
    {
        
        //Set the questions and answers
        
            var DBTask = DBreference.Child("quiz").Child(QuizName.text).Child("question" + (i+1).ToString()).SetValueAsync(questions[i]);
            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else
            {
                //Database username is now updated
            }
        
    }
    private IEnumerator UpdateAnswersDatabase(string [] answers,int i){
     
            var DBTask2 = DBreference.Child("quiz").Child(QuizName.text).Child("answer" + (i+1).ToString()).SetValueAsync(answers[i]);
            yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);

            if (DBTask2.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask2.Exception}");
            }
            else
            {
                //Database username is now updated
            }
        
    }
}
