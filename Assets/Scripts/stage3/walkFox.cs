using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class walkFox : MonoBehaviour
{


    public GameObject foxObj;
    void Awake()
    {

        //  lamamove = GetComponent<lamaMove>();

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        




        // if (coll.gameObject == lamaMove.InstanceNew.gameObject)
        if (coll.gameObject.tag == "Fox")
        {
            foxObj = coll.gameObject;
            StartCoroutine(Tagging());
            foxObj.GetComponent<FoxMove>()?.Tired();

        }
    }
   

    public IEnumerator Tagging()
    {
        
        this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false; //
        this.gameObject.tag = "TreesQuit";
        yield return new WaitForSeconds(15f);
        this.gameObject.tag = "Trees";
        this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
    
}