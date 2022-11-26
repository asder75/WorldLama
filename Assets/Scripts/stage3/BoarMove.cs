using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoarMove : Animals
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

    public Transform targetGrass;

    public GameObject[] bearGrass;
    public GameObject closestGrass;

    GameObject closest; //new

    public string nearest;  //new

    private SpriteRenderer spriteRenderer;

    public float chikenCordinX;
    public float chikenCordinY;

    public float grassCordinX;
    public float grassCordinY;

    private float coordinateX;
    private float coordinateXTwo;

    public bool starthunt = false;
    public bool runTired = false;

    public bool grassRespawn = false;

    //размножение
    public GameObject lovePictObj;
    public Transform targetLove;
    public GameObject[] loveObjects;
    public GameObject closestLove;
    public GameObject boar;
    //размножение

    public Text infoClassText;
    public Text infoFoodText;
    public Text infoEatText;
    public Text infoThisCoordinatesText;

    public GameObject sleepSymbol;

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
        food = 80;
        maxfood = 80;
        hp = 80;
        hungerCheck = 0;
        runningRightOrLeft = 0;
        runningUpOrDown = 0;
        speed = 2.2f;
        timer = 0f;
        timerActive = false;
        runCoroutine = false;
        readyLove = false;
        readyCoroutineLove = false;
        readyCoroutineGrow = false;
        thisIsClass = "Кабан, всеядное";
        runCoroutineRunForFood = false;
        defaultTag = "Boar";


        grassi = GameObject.FindGameObjectsWithTag("Stonebear");

        physic = GetComponent<Rigidbody2D>();

        foxFoods = GameObject.FindGameObjectsWithTag("Frog");

        bearGrass = GameObject.FindGameObjectsWithTag("Food");

        //размножение

        genderBoy = UnityEngine.Random.Range(0, 2);
        timeSecToFirstLove = UnityEngine.Random.Range(4, 10);


        //размножение


        StartCoroutine(GrassRespawnCoroutine());
    }
    GameObject FindClosestEnemy()
    {
    
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in grassi)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
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
    GameObject FindClosestBearGrass()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in bearGrass)
        {
            if (go != null)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestGrass = go;
                    distance = curDistance;
                }
            }
        }
        return closestGrass;
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
        grassi = GameObject.FindGameObjectsWithTag("Stonebear");

        foxFoods = GameObject.FindGameObjectsWithTag("Frog");
        nearest = FindClosestEnemy().name;
        target = FindClosestEnemy().transform;
        if (grassRespawn == true)
        {
            bearGrass = GameObject.FindGameObjectsWithTag("Food");
            targetGrass = FindClosestBearGrass().transform;
        }


        targetFood = FindClosestFood().transform;


        
            Vector3 dir = transform.right * 1;
            Vector3 dirTwo = transform.right * -1;
            Vector3 moveInput = transform.up * 1;
            Vector3 moveInputTwo = transform.up * -1;

        if (WinterScript.timeSummer == true)
        {
            if (readyLove == false)
            {
                SetMotionVectors();

                if (timerActive == true)
                {
                    if (runningToTheRight == true)
                    {

                        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

                        //spriteRenderer.flipX = true;
                    }
                    if (runningToTheLeft == true)
                    {

                        transform.position = Vector3.MoveTowards(transform.position, transform.position + dirTwo, speed * Time.deltaTime);


                        //spriteRenderer.flipX = false;
                    }

                    if (runningUp == true)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveInput, speed * Time.deltaTime);


                    }
                    if (runningDown == true)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveInputTwo, speed * Time.deltaTime);

                    }
                }
            }
            else
            {
                if (targetLove != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetLove.position + moveInputTwo, speed * Time.deltaTime);
                }
            }
            sleepSymbol.SetActive(false);
        }
        else
        {
            lovePictObj.SetActive(false);
            food = maxfood;
            sleepSymbol.SetActive(true);
        }


        if (starthunt == false)
        {
            if (runCoroutine == false)
            {
                StartCoroutine(Timing());
            }
        }
        else
        {
            StopCoroutine(Timing());
            if (targetFood.transform.position.y > targetGrass.transform.position.y)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetFood.position, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, targetGrass.position, speed * Time.deltaTime);

            }
        }

        //размножение
        if (readyCoroutineLove == false)
        {
            if (food >= 36)
            {
                StartCoroutine(FallInLoveColdown());
            }
            else
            {
                StopCoroutine(FallInLoveColdown());
                readyLove = false;
            }
        }



     


        





    }
    void FixedUpdate()
    {
        infoClassText.text = "ЭТО: " + thisIsClass;
        infoFoodText.text = "СЫТОСТЬ: " + food + "/" + maxfood;
        infoEatText.text = "ЗДОРОВЬЕ " + hp + "/" + maxfood;
        infoThisCoordinatesText.text = "X:" + this.gameObject.transform.position.x + " Y:" + this.gameObject.transform.position.y + " Z:" + this.gameObject.transform.position.z;


        if (this.gameObject.tag == "BoarMale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("BoarFemale");
        }
        if (this.gameObject.tag == "BoarFemale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("BoarMale");
        }


        if (targetFood != null)
        {
            targetFood.GetComponent<FrogMove>()?.Attack();
            chikenCordinX = targetFood.transform.position.x;
            chikenCordinY = targetFood.transform.position.y;
        }
        if (targetGrass != null)
        {
            // targetGrass.GetComponent<walkLama>()?.Attack();
            grassCordinX = targetGrass.transform.position.x;
            grassCordinY = targetGrass.transform.position.y;
        }

        StartCoroutine(Tired());

        if (food >= 36)
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

        SetTagByGender();  //дать себе тэг по полу

        if (readyLove == true)
        {
            if (this.gameObject.tag == "BoarMale" || this.gameObject.tag == "BoarFemale")
            {
                if (FindClosestLove() != null)
                {
                    targetLove = FindClosestLove().transform;
                }
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


        if (food < 35)
        {
            starthunt = true;
            if (runCoroutineRunForFood == false)
            {
                StartCoroutine(RunForFood());
            }
        }
        else
        {
            starthunt = false;
        }


    }
   
    private IEnumerator Tired()
    {
        if (runTired == false)
        {
            runTired = true;
            food = food - UnityEngine.Random.Range(2, 6);
            yield return new WaitForSeconds(4f);
            runTired = false;
        }
    }

    public override IEnumerator RunRightOrLeft()
    {
        coordinateX = this.gameObject.transform.position.x;
        yield return new WaitForSeconds(1f);
        coordinateXTwo = this.gameObject.transform.position.x;

    }
    private IEnumerator Timing()
    {
        runCoroutine = true;

        runningRightOrLeft = UnityEngine.Random.Range(2, 4);
        runningUpOrDown = UnityEngine.Random.Range(1, 4);
        timerActive = true;
        timer = UnityEngine.Random.Range(0.1f, 1.5f);
        yield return new WaitForSeconds(timer);


        

        timerActive = false;
        runCoroutine = false;


    }
    private IEnumerator GrassRespawnCoroutine()
    {
        yield return new WaitForSeconds(12f);
        grassRespawn = true;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {


        if (this.gameObject.tag == "BoarMale")
        {
            if (coll.gameObject.tag == "BoarFemale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;

                this.gameObject.tag = "Boar";
                coll.gameObject.tag = "Boar";
                coll.gameObject.GetComponent<BoarMove>().readyLove = false;

                Instantiate(boar, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);
            }
        }
        if (this.gameObject.tag == "BoarFemale")
        {
            if (coll.gameObject.tag == "BoarMale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;
                this.gameObject.tag = "Boar";
                coll.gameObject.tag = "Boar";
                coll.gameObject.GetComponent<BoarMove>().readyLove = false;
                Instantiate(boar, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);

            }
        }
    }

}
