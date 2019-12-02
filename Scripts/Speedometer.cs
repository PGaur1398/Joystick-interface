using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour {

	static float minAngle =  127.8f;
	static float maxAngle = -100.0f;
	static Speedometer speedo;
	void Start () {

		speedo = this;

	}

	public static void ShowSpeed(float speed,float min, float max)
	{
		float ang = Mathf.Lerp(minAngle,maxAngle,Mathf.InverseLerp(min,max,speed));
		speedo.transform.eulerAngles = new Vector3(0,0,ang);
	}

}
