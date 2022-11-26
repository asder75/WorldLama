using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class badstrawbClass : MonoBehaviour
{
    public bool eatable = false;
    private lamaMove lamamove;
    public GameObject lamaObj;
    public GameObject frogObj;
    public GameObject chickenObj;
    public GameObject foxObj;
    public GameObject bearObj;
    public GameObject boarObj;
    public GameObject humanObj;
    public bool runFoxCoroutine = false;

    public Text infoClassText;
    public Text infoThisCoordinatesText;

    void Awake()
    {

        //  lamamove = GetComponent<lamaMove>();

    }
    void FixedUpdate()
    {
        infoClassText.text = "ЭТО: ЯДОВИТЫЙ ПЛОД";
        infoThisCoordinatesText.text = "X:" + this.gameObject.transform.position.x + " Y:" + this.gameObject.transform.position.y + " Z:" + this.gameObject.transform.position.z;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {

        // if (coll.gameObject == lamaMove.InstanceNew.gameObject)
        if (coll.gameObject.tag == "Lamster")
        {
            lamaObj = coll.gameObject;
            lamaObj.GetComponent<lamaMove>()?.GoDie();




            Destroy(this.gameObject);



        }
        if (coll.gameObject.tag == "Frog")
        {
           
                frogObj = coll.gameObject;
                frogObj.GetComponent<FrogMove>()?.GoDie();





                Destroy(this.gameObject);
            



        }
        if (coll.gameObject.tag == "Chicken")
        {
            
                chickenObj = coll.gameObject;
                chickenObj.GetComponent<lamaMove>()?.GoDie();





                Destroy(this.gameObject);
           



        }
        if (coll.gameObject.tag == "Fox")
        {
            
                foxObj = coll.gameObject;

                if (runFoxCoroutine == false)
                {
                    StartCoroutine(FoxCollision());
                }



            



        }
        if (coll.gameObject.tag == "Bear")
        {
            
                bearObj = coll.gameObject;
                bearObj.GetComponent<BearMove>()?.GoDie();

                Destroy(this.gameObject);


            



        }
        if (coll.gameObject.tag == "Boar")
        {

            boarObj = coll.gameObject;
            boarObj.GetComponent<BoarMove>()?.GoDie();

            Destroy(this.gameObject);






        }
        if (coll.gameObject.tag == "Human" || coll.gameObject.tag == "HumanFemale" || coll.gameObject.tag == "HumanMale")
        {
            if (coll.gameObject.GetComponent<Backpack>().currentLoot < 10)
            {
                humanObj = coll.gameObject;
                humanObj.GetComponent<Backpack>().countPoisonBerry++;

                Destroy(this.gameObject);
            }
            else
            {
                if (runFoxCoroutine == false)
                {
                    StartCoroutine(FoxCollision());
                }
            }






        }

    }
    private IEnumerator FoxCollision()
    {
        runFoxCoroutine = true;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2.5f);
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        runFoxCoroutine = false;
    }
}

