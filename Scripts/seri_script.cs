using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
public class SerComm : MonoBehaviour {
SerialPort sp = new SerialPort("/dev/cu.wchusbserial1420",9600);
	// Use this for initialization
	void Start () {
		sp.Open();
		sp.ReadTimeout = 1;

	}

	// Update is called once per frame
	void Update () {
try{
	print(sp.ReadLine());
}
catch(System.Exception){

}
	}
}
