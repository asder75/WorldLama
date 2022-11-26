using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxMove : Animals
{
    public Text hpText;
    public Text eatText;

    //follow
    private Rigidbody2D physic;

    public Transform target;
    public Transform targetFood;
   

    public GameObject[] grassi;

    public GameObject[] foxFoods;
    public GameObject closestFood;


    GameObject closest; //new

    public string nearest;  //new

    private SpriteRenderer spriteRenderer;

    public float chikenCordinX;
    public float chikenCordinY;

    private float coordinateX;
    private float coordinateXTwo;

    public bool starthunt = false;

    //размножение
    public GameObject lovePictObj;
    public Transform targetLove;
    public GameObject[] loveObjects;
    public GameObject closestLove;
    public GameObject fox;
    //размножение

    public Text infoClassText;
    public Text infoFoodText;
    public Text infoEatText;
    public Text infoThisCoordinatesText;
    private void Awake()
    {

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }
    private void Start()
    {
        runningToTheRight = false;
        runningToTheLeft = false;
        runningUp = false;
        runningDown = false;
        food = 100;
        maxfood = 100;
        hp = 100;
        hungerCheck = 0;
        runningRightOrLeft = 0;
        runningUpOrDown = 0;
        timer = 0f;
        timerActive = false;
        runCoroutine = false;
        readyLove = false;
        readyCoroutineLove = false;
        readyCoroutineGrow = false;
        thisIsClass = "Лиса, хищник";
        runCoroutineRunForFood = false;
        defaultTag = "Fox";

        grassi = GameObject.FindGameObjectsWithTag("Trees");
      
        physic = GetComponent<Rigidbody2D>();

        foxFoods = GameObject.FindGameObjectsWithTag("Chicken");

        //размножение

        genderBoy = UnityEngine.Random.Range(0, 2);
        timeSecToFirstLove = UnityEngine.Random.Range(4, 10);


        //размножение

        speed = UnityEngine.Random.Range(1.5f, 2.1f);

    }
    GameObject FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in grassi)
        {
            if (go != null)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }
    GameObject FindClosestFood()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in foxFoods)
        {
            if (go != null)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestFood = go;
                    distance = curDistance;
                }
            }
        }
        return closestFood;
    }
    GameObject FindClosestLove()
    {
        
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in loveObjects)
            {
            if (go != null)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestLove = go;
                    distance = curDistance;
                }
            }
            }
        
        return closestLove;
    }
    void Update()
    {
        grassi = GameObject.FindGameObjectsWithTag("Trees");
        foxFoods = GameObject.FindGameObjectsWithTag("Chicken");
        
        nearest = FindClosestEnemy().name;
        target = FindClosestEnemy().transform;

        
        targetFood = FindClosestFood().transform;


        
            if (starthunt == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
           
        }
            else
            {
            {
                if (readyLove == false)//размножение
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetFood.position, speed * Time.deltaTime);
                    
                }
                else
                {
                    if (targetLove != null)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, targetLove.position, speed * Time.deltaTime);
                    }
                }
            }
        }
      

        //размножение
        if (readyCoroutineLove == false)
        {
            if (food >= 31)
            {
                StartCoroutine(FallInLoveColdown());
            }
            else
            {
                StopCoroutine(FallInLoveColdown());
                readyLove = false;
            }
        }

        if (this.gameObject.tag == "FoxMale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("FoxFemale");
        }
        if (this.gameObject.tag == "FoxFemale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("FoxMale");
        }

        SetTagByGender();  //дать себе тэг по полу

        if (readyLove == true)
        {
            if (this.gameObject.tag == "FoxMale" || this.gameObject.tag == "FoxFemale")
            {
                if (FindClosestLove() != null)
                {
                    targetLove = FindClosestLove().transform;
                }
            }
        }


        //размножение

    }
    void FixedUpdate()
    {
        infoClassText.text = "ЭТО: " + thisIsClass;
        infoFoodText.text = "СЫТОСТЬ: " + food + "/" + maxfood;
        infoEatText.text = "ЗДОРОВЬЕ " + hp + "/" + maxfood;
        infoThisCoordinatesText.text = "X:" + this.gameObject.transform.position.x + " Y:" + this.gameObject.transform.position.y + " Z:" + this.gameObject.transform.position.z;

        if (targetFood != null)
        {
            targetFood.GetComponent<ChickenMove>()?.Attack();
            chikenCordinX = targetFood.transform.position.x;
            chikenCordinY = targetFood.transform.position.y;
        }

        if (food >= 31)
        {

            if (readyLove == true)
            {
                lovePictObj.SetActive(true);
            }
            else
            {
                lovePictObj.SetActive(false);
            }

        }




        StartCoroutine(RunRightOrLeft());
        if (coordinateX > coordinateXTwo) //движеится ли вправо
        {
            spriteRenderer.flipX = true;
        }
        if (coordinateX < coordinateXTwo) //движеится ли вправо
        {
            spriteRenderer.flipX = false;
        }

        hpText.text = "" + hp;
        if (food > 0)
        {
            eatText.text = "" + food;
        }
        else
        {
            food = 0;
            eatText.text = "" + "0";
        }


        if (food <= 0)
        {
            if (hungerCheck == 0)
            {
                StartCoroutine(Starve());
            }
        }
        if (hp <= 0)
        {
            GoDie();
        }


        if(food < 30)
        {
            starthunt = true;
            if(runCoroutineRunForFood == false)
            {
                StartCoroutine(RunForFood());
            }
        }
        else
        {
            starthunt = false;
        }


    }
   
    public void Tired()
    {
        food = food - UnityEngine.Random.Range(4, 13);
    }

    public override IEnumerator RunRightOrLeft()
    {
        coordinateX = this.gameObject.transform.position.x;
        yield return new WaitForSeconds(0.1f);
        coordinateXTwo = this.gameObject.transform.position.x;

    }

    void OnCollisionEnter2D(Collision2D coll)
    {


        if (this.gameObject.tag == "FoxMale")
        {
            if (coll.gameObject.tag == "FoxFemale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;

                this.gameObject.tag = "Fox";
                coll.gameObject.tag = "Fox";
                coll.gameObject.GetComponent<FoxMove>().readyLove = false;

                Instantiate(fox, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);
            }
        }
        if (this.gameObject.tag == "FoxFemale")
        {
            if (coll.gameObject.tag == "FoxMale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;
                this.gameObject.tag = "Fox";
                coll.gameObject.tag = "Fox";
                coll.gameObject.GetComponent<FoxMove>().readyLove = false;
                Instantiate(fox, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);

            }
        }
    }
   


}
