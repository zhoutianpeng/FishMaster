using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttr : MonoBehaviour
{
    public int speed;
    public int damage;

    public GameObject webPerfab;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Border"){
            Destroy(gameObject);
        }
        if(collision.tag == "Fish")
        {
            GameObject web =  Instantiate(webPerfab);
            web.transform.SetParent(gameObject.transform.parent, false);
            web.transform.position = gameObject.transform.position;
            web.GetComponent<WebAttr>().damage = damage;
            Destroy(gameObject); 
        }
    }

}
