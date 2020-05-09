using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttr : MonoBehaviour
{
    public int maxNum;
    public int maxSpeed;
    public int exp;
    public int gold;
    public int hp;

    public GameObject diePrefab;
    public GameObject goldPrefab;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Border"){
            Destroy(gameObject);
        }
    } 

    void TakeDamage(int value){
        hp -= value;
        if(hp <= 0){
            CameManager.Instance.gold += gold;
            print(gold);
            print(CameManager.Instance.gold);
            CameManager.Instance.exp += exp;
            GameObject die =  Instantiate(diePrefab);      
            die.transform.SetParent(gameObject.transform.parent, false);
            die.transform.position = transform.position;
            die.transform.rotation = transform.rotation;

            GameObject goldGO =  Instantiate(goldPrefab);      
            goldGO.transform.SetParent(gameObject.transform.parent, false);
            goldGO.transform.position = transform.position;
            goldGO.transform.rotation = transform.rotation;   

            if(gameObject.GetComponent<Ef_PlayEffect>() != null){
                AudioManager.Instance.PlayEffectSound(AudioManager.Instance.rewardClip);
                gameObject.GetComponent<Ef_PlayEffect>().PlayEffect();
            }
            Destroy(gameObject); 
        }
    }
}
