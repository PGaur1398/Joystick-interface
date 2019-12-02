using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

 public GameObject BackLights;
 public Transform path;
 private List<Transform> nodes;
 private int current_node = 0;
 public float maxSteeringAngle = 30;
 public float turn_speed  = 5f;
 public WheelCollider FrontCL, FrontCR;
 public WheelCollider RearCL, RearCR;
 public Transform FrontTL,FrontTR;
 public Transform RearTL,RearTR;
 public Vector3 centerOfMass;
 public float motorForce = 10;
 public float currentspeed;
 public float maxSpeed = 100f;
 public bool is_braking  = false;
 public float Brakes = 3000f;
 private float targetSteerAngle = 0;
 int iter = 0;
 int[] break_points = new int[]{2,9,10,13,15,16,20,21,22,26,28,30,34};
 [Header("Sensors")]

 private float sensorLength = 2f;
 private float frontSensorPosition = 0.2f;
 private float sideSensorPosition = 0.2f;
 private float frontSensorAngle = 30f;
 private void Start () {

GetComponent<Rigidbody>().centerOfMass = centerOfMass;
	Transform[] pathtransform = path.GetComponentsInChildren<Transform>();
    nodes = new List<Transform>();
    for(int i =0;i<pathtransform.Length;i++){

      if(pathtransform[i] != path.transform)
      nodes.Add(pathtransform[i]);
    }
	}
  IEnumerator BrakeAtTurn(int n){
   is_braking = true;
   yield return new WaitForSeconds(n);
   is_braking = false;
  }

	private void ApplySteer(){

	 Vector3 relativeVector = transform.InverseTransformPoint(nodes[current_node].position);
	 float steering_angle = (relativeVector.x/relativeVector.magnitude) * maxSteeringAngle;
	 targetSteerAngle = steering_angle;
	}
	private void AIdrive(){
		currentspeed = 2 * Mathf.PI * FrontCL.radius * FrontCL.rpm * 60 /1000;
		if(currentspeed < maxSpeed && !is_braking){
		FrontCL.motorTorque = motorForce;
		FrontCR.motorTorque = motorForce;
		RearCL.motorTorque =  motorForce;
		RearCR.motorTorque =  motorForce;
	}
	else {
		FrontCL.motorTorque = 0;
		FrontCR.motorTorque = 0;
		RearCL.motorTorque =  0;
		RearCR.motorTorque =  0;
	}
	}
	private void UpdateWheelPosses(){
		UpdateWheelpose(FrontCL,FrontTL);
		UpdateWheelpose(FrontCR,FrontTR);
		UpdateWheelpose(RearCL,RearTL);
		UpdateWheelpose(RearCR,RearTR);
	}
	private void UpdateWheelpose(WheelCollider collider, Transform transform){
	  Vector3 pos = transform.position;
	  Quaternion quat = transform.rotation;
	  collider.GetWorldPose(out pos, out quat);
	  transform.position = pos;
	  transform.rotation = quat;
	}
	private void GetDistance(){
		if(Vector3.Distance(transform.position,nodes[current_node].position) < 3.5f){
			if(current_node == nodes.Count -1){
				current_node  = 0;

	   }
    else{
 		current_node++;
    if(current_node == break_points[iter])
    {
      if(iter > 8){
        StartCoroutine(BrakeAtTurn(5));
      }

    else {
      StartCoroutine(BrakeAtTurn(2));
    }
      iter = iter + 1;

      if(iter == break_points.Length)
      {
        iter = 0;
      }


    }
  }
  Debug.Log(nodes[current_node]);


	}
	}
  private void Braking(){
    if(is_braking){
    BackLights.gameObject.SetActive(true);
    FrontCL.brakeTorque = Brakes;
    FrontCR.brakeTorque = Brakes;
    FrontCL.motorTorque = 0;
    FrontCR.motorTorque = 0;
    }
    else{
      BackLights.gameObject.SetActive(false);
      FrontCL.brakeTorque = 0;
      FrontCR.brakeTorque = 0;
    }
  }
private void Sensors(){
  RaycastHit hit;
  Vector3 sensorStartPos = transform.position;
  sensorStartPos.z += frontSensorPosition;
  if(Physics.Raycast(sensorStartPos,transform.forward,out hit,sensorLength))
  {
    if(hit.collider.CompareTag("Terrain"))
  Debug.DrawLine(sensorStartPos,hit.point);
  }

  sensorStartPos.x += sideSensorPosition;
  if(Physics.Raycast(sensorStartPos,transform.forward,out hit,sensorLength))
  {
    if(hit.collider.CompareTag("Terrain"))
  Debug.DrawLine(sensorStartPos,hit.point);
  }

  if(Physics.Raycast(sensorStartPos,Quaternion.AngleAxis(frontSensorAngle,transform.up) * transform.forward,out hit,sensorLength))
  {
  if(hit.collider.CompareTag("Terrain"))
  Debug.DrawLine(sensorStartPos,hit.point);
  }

  sensorStartPos.x -= 2 * sideSensorPosition;
  if(Physics.Raycast(sensorStartPos,transform.forward,out hit,sensorLength))
  {
    if(hit.collider.CompareTag("Terrain"))
Debug.DrawLine(sensorStartPos,hit.point);
  }

  if(Physics.Raycast(sensorStartPos,Quaternion.AngleAxis(-frontSensorAngle,transform.up) * transform.forward,out hit,sensorLength))
  {
    if(hit.collider.CompareTag("Terrain"))
Debug.DrawLine(sensorStartPos,hit.point);
  }

}
private void lerpToSteer(){
  FrontCL.steerAngle = Mathf.Lerp(FrontCL.steerAngle,targetSteerAngle,Time.deltaTime * turn_speed);
  FrontCR.steerAngle = Mathf.Lerp(FrontCR.steerAngle,targetSteerAngle,Time.deltaTime * turn_speed);
}

private void FixedUpdate() {
   // Sensors();
	 ApplySteer();
   lerpToSteer();
   AIdrive();
	 UpdateWheelPosses();
	 GetDistance();
   Braking();

	}
}
