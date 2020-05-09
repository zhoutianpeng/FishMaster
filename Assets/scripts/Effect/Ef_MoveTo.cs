using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_MoveTo : MonoBehaviour
{
    private GameObject goldCollet;

    void Start(){
        goldCollet = GameObject.Find("GoldCollect");
    }

    void Update(){
        transform.position = Vector3.MoveTowards(transform.position, goldCollet.transform.position, 15 * Time.deltaTime);
    }
}
