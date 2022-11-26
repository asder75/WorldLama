using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenMove : Animals
{
    public Text hpText;
    public Text eatText;

    private float coordinateX;
    private float coordinateXTwo;

    private SpriteRenderer spriteRenderer;

    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    public GameObject foxObj;
    public GameObject bearObj;
    public bool isAttacking = false;
    public bool fakeCollisionStart = false;
    public bool waitFoodStart = false;
    public bool runEggCoroutine = false;

    //размножение
    public GameObject lovePictObj;
    public Transform targetLove;
    public GameObject[] loveObjects;
    public GameObject closestLove;
    public GameObject chicken;
    //размножение

    public Text infoClassText;
    public Text infoFoodText;
    public Text infoEatText;
    public Text infoThisCoordinatesText;

    //приручение
    public GameObject closestHuman;
    public Transform targetHuman;



    private void Awake()
    {

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();


    }
    private void Start()
    {
        

        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject

        runningToTheRight = false;
        runningToTheLeft = false;
        runningUp = false;
        runningDown = false;
        food = 50;
        maxfood = 50;
        hp = 50;
        hungerCheck = 0;
        runningRightOrLeft = 0;
        runningUpOrDown = 0;
        speed = 1f;
        timer = 0f;
        timerActive = false;
        runCoroutine = false;
        readyLove = false;
        readyCoroutineLove = false;
        readyCoroutineGrow = false;
        thisIsClass = "Курица, травоядное";
        helmOfDominator = false;
        defaultTag = "Chicken";


        //размножение

        genderBoy = UnityEngine.Random.Range(0, 2);
        timeSecToFirstLove = UnityEngine.Random.Range(4, 10);


        //размножение

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

        if(helmOfDominator == true)
        {
            targetHuman = closestHuman.transform;

            if(runEggCoroutine == false)
            {
                StartCoroutine(GiveEggCoroutine());
            }
        }
       

        if (food >= 16)
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

        if (this.gameObject.tag == "ChickenMale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("ChickenFemale");
            if (food < 17)
            {
                food = 17;
            }
        }
        if (this.gameObject.tag == "ChickenFemale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("ChickenMale");
            if (food < 17)
            {
                food = 17;
            }
        }

        //размножение
        if (readyCoroutineLove == false)
        {
            if (food >= 16)
            {
                StartCoroutine(FallInLoveColdown());
            }
            else
            {
                StopCoroutine(FallInLoveColdown());
                readyLove = false;
            }
        }

        SetTagByGender();  //дать себе тэг по полу

        if (readyLove == true)
        {
            if (this.gameObject.tag == "ChickenMale" || this.gameObject.tag == "ChickenFemale")
            {
                if (FindClosestLove() != null)
                {
                    targetLove = FindClosestLove().transform;
                }
            }
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

        //new
        if (food <= 15)
        {
            StartCoroutine(RunRightOrLeft());
            if (hungerCheck == 0)
            {
                StartCoroutine(Starve());
            }
            runningRightOrLeft = 1;
            this.GetComponent<ChickenFollow>().enabled = true;
            this.GetComponent<CapsuleCollider2D>().enabled = true; //
        }
        else
        {
            if (isAttacking == false)
            {
                if (readyLove == false)
                {
                    if (fakeCollisionStart == false)
                    {
                        this.GetComponent<CapsuleCollider2D>().enabled = true; //false
                    }
                    else
                    {
                        this.GetComponent<CapsuleCollider2D>().enabled = false;
                    }
                }
                else
                {

                    this.GetComponent<CapsuleCollider2D>().enabled = true;
                }
            }
            else
            {
                if (fakeCollisionStart == false)
                {
                    this.GetComponent<CapsuleCollider2D>().enabled = true;
                }
            }
        }

        if (hp <= 0)
        {
            GoDie();
        }

        if (food <= 0)
        {

            this.GetComponent<ChickenFollow>().enabled = true;
        }
        else
        {
            if (food > 15)
            {
                this.GetComponent<ChickenFollow>().enabled = false;
            }
        }
    }
    private void Update()
    {
        Vector3 dir = transform.right * 1;
        Vector3 dirTwo = transform.right * -1;
        Vector3 moveInput = transform.up * 1;
        Vector3 moveInputTwo = transform.up * -1;
        if (readyLove == false)//размножение
        {
            if (helmOfDominator == false)
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
                transform.position = Vector3.MoveTowards(transform.position, targetHuman.position + moveInputTwo, speed * Time.deltaTime);
            }
        }
        else
        {
            if (targetLove != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetLove.position + moveInputTwo, speed * Time.deltaTime);
            }
        }

        


        //размножение
    }

    private IEnumerator GiveEggCoroutine()
    {
        if (helmOfDominator == true)
        {
            runEggCoroutine = true;
            yield return new WaitForSeconds(15f);
            if (helmOfDominator == true)
            {
                closestHuman.GetComponent<Backpack>().countEgg++;
            }
            runEggCoroutine = false;
        }
    }    

    private IEnumerator Timing()
    {
        runCoroutine = true;

        runningRightOrLeft = UnityEngine.Random.Range(2, 4);
        runningUpOrDown = UnityEngine.Random.Range(1, 3);
        timerActive = true;
        timer = UnityEngine.Random.Range(2f, 6f);
        yield return new WaitForSeconds(timer);


        GraduallyStarve();

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
    private IEnumerator FakeCollision()
    {
        fakeCollisionStart = true;
        this.GetComponent<CapsuleCollider2D>().enabled = false; 
        yield return new WaitForSeconds(1.5f);
        fakeCollisionStart = false;

    }
    private IEnumerator WaitFood()
    {
        waitFoodStart = true;
        yield return new WaitForSeconds(10f);
        if(closestHuman.GetComponent<Backpack>().countGrass == 0 && closestHuman.GetComponent<Backpack>().countBerry == 0)
        {
            helmOfDominator = false;
            closestHuman.GetComponent<HumanMove>().summonChicken--;
            targetHuman = null;
            closestHuman = null;
        }

        if (closestHuman.GetComponent<Backpack>().countGrass > 0)
        {
            closestHuman.GetComponent<Backpack>().countGrass--;
            food = maxfood;
        }
        if (food != maxfood)
        {
            if (closestHuman.GetComponent<Backpack>().countBerry > 0)
            {
                closestHuman.GetComponent<Backpack>().countBerry--;
                food = maxfood;
            }
        }
        waitFoodStart = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Fox")
        {
            foxObj = coll.gameObject;
            Attack();
            foxObj.GetComponent<FoxMove>()?.Eat();
            Destroy(this.gameObject);

        }

        if (coll.gameObject.tag == "Bear")
        {
            bearObj = coll.gameObject;
            Attack();
            bearObj.GetComponent<BearMove>()?.Eat();
            Destroy(this.gameObject);

        }
        if (this.gameObject.tag == "ChickenMale")
        {
            if (coll.gameObject.tag == "ChickenFemale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;

                this.gameObject.tag = "Chicken";
                coll.gameObject.tag = "Chicken";
                coll.gameObject.GetComponent<ChickenMove>().readyLove = false;

                Instantiate(chicken, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);
            }
        }
        if (this.gameObject.tag == "ChickenFemale")
        {
            if (coll.gameObject.tag == "ChickenMale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;
                this.gameObject.tag = "Chicken";
                coll.gameObject.tag = "Chicken";
                coll.gameObject.GetComponent<ChickenMove>().readyLove = false;
                Instantiate(chicken, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);

            }
        }

        if (coll.gameObject.tag == "Human")
        {
            
            if (helmOfDominator == false)
            {
                if (coll.gameObject.GetComponent<Backpack>().countGrass > 0)
                {
                    coll.gameObject.GetComponent<Backpack>().countGrass--;
                    closestHuman = coll.gameObject;
                    coll.gameObject.GetComponent<HumanMove>().summonChicken++;
                    Debug.Log("HELMOFDOMINATORYES");
                    helmOfDominator = true;
                }
                if (helmOfDominator == false)
                {
                    if (coll.gameObject.GetComponent<Backpack>().countBerry > 0)
                    {
                        coll.gameObject.GetComponent<Backpack>().countBerry--;
                        closestHuman = coll.gameObject;
                        Debug.Log("HELMOFDOMINATORYES");
                        coll.gameObject.GetComponent<HumanMove>().summonChicken++;
                        helmOfDominator = true;
                    }
                }

           }
            if (food <= 25)
            {
                if (helmOfDominator == true)
                {
                    if(closestHuman.GetComponent<Backpack>().countGrass == 0 && closestHuman.GetComponent<Backpack>().countBerry == 0)
                    {
                        if(waitFoodStart == false)
                        {
                            StartCoroutine(WaitFood());
                        }
                    }

                    if (closestHuman.GetComponent<Backpack>().countGrass > 0)
                    {
                        closestHuman.GetComponent<Backpack>().countGrass--;
                        food = maxfood;
                    }
                    if (food != maxfood)
                    {
                        if (closestHuman.GetComponent<Backpack>().countBerry > 0)
                        {
                            closestHuman.GetComponent<Backpack>().countBerry--;
                            food = maxfood;
                        }
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