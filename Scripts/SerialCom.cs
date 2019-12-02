using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
public class SerialCom : MonoBehaviour
{

  SerialPort sp = new SerialPort("/dev/cu.wchusbserial1420",9600);
  public static SerialCom sc;
  public static float x;
  public static float y;
  public static float z;

    void Awake()
    {
    sc = this;
   sp.Open();
   sp.ReadTimeout = 100;

}
  void Update()
    {
try{
   string[] read_input = sp.ReadLine().Split(',');
   x= float.Parse(read_input[1]);
   y = float.Parse(read_input[0]);
   z = float.Parse(read_input[2]);
   print(z);

  }
  catch(System.Exception) {
   }

    }
}
