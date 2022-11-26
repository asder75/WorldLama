using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class walkLama : MonoBehaviour
{
    private lamaMove lamamove;
    public GameObject lamaObj;
    public GameObject frogObj;
    public GameObject chickenObj;
    public GameObject foxObj;
    public GameObject bearObj;
    public GameObject boarObj;
    public GameObject humanObj;
    public bool runFoxCoroutine = false;

    public bool eatableNewRepeat = false;
    void Awake()
    {
       
      //  lamamove = GetComponent<lamaMove>();

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject go = this.gameObject;
        grassClass eatable = go.GetComponent<grassClass>();
        bool eatableNew = eatable.eatable;

     if(eatable.eatable == true)
        {
            eatableNewRepeat = true;
        }
        else
        {
            eatableNewRepeat = false;
        }


        // if (coll.gameObject == lamaMove.InstanceNew.gameObject)
        if (coll.gameObject.tag == "Lamster")
        {
            //для травы
            if (eatableNew == true)
            {
                lamaObj = coll.gameObject;
                lamaObj.GetComponent<lamaMove>()?.Eat();

                lamaObj.GetComponent<Pathfinding.AIPath>().enabled = false; //


                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
            if (eatableNew == false)
            {
                lamaObj = coll.gameObject;
                lamaObj.GetComponent<lamaMove>()?.GoDie();

             

                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
           
        }

        if (coll.gameObject.tag == "Frog")
        {
            //для травы
            if (eatableNew == true)
            {
                frogObj = coll.gameObject;
                frogObj.GetComponent<FrogMove>()?.Eat();



                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
            if (eatableNew == false)
            {
                frogObj = coll.gameObject;
                frogObj.GetComponent<FrogMove>()?.GoDie();



                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }

        }

        if (coll.gameObject.tag == "Chicken")
        {
            //для травы
            if (eatableNew == true)
            {
                chickenObj = coll.gameObject;
                chickenObj.GetComponent<ChickenMove>()?.Eat();



                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
            if (eatableNew == false)
            {
                chickenObj = coll.gameObject;
                chickenObj.GetComponent<ChickenMove>()?.GoDie();



                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }

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
            //для травы
            if (eatableNew == true)
            {
                bearObj = coll.gameObject;
                bearObj.GetComponent<BearMove>()?.Eat();



                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
            if (eatableNew == false)
            {
                bearObj = coll.gameObject;
                bearObj.GetComponent<BearMove>()?.GoDie();



                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }

        }

        if (coll.gameObject.tag == "Boar")
        {
            //для травы
            if (eatableNew == true)
            {
                boarObj = coll.gameObject;
                boarObj.GetComponent<BoarMove>()?.Eat();



                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
            if (eatableNew == false)
            {
                boarObj = coll.gameObject;
                boarObj.GetComponent<BoarMove>()?.GoDie();



                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }

        }

        if (coll.gameObject.tag == "Human" || coll.gameObject.tag == "HumanFemale" || coll.gameObject.tag == "HumanMale")
        {
            if (coll.gameObject.GetComponent<Backpack>().currentLoot < 10)
            {
                //для травы
                if (eatableNew == true)
                {
                    humanObj = coll.gameObject;
                    humanObj.GetComponent<Backpack>().countGrass++;



                    this.gameObject.SetActive(false);
                    Destroy(this.gameObject);
                }
                if (eatableNew == false)
                {
                    humanObj = coll.gameObject;
                    humanObj.GetComponent<Backpack>().countPoisonGrass++;



                    this.gameObject.SetActive(false);
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
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(2.5f);
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        runFoxCoroutine = false;
    }

    
}
