using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
  public Color lineColor;
  private List<Transform> nodes;
void OnDrawGizmosSelected(){
    Gizmos.color = lineColor;
    Transform[] pathtransform = GetComponentsInChildren<Transform>();
    nodes = new List<Transform>();
    for(int i =0;i<pathtransform.Length;i++){

      if(pathtransform[i] != transform)
      nodes.Add(pathtransform[i]);
    }
    for(int i = 0; i< nodes.Count; i++) {
      Vector3 currentnode  = nodes[i].position;
      Vector3 previousnode = Vector3.zero;
      if(i>0)
      previousnode =  nodes[i-1].position;
      else if(i == 0 && nodes.Count >1)
      previousnode = nodes[nodes.Count -1].position;


   Gizmos.DrawLine(previousnode,currentnode);
   Gizmos.DrawWireSphere(currentnode,0.3f);
    }
  }
}
