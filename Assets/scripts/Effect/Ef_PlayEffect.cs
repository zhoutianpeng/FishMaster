using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_PlayEffect : MonoBehaviour
{
    public GameObject[] effectPrefabs;
    public void PlayEffect(){
        foreach(GameObject item in effectPrefabs){
            Instantiate(item);
        }
    }
}
