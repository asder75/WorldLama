using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearMove : Animals
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

    public bool runHatCoroutine = false;
    public GameObject closestHuman;
    public Transform targetHuman;
    public bool waitFoodStart = false;
    public bool fakeCollisionStart = false;

    public bool grassRespawn = false;

    //размножение
    public GameObject lovePictObj;
    public Transform targetLove;
    public GameObject[] loveObjects;
    public GameObject closestLove;
    public GameObject bear;
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
        food = 125;
        maxfood = 125;
        hp = 125;
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
        thisIsClass = "Медведь, всеядное";
        runCoroutineRunForFood = false;
        helmOfDominator = false;
        defaultTag = "Bear";


        grassi = GameObject.FindGameObjectsWithTag("Stonebear");

        physic = GetComponent<Rigidbody2D>();

        foxFoods = GameObject.FindGameObjectsWithTag("Chicken");

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

        foxFoods = GameObject.FindGameObjectsWithTag("Chicken");
        nearest = FindClosestEnemy().name;
        target = FindClosestEnemy().transform;
        if (grassRespawn == true)
        {
            bearGrass = GameObject.FindGameObjectsWithTag("Food");
            targetGrass = FindClosestBearGrass().transform;
        }


        targetFood = FindClosestFood().transform;

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
        if (this.gameObject.tag == "BearMale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("BearFemale");
        }
        if (this.gameObject.tag == "BearFemale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("BearMale");
        }
        
        SetTagByGender();  //дать себе тэг по полу

        if (readyLove == true)
        {
            if (this.gameObject.tag == "BearMale" || this.gameObject.tag == "BearFemale")
            {
                if (FindClosestLove() != null)
                {
                    targetLove = FindClosestLove().transform;
                }
            }
        }


        //размножение

        if (WinterScript.timeSummer == true)
        {
            if (readyLove == false)//размножение
            {
                if (starthunt == false)
                {
                    if (helmOfDominator == false)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, targetHuman.position, speed * Time.deltaTime);
                    }
                }
                else
                {
                    if (targetFood.transform.position.y > targetGrass.transform.position.y)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, targetFood.position, speed * Time.deltaTime);
                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(transform.position, targetGrass.position, speed * Time.deltaTime);

                    }
                }
            }
            else
            {
                if (targetLove != null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetLove.position, speed * Time.deltaTime);
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



    }
    void FixedUpdate()
    {
        infoClassText.text = "ЭТО: " + thisIsClass;
        infoFoodText.text = "СЫТОСТЬ: " + food + "/" + maxfood;
        infoEatText.text = "ЗДОРОВЬЕ " + hp + "/" + maxfood;
        infoThisCoordinatesText.text = "X:" + this.gameObject.transform.position.x + " Y:" + this.gameObject.transform.position.y + " Z:" + this.gameObject.transform.position.z;

        if (fakeCollisionStart == false)
        {
            if (WinterScript.timeSummer == true)
            {
                this.GetComponent<PolygonCollider2D>().enabled = true;
            }
            else
            {
                this.GetComponent<PolygonCollider2D>().enabled = false;
            }
        }

        if (helmOfDominator == true)
        {
            targetHuman = closestHuman.transform;

            if (runHatCoroutine == false)
            {
                StartCoroutine(GiveHatCoroutine());
            }
            if(closestHuman.GetComponent<Backpack>().winterHat == true)
            {
                closestHuman.GetComponent<HumanMove>().winterhatobject.SetActive(true);
            }
            else
            {
                closestHuman.GetComponent<HumanMove>().winterhatobject.SetActive(false);
            }
        }



        if (food >= 70)
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

        if (targetFood != null)
        {
            targetFood.GetComponent<ChickenMove>()?.Attack();
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


        if (food < 65)
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
            food = food - UnityEngine.Random.Range(4, 9);
            yield return new WaitForSeconds(4f);
            runTired = false;
        }
    }
   
    public override IEnumerator RunRightOrLeft()
    {
        coordinateX = this.gameObject.transform.position.x;
        yield return new WaitForSeconds(0.1f);
        coordinateXTwo = this.gameObject.transform.position.x;

    }
    private IEnumerator GrassRespawnCoroutine()
    {
        yield return new WaitForSeconds(12f);
        grassRespawn = true;
    }
    private IEnumerator GiveHatCoroutine()
    {
        if (helmOfDominator == true)
        {
            runHatCoroutine = true;
            yield return new WaitForSeconds(30f);
            if (helmOfDominator == true)
            {
                closestHuman.GetComponent<Backpack>().winterHat = true;
            }
            runHatCoroutine = false;
        }
    }
    private IEnumerator WaitFood()
    {
        waitFoodStart = true;
        yield return new WaitForSeconds(10f);
        if (closestHuman.GetComponent<Backpack>().countGrass == 0 && closestHuman.GetComponent<Backpack>().countBerry == 0)
        {
            helmOfDominator = false;
            closestHuman.GetComponent<HumanMove>().summonBear--;
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


        if (this.gameObject.tag == "BearMale")
        {
            if (coll.gameObject.tag == "BearFemale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;

                this.gameObject.tag = "Bear";
                coll.gameObject.tag = "Bear";
                coll.gameObject.GetComponent<BearMove>().readyLove = false;

                Instantiate(bear, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);
            }
        }
        if (this.gameObject.tag == "BearFemale")
        {
            if (coll.gameObject.tag == "BearMale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;
                this.gameObject.tag = "Bear";
                coll.gameObject.tag = "Bear";
                coll.gameObject.GetComponent<BearMove>().readyLove = false;
                Instantiate(bear, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);

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
                    closestHuman.GetComponent<HumanMove>().summonBear++;
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
                        closestHuman.GetComponent<HumanMove>().summonBear++;
                        helmOfDominator = true;
                    }
                }

            }
            if (food <= 25)
            {
                if (helmOfDominator == true)
                {
                    if (closestHuman.GetComponent<Backpack>().countGrass == 0 && closestHuman.GetComponent<Backpack>().countBerry == 0)
                    {
                        if (waitFoodStart == false)
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
