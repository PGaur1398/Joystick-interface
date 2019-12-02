using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour {

public GameObject LapCompleteTrigger;
public GameObject halfLapTrigger;
public GameObject minuteDisp;
public GameObject secDisp;
public GameObject milliDisp;
public GameObject  LapTimeBox;
private float rawTime;
void OnTriggerEnter(){
	rawTime = PlayerPrefs.GetFloat("RawTime");
	if(GameManager.rawTime <= rawTime){

	if(GameManager.secCount <= 9){
  secDisp.GetComponent<Text>().text = "0" + GameManager.secCount + ":";
	}
	else{
		secDisp.GetComponent<Text>().text  = "" + GameManager.secCount + ":";
	}
	if(GameManager.minuteCount <= 9){
	minuteDisp.GetComponent<Text>().text = "0" + GameManager.minuteCount + ":";
	}
	else{
		minuteDisp.GetComponent<Text>().text  = "" + GameManager.minuteCount + ":";
	}
	milliDisp.GetComponent<Text>().text = "" + GameManager.milliCount + ".";
}
PlayerPrefs.SetInt("MinSave",GameManager.minuteCount);
PlayerPrefs.SetInt("SecSave",GameManager.secCount);
PlayerPrefs.SetFloat("MilliSave",GameManager.milliCount);
PlayerPrefs.SetFloat("RawTime",GameManager.rawTime);

GameManager.minuteCount = 0;
GameManager.secCount = 0;
GameManager.milliCount = 0;
GameManager.rawTime = 0;
halfLapTrigger.SetActive(true);
LapCompleteTrigger.SetActive(false);
}


}
