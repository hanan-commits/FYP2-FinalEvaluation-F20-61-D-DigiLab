using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;


public class AttemptQuizManager : MonoBehaviour
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

    string [] actualAnswers = new string [5];
    string userId ;
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
        string quizName = PlayerPrefs.GetString("QuizName");
        QuizName.text = quizName;
        Debug.Log(PlayerPrefs.GetString("QuizName"));
        userId = PlayerPrefs.GetString("UserId");

    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void startQuiz(){
        StartCoroutine(loadQuizData(QuizName.text));
    }

    public void RegisterButton()
    {
        Debug.Log("Making Arrays");
        string [] questions = new string[5] {QuestionField1.text,QuestionField2.text,QuestionField3.text,QuestionField4.text,QuestionField5.text}; 
        string [] answers = new string [5] {AnswerField1.text,AnswerField2.text,AnswerField3.text,AnswerField4.text,AnswerField5.text};
        Debug.Log("Starting Coroutine");
        StartCoroutine(UpdateQuestionsDatabase(questions,answers));
        SceneManager.LoadScene("QuizDashboard");
        Debug.Log(" Coroutine Ended");
    }

    private IEnumerator UpdateQuestionsDatabase(string [] questions, string [] answers)
    {
        //Set the questions and answers
        for(int i = 0; i < questions.Length; ++i){
            var DBTask = DBreference.Child("users").Child(userId).Child("quiz").Child(QuizName.text).Child("question" + (i+1).ToString()).SetValueAsync(questions[i]);
            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else
            {
                //Database username is now updated
            }
            var DBTask2 = DBreference.Child("users").Child(userId).Child("quiz").Child(QuizName.text).Child("Student answer" + (i+1).ToString()).SetValueAsync(answers[i]);
            yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);

            if (DBTask2.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask2.Exception}");
            }
            else
            {
                //Database username is now updated
            }
            var DBTask3 = DBreference.Child("users").Child(userId).Child("quiz").Child(QuizName.text).Child("Actual answer" + (i+1).ToString()).SetValueAsync(actualAnswers[i]);
            yield return new WaitUntil(predicate: () => DBTask3.IsCompleted);

            if (DBTask3.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask3.Exception}");
            }
            else
            {
                //Database username is now updated
            }            
        }
    }

    
    private IEnumerator loadQuizData(string quizName)
    {
        //Get the currently logged in user data
       // var DBTask = DBreference.Child("users").Child(User.UserId).Child("quiz").Child(quizName).GetValueAsync();
        var DBTask = DBreference.Child("quiz").Child(quizName).GetValueAsync();
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

            actualAnswers[0] = snapshot.Child("answer1").Value.ToString();
            actualAnswers[1] = snapshot.Child("answer2").Value.ToString();
            actualAnswers[2] = snapshot.Child("answer3").Value.ToString();
            actualAnswers[3] = snapshot.Child("answer4").Value.ToString();
            actualAnswers[4] = snapshot.Child("answer5").Value.ToString();


        }
    }

}
