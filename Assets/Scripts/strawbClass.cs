using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class strawbClass : MonoBehaviour
{
    public bool eatable = true;
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
        infoClassText.text = "ЭТО: СПЕЛЫЙ ПЛОД";
        infoThisCoordinatesText.text = "X:" + this.gameObject.transform.position.x + " Y:" + this.gameObject.transform.position.y + " Z:" + this.gameObject.transform.position.z;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
      
        // if (coll.gameObject == lamaMove.InstanceNew.gameObject)
        if (coll.gameObject.tag == "Lamster")
        {
            if (eatable == true)
            {
                lamaObj = coll.gameObject;
                lamaObj.GetComponent<lamaMove>()?.Eat();

                lamaObj.GetComponent<Pathfinding.AIPath>().enabled = false; //


                
                Destroy(this.gameObject);
            }
            
          

        }
        if (coll.gameObject.tag == "Frog")
        {
            if (eatable == true)
            {
                frogObj = coll.gameObject;
                frogObj.GetComponent<FrogMove>()?.Eat();

              



                Destroy(this.gameObject);
            }



        }
        if (coll.gameObject.tag == "Chicken")
        {
            if (eatable == true)
            {
                chickenObj = coll.gameObject;
                chickenObj.GetComponent<lamaMove>()?.Eat();

              



                Destroy(this.gameObject);
            }



        }
        if (coll.gameObject.tag == "Fox")
        {
            if (eatable == true)
            {
                foxObj = coll.gameObject;

                if (runFoxCoroutine == false)
                {
                    StartCoroutine(FoxCollision());
                }



            }



        }
        if (coll.gameObject.tag == "Bear")
        {
            if (eatable == true)
            {
                bearObj = coll.gameObject;
                bearObj.GetComponent<BearMove>()?.Eat();

                Destroy(this.gameObject);


            }



        }

        if (coll.gameObject.tag == "Boar")
        {
            if (eatable == true)
            {
                boarObj = coll.gameObject;
                boarObj.GetComponent<BoarMove>()?.Eat();

                Destroy(this.gameObject);


            }



        }

        if (coll.gameObject.tag == "Human" || coll.gameObject.tag == "HumanFemale" || coll.gameObject.tag == "HumanMale")
        {
            if (coll.gameObject.GetComponent<Backpack>().currentLoot < 10)
            {
                if (eatable == true)
                {
                    humanObj = coll.gameObject;
                    humanObj.GetComponent<Backpack>().countBerry++;

                    Destroy(this.gameObject);


                }



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
