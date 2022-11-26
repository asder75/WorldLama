using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TigerMove : Animals
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
    public GameObject[] tigerFoods;
    GameObject closest; //new

    public string nearest;  //new

    //размножение
    public GameObject lovePictObj;
    public Transform targetLove;
    public GameObject[] loveObjects;
    public GameObject closestLove;
    public GameObject tiger;
    //размножение

    public Text infoClassText;
    public Text infoFoodText;
    public Text infoEatText;
    public Text infoThisCoordinatesText;

    public bool runCapeCoroutine = false;
    public GameObject closestHuman;
    public Transform targetHuman;
    public bool waitFoodStart = false;
    public bool fakeCollisionStart = false;
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
        food = 90;
        maxfood = 90;
        hp = 90;
        hungerCheck = 0;
        runningRightOrLeft = 0;
        runningUpOrDown = 0;
        speed = 2f;
        timer = 0f;
        timerActive = false;
        runCoroutine = false;
        readyLove = false;
        readyCoroutineLove = false;
        readyCoroutineGrow = false;
        runCoroutineRunForFood = false;
        thisIsClass = "Тигр, хищник";
        helmOfDominator = false;
        defaultTag = "Tiger";

        physic = GetComponent<Rigidbody2D>();

        tigerFoods = GameObject.FindGameObjectsWithTag("Lamster");


        //размножение

        genderBoy = UnityEngine.Random.Range(0, 2);
        timeSecToFirstLove = UnityEngine.Random.Range(4, 10);


        //размножение



    }
    GameObject FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in tigerFoods)
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

        if (helmOfDominator == true)
        {
            targetHuman = closestHuman.transform;

            if (runCapeCoroutine == false)
            {
                StartCoroutine(GiveCapeCoroutine());
            }
            if (closestHuman.GetComponent<Backpack>().tigerCap == true)
            {
                closestHuman.GetComponent<HumanMove>().capetigerobject.SetActive(true);
            }
            else
            {
                closestHuman.GetComponent<HumanMove>().capetigerobject.SetActive(false);
            }
        }
        //размножение
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

        //размножение

        StartCoroutine(RunRightOrLeft());
        if (target != null)
        {
            target.GetComponent<lamaMove>()?.Attack();
            frogCordinX = target.transform.position.x;
            frogCordinY = target.transform.position.y;
        }

        if(isAttacking == true)
        {
            speed = 5f;
        }

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
            this.GetComponent<PolygonCollider2D>().enabled = true; //
        }
        else
        {
            if (isAttacking == false)
            {
                if (readyLove == false)
                {
                    if (fakeCollisionStart == false)
                    {
                        this.GetComponent<PolygonCollider2D>().enabled = true; //false
                    }
                    else
                    {
                        this.GetComponent<PolygonCollider2D>().enabled = false;
                    }
                }
                else
                {
                    this.GetComponent<PolygonCollider2D>().enabled = true;
                }
            }
            else
            {
                if (fakeCollisionStart == false)
                {
                    this.GetComponent<PolygonCollider2D>().enabled = true;
                }
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

        if (this.gameObject.tag == "TigerMale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("TigerFemale");
        }
        if (this.gameObject.tag == "TigerFemale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("TigerMale");
        }

        SetTagByGender();  //дать себе тэг по полу

        if (readyLove == true)
        {
            if (this.gameObject.tag == "TigerMale" || this.gameObject.tag == "TigerFemale")
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



        tigerFoods = GameObject.FindGameObjectsWithTag("Lamster");
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

        speed = UnityEngine.Random.Range(1f, 3f);

        runningRightOrLeft = UnityEngine.Random.Range(2, 4);
        runningUpOrDown = UnityEngine.Random.Range(1, 4);
        timerActive = true;
        timer = UnityEngine.Random.Range(2f, 5f);
        yield return new WaitForSeconds(timer);

        food = food - UnityEngine.Random.Range(1, 3);

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
    private IEnumerator GiveCapeCoroutine()
    {
        if (helmOfDominator == true)
        {
            runCapeCoroutine = true;
            yield return new WaitForSeconds(30f);
            if (helmOfDominator == true)
            {
                closestHuman.GetComponent<Backpack>().tigerCap = true;
            }
            runCapeCoroutine = false;
        }
    }
    private IEnumerator WaitFood()
    {
        waitFoodStart = true;
        yield return new WaitForSeconds(10f);
        if (closestHuman.GetComponent<Backpack>().countMeet == 0 )
        {
            helmOfDominator = false;
            closestHuman.GetComponent<HumanMove>().summonTiger--;
            targetHuman = null;
            closestHuman = null;
        }

        
        if (food != maxfood)
        {
            if (closestHuman.GetComponent<Backpack>().countMeet > 0)
            {
                closestHuman.GetComponent<Backpack>().countMeet--;
                food = maxfood;
            }
        }
        waitFoodStart = false;
    }
    private IEnumerator FakeCollision()
    {
        fakeCollisionStart = true;
        this.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        fakeCollisionStart = false;

    }


    void OnCollisionEnter2D(Collision2D coll)
    {


        if (this.gameObject.tag == "TigerMale")
        {
            if (coll.gameObject.tag == "TigerFemale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;

                this.gameObject.tag = "Tiger";
                coll.gameObject.tag = "Tiger";
                coll.gameObject.GetComponent<TigerMove>().readyLove = false;

                Instantiate(tiger, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);
            }
        }
        if (this.gameObject.tag == "TigerFemale")
        {
            if (coll.gameObject.tag == "TigerMale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;
                this.gameObject.tag = "Tiger";
                coll.gameObject.tag = "Tiger";
                coll.gameObject.GetComponent<TigerMove>().readyLove = false;
                Instantiate(tiger, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);

            }
        }

        if (coll.gameObject.tag == "Human")
        {

            if (helmOfDominator == false)
            {
                if (coll.gameObject.GetComponent<Backpack>().countMeet > 0)
                {
                    coll.gameObject.GetComponent<Backpack>().countMeet--;
                    closestHuman = coll.gameObject;
                    coll.gameObject.GetComponent<HumanMove>().summonTiger++;
                    Debug.Log("HELMOFDOMINATORYES");
                    helmOfDominator = true;
                }
               

            }
            if (food <= 25)
            {
                if (helmOfDominator == true)
                {
                    if (closestHuman.GetComponent<Backpack>().countMeet == 0)
                    {
                        if (waitFoodStart == false)
                        {
                            StartCoroutine(WaitFood());
                        }
                    }

                    if (closestHuman.GetComponent<Backpack>().countMeet > 0)
                    {
                        closestHuman.GetComponent<Backpack>().countMeet--;
                        food = maxfood;
                    }
                   
                }
            }
        }

        if (fakeCollisionStart == false)
        {
            StartCoroutine(FakeCollision());
        }
    }

}