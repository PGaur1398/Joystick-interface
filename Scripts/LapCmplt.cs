using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCmplt : MonoBehaviour {
public GameObject lapCompleteTrig;
public GameObject halfLapTrigg;
void OnTriggerEnter(){
	lapCompleteTrig.SetActive(true);
	halfLapTrigg.SetActive(false);

}
}
