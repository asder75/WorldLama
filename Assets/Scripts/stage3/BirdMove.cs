using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdMove : Animals
{
    public Text hpText;
    public Text eatText;


    private float coordinateX;
    private float coordinateXTwo;

    private SpriteRenderer spriteRenderer;

    //public GameObject foxObj;
    public bool isAttacking = false;

    public float frogCordinX;
    public float frogCordinY;

    private Rigidbody2D physic;

    public bool starthunt = false;

    public Transform target;
    public GameObject[] birdFoods;
    GameObject closest; //new

    public string nearest;  //new

    //размножение
    public GameObject lovePictObj;
    public Transform targetLove;
    public GameObject[] loveObjects;
    public GameObject closestLove;
    public GameObject bird;
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
        food = 75;
        maxfood = 75;
        hp = 75;
        hungerCheck = 0;
        runningRightOrLeft = 0;
        runningUpOrDown = 0;
        speed = 2f;
        readyLove = false;
        readyCoroutineLove = false;
        readyCoroutineGrow = false;
        thisIsClass = "Кондор, хищник";
        runCoroutineRunForFood = false;
        defaultTag = "Bird";

        physic = GetComponent<Rigidbody2D>();

        birdFoods = GameObject.FindGameObjectsWithTag("Frog");



        //размножение
  
        genderBoy = UnityEngine.Random.Range(0, 2);
        timeSecToFirstLove = UnityEngine.Random.Range(4, 10);


        //размножение


    }
    GameObject FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in birdFoods)
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
    private void FixedUpdate()
    {
        infoClassText.text = "ЭТО: " + thisIsClass;
        infoFoodText.text = "СЫТОСТЬ: " + food + "/" + maxfood;
        infoEatText.text = "ЗДОРОВЬЕ " + hp + "/" + maxfood;
        infoThisCoordinatesText.text = "X:" + this.gameObject.transform.position.x + " Y:" + this.gameObject.transform.position.y + " Z:" + this.gameObject.transform.position.z;

        if (target != null)
        {
            target.GetComponent<FrogMove>()?.Attack();
            frogCordinX = target.transform.position.x;
            frogCordinY = target.transform.position.y;
        }

        //размножение
        if (food >= 26)
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

        //размножение

        if (runCoroutine == false)
        {
            StartCoroutine(Timing());
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
        StartCoroutine(RunRightOrLeft());
        //new
        if (food <= 15)
        {
            StartCoroutine(RunRightOrLeft());
            if (hungerCheck == 0)
            {
                StartCoroutine(Starve());
            }
            runningRightOrLeft = 1;
            //this.GetComponent<ChickenFollow>().enabled = true;
            this.GetComponent<CircleCollider2D>().enabled = true; //
        }
        else
        {
            if (isAttacking == false)
            {
                if (readyLove == false)
                {
                    this.GetComponent<CircleCollider2D>().enabled = false;
                }
                else
                {
                    this.GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else
            {
                this.GetComponent<CircleCollider2D>().enabled = true;
            }
        }

        if (hp <= 0)
        {
            GoDie();
        }

        if (food <= 0)
        {

           // this.GetComponent<ChickenFollow>().enabled = true;
        }
        else
        {
            if (food > 15)
            {
               // this.GetComponent<ChickenFollow>().enabled = false;
            }
        }

        if (food < 25)
        {
            runningRightOrLeft = 1;
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
    private void Update()
    {
        Vector3 dir = transform.right * 1;
        Vector3 dirTwo = transform.right * -1;
        Vector3 moveInput = transform.up * 1;
        Vector3 moveInputTwo = transform.up * -1;

        //размножение
        if (readyCoroutineLove == false)
        {
            if (food >= 26)
            {
                StartCoroutine(FallInLoveColdown());
            }
            else
            {
                StopCoroutine(FallInLoveColdown());
                readyLove = false;
            }
        }

        if (this.gameObject.tag == "BirdMale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("BirdFemale");
        }
        if (this.gameObject.tag == "BirdFemale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("BirdMale");
        }

        SetTagByGender();  //дать себе тэг по полу

        if (readyLove == true)
        {
            if (this.gameObject.tag == "BirdMale" || this.gameObject.tag == "BirdFemale")
            {
                if (FindClosestLove() != null)
                {
                    targetLove = FindClosestLove().transform;
                }
            }
        }


        //размножение

        if (readyLove == false)//размножение
        {
            SetMotionVectors();

            if (timerActive == true)
            {
                if (runningToTheRight == true)
                {

                    transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

                    spriteRenderer.flipX = true;
                }
                if (runningToTheLeft == true)
                {

                    transform.position = Vector3.MoveTowards(transform.position, transform.position + dirTwo, speed * Time.deltaTime);

                    spriteRenderer.flipX = false;
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


       
        birdFoods = GameObject.FindGameObjectsWithTag("Frog");
        nearest = FindClosestEnemy().name;
        target = FindClosestEnemy().transform;



        if (starthunt == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
       




    }

    private IEnumerator Timing()
    {
        runCoroutine = true;

        runningRightOrLeft = 2;
        runningUpOrDown = 3;
        timerActive = true;
        timer = 5f;
        yield return new WaitForSeconds(timer);
        food = food - UnityEngine.Random.Range(2, 7);
        runningRightOrLeft = 2;
        runningUpOrDown = 1;
        timer = 5f;
        yield return new WaitForSeconds(timer);
        food = food - UnityEngine.Random.Range(2, 7);
        runningRightOrLeft = 2;
        runningUpOrDown = 2;
        timer = 5f;
        yield return new WaitForSeconds(timer);
        food = food - UnityEngine.Random.Range(2, 7);
        runningRightOrLeft = 4;
        runningToTheRight = false;
        runningToTheLeft = false;
        runningUp = true;
        runningDown = false;
        timer = 5f;
        yield return new WaitForSeconds(timer);
        food = food - UnityEngine.Random.Range(2, 7);
        runningRightOrLeft = 3;
        runningUpOrDown = 2;
        timer = 5f;
        yield return new WaitForSeconds(timer);
        food = food - UnityEngine.Random.Range(2, 7);
        runningRightOrLeft = 3;
        runningUpOrDown = 3;
        timer = 5f;
        yield return new WaitForSeconds(timer);
        food = food - UnityEngine.Random.Range(2, 7);

        timerActive = false;
        runCoroutine = false;


    }
   
    public void Attack()
    {
        isAttacking = true;
    }

    

    public override IEnumerator RunRightOrLeft()
    {
        coordinateX = this.gameObject.transform.position.x;
        yield return new WaitForSeconds(0.5f);
        coordinateXTwo = this.gameObject.transform.position.x;

    }


    void OnCollisionEnter2D(Collision2D coll)
    {
  

        if (this.gameObject.tag == "BirdMale")
        {
            if (coll.gameObject.tag == "BirdFemale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;

                this.gameObject.tag = "Bird";
                coll.gameObject.tag = "Bird";
                coll.gameObject.GetComponent<BirdMove>().readyLove = false;

                Instantiate(bird, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);
            }
        }
        if (this.gameObject.tag == "BirdFemale")
        {
            if (coll.gameObject.tag == "BirdMale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;
                this.gameObject.tag = "Bird";
                coll.gameObject.tag = "Bird";
                coll.gameObject.GetComponent<BirdMove>().readyLove = false;
                Instantiate(bird, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);

            }
        }
    }

    //размножение



}