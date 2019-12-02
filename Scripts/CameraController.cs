using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
  public Transform objectToFollow;
  public Vector3 offset;
  public float followSpeed =10;
  public float lookSpeed = 10;


public void LookAtTarget(){
  Vector3 look_direction = objectToFollow.position - transform.position;
  Quaternion rot = Quaternion.LookRotation(look_direction,objectToFollow.up);
  transform.rotation = Quaternion.Slerp(transform.rotation,rot,lookSpeed * Time.deltaTime);
  }
  // public void MoveToTarget(){
  //      Vector3 targetPos = objectToFollow.position + offset;
  //      transform.position = Vector3.Lerp(transform.position,targetPos,followSpeed * Time.deltaTime);
  // }
  void FixedUpdate(){
  LookAtTarget();
  // MoveToTarget();
}
}
