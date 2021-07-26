using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ChooseQuiz : MonoBehaviour
{

    public void QuizButton(){
        Debug.Log("QuizName");
        string text = GameObject.Find("buttonName").GetComponentInChildren<TMP_Text>().text;
        Debug.Log(text);
        PlayerPrefs.SetString("QuizName", text);
        SceneManager.LoadScene("AttemptQuiz");
        Debug.Log(PlayerPrefs.GetString("QuizName"));
    }
    public void Quiz2Button(){
        Debug.Log("QuizName");
        string text = GameObject.Find("quiz2Btn").GetComponentInChildren<TMP_Text>().text;
        Debug.Log(text);
        PlayerPrefs.SetString("QuizName", text);
        SceneManager.LoadScene("AttemptQuiz");
        Debug.Log(PlayerPrefs.GetString("QuizName"));
    }
}
