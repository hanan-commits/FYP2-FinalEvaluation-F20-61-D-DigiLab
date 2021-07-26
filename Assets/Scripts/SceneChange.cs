using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public void ShowHistory()
    {
        SceneManager.LoadScene("ViewHistory");
    }
    
    public void ShowTeacherLogin()
    {
        SceneManager.LoadScene("TeacherLogin");
    }
    public void ShowStudentDashboard(){
        SceneManager.LoadScene("StudentDashboard");
    }
    public void ShowTeacherDashboard(){
        SceneManager.LoadScene("TeacherDashboard");
    }
    public void ShowTest(){
        SceneManager.LoadScene("test");
    }
    public void CreateQuiz(){
        SceneManager.LoadScene("CreateQuiz");
    }
    public void ShowPractical()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowProcedure()
    {
        SceneManager.LoadScene("ViewProcedure");
    }

    public void ShowLogin()
    {
        SceneManager.LoadScene("Login");
    }
    public void ShowQuizDashboard(){
        SceneManager.LoadScene("QuizDashboard");
    }
    public void ShowSignUp()
    {
        SceneManager.LoadScene("SignUp");
    }
    public void ShowScrewGauge(){
        SceneManager.LoadScene("ScrewGauge");
    }
    public void ShowDashboard()
    {
        SceneManager.LoadScene("Dashboard");
    }
    public void ShowScrewGaugeDetails(){
        SceneManager.LoadScene("ScrewGaugeDetails");
    }
    public void ShowDummy(){
        SceneManager.LoadScene("DummyScene");
    }
     public void ShowSimplePendulumChoices()
    {
        SceneManager.LoadScene("PracticalDetails");
    }
 
     public void ShowVernierCalliperChoices()
    {
        SceneManager.LoadScene("PracticalDetails2");
    }
       public void ShowSimplePendulum()
    {
        SceneManager.LoadScene("ViewProcedureTest1");
    }
    public void ShowScrewGaugeProcedure(){
        SceneManager.LoadScene("ViewProcedureTest3"); 
    }
     public void ShowVernierCalliper()
    {
        SceneManager.LoadScene("ViewProcedureTest2");
    }
    public void showARCalliper()
    {
        SceneManager.LoadScene("VernierCalliper");
    }
    public void showSpringBalanceChoices()
    {
        SceneManager.LoadScene("PracticalDetails4");
    }
    public void showARSpringBalance()
    {
        SceneManager.LoadScene("SpringBalance");
    }
    public void showSpringBalanceProcedure(){
        SceneManager.LoadScene("ViewProcedureTest4"); 
    }

}
