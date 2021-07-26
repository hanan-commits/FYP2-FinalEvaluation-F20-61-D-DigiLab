using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CheckQuizManager1 : MonoBehaviour
{
    [SerializeField] Text username;


    public void checkResult(){
        string buttonName = GameObject.Find("quiz1").GetComponentInChildren<Text>().text;
        PlayerPrefs.SetString("studentUsername", username.text);
        PlayerPrefs.SetString("QuizName", buttonName);
        Debug.Log(PlayerPrefs.GetString("QuizName"));
        SceneManager.LoadScene("CheckQuizTeacher");
    }
    public void checkResult2(){
        string buttonName = GameObject.Find("quiz2").GetComponentInChildren<Text>().text;
        PlayerPrefs.SetString("studentUsername", username.text);
        PlayerPrefs.SetString("QuizName", buttonName);
        Debug.Log(PlayerPrefs.GetString("QuizName"));
        SceneManager.LoadScene("CheckQuizTeacher");
    }

}
