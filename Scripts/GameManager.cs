using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public static GameManager gm;
  public static int minuteCount;
  public static int secCount;
  // public AudioSource musicAudioSource;
  public bool gameIsOver = false;
  public string playAgainLevelToLoad;
  public static float milliCount;
  public static string milliDisplay;
  public GameObject minuteDisp;
  public GameObject secDisp;
  public GameObject milliDisp;
  public GameObject controller;
  public GameObject CounDown;
  public static float rawTime;
  public GameObject AIController;
  void Start()
    {
      if (gm == null)
      gm =this.gameObject.GetComponent<GameManager>();
      StartCoroutine(CountStart());
    }
  IEnumerator CountStart(){
      CounDown.GetComponent<Text>().text = "3";
      yield return new WaitForSeconds(1);
      CounDown.GetComponent<Text>().text = "2";
      yield return new WaitForSeconds(1);
      CounDown.GetComponent<Text>().text = "1";
      yield return new WaitForSeconds(1);
      CounDown.GetComponent<Text>().text = "START";
      gameIsOver = false;
      yield return new WaitForSeconds(1);
      CounDown.GetComponent<Text>().text = "";
    }
void Update(){
  if(!gm.gameIsOver){
  milliCount += Time.deltaTime * 10;
  rawTime = Time.deltaTime;
  milliDisplay =  milliCount.ToString("F0");
  milliDisp.GetComponent<Text>().text = "0" + milliDisplay + ".";
 if(milliCount >= 10){
   milliCount = 0;
   secCount += 1;
 }
 if(secCount <= 9){
   secDisp.GetComponent<Text>().text = "0" + secCount + ":";
 }
 else {
   secDisp.GetComponent<Text>().text  = "" + secCount + ":";
 }
 if(secCount >= 60){
   secCount = 0;
   minuteCount += 1;
 }
 if(minuteCount <= 9){
   minuteDisp.GetComponent<Text>().text = "0" + minuteCount + ":";
 }
 else {
   minuteDisp.GetComponent<Text>().text = "" + minuteCount + ":";
 }
controller.GetComponent<CarController>().enabled = true;
AIController.GetComponent<AIController>().enabled = true;

}
else{
  controller.GetComponent<CarController>().enabled = false;
  AIController.GetComponent<AIController>().enabled = false;

}
}



  public void  EndGame(){
    gameIsOver = true;

  }
  public void RestartGame(){
    SceneManager.LoadScene(playAgainLevelToLoad);
  }

}
