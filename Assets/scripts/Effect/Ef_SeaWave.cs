using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_SeaWave : MonoBehaviour
{
   private Vector3 tmp;

   void Start(){
       tmp = -transform.position;
   }

   void Update(){
       transform.position = Vector3.MoveTowards(transform.position, tmp, 10*Time.deltaTime);

   }
}
