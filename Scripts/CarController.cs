using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.IO.Ports;

public class CarController : MonoBehaviour
{

public GameObject breaklights;
private float horizontal_input;
private float vertical_input;
private float steering_angle;
public WheelCollider FrontCL, FrontCR;
public WheelCollider RearCL, RearCR;
public Transform FrontTL,FrontTR;
public Transform RearTL,RearTR;
public float maxSteeringAngle = 30;
public float turnSpeed = 5f;
public float motorForce = 300;
public float Brakes = 3000f;
public AudioClip otherclip;
private float targetSteerAngle = 0;
private int still = 0;
Rigidbody rb;
AudioSource audio_source;
IEnumerator WaitAndExecute() {

  yield return new WaitForSeconds(10f);
  if(Mathf.Round(rb.velocity.magnitude) == still)
  print(Mathf.Round(rb.velocity.magnitude));
}

void Start(){
  audio_source = this.GetComponent<AudioSource>();
  rb = this.GetComponent<Rigidbody>();
}

public void GetInputs(){
if (this.GetComponent<SerialCom>().enabled){
  horizontal_input = SerialCom.x;
  vertical_input = SerialCom.y;
}
else{
  horizontal_input = Input.GetAxis("Horizontal");
  vertical_input = Input.GetAxis("Vertical");
}
 // Debug.Log(horizontal_input);
}
private void Steering(){
  steering_angle = maxSteeringAngle * horizontal_input;
  // Debug.Log(steering_angle);
  targetSteerAngle = steering_angle;

}
private void EngineSound(){
  audio_source.pitch = vertical_input + 1;
}

private void Accelerate(){
  FrontCL.motorTorque = vertical_input * motorForce;
  FrontCR.motorTorque = vertical_input * motorForce;
  RearCL.motorTorque = vertical_input * motorForce;
  RearCR.motorTorque = vertical_input * motorForce;
  Speedometer.ShowSpeed(rb.velocity.magnitude,0,50);


}
private void UpdateWheelPosses(){
  UpdateWheelpose(FrontCL,FrontTL);
  UpdateWheelpose(FrontCR,FrontTR);
  UpdateWheelpose(RearCL,RearTL);
  UpdateWheelpose(RearCR,RearTR);

}
private void brakes(){
  if (SerialCom.z == 1){
    breaklights.gameObject.SetActive(true);
    FrontCL.brakeTorque = Brakes;
    FrontCR.brakeTorque = Brakes;
    FrontCL.motorTorque = 0;
    FrontCR.motorTorque = 0;

    }
    else
    {
      breaklights.gameObject.SetActive(false);
      FrontCL.brakeTorque = 0;
      FrontCR.brakeTorque = 0;
    }
}
private void UpdateWheelpose(WheelCollider collider, Transform transform){
  Vector3 pos = transform.position;
  Quaternion quat = transform.rotation;
  collider.GetWorldPose(out pos, out quat);
  transform.position = pos;
  transform.rotation = quat;
}
private void lerpToSteerAngle(){
  FrontCL.steerAngle = Mathf.Lerp(FrontCL.steerAngle,targetSteerAngle,Time.deltaTime * turnSpeed);
  FrontCR.steerAngle = Mathf.Lerp(FrontCR.steerAngle,targetSteerAngle,Time.deltaTime * turnSpeed);

}
private void FixedUpdate(){
  EngineSound();
  GetInputs();
  Steering();
  lerpToSteerAngle();
  Accelerate();
  UpdateWheelPosses();
  brakes();

  // if(Mathf.Round(rb.velocity.magnitude) == still){
  //   StartCoroutine(WaitAndExecute());
  // }
}
}
