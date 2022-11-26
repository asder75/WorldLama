using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanMove : Animals
{
    public bool StateHumanBaby;
    public bool RunBabyCoroutine;
    public bool ForeverLove;
    public int LoveId;

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

    private SpriteRenderer sprite;
    private Animator anim;
    public GameObject boarObj;
    private Rigidbody2D rb;


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

    public GameObject wife;
    public GameObject swordObj;

    public GameObject winterhatobject;
    public GameObject capetigerobject;

    public Text infoClassText;
    public Text infoFoodText;
    public Text infoEatText;
    public Text infoSpText;
    public Text infoKpText;
    public Text infoThisCoordinatesText;
    public Text infoDominatorText;

    public int summonChicken;
    public int summonBear;
    public int summonTiger;
    //размножение

    public Backpack backComp;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        backComp = this.gameObject.GetComponent<Backpack>();
    }
    void Start()
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
        speed = UnityEngine.Random.Range(1.2f, 1.6f) ;
        timer = 0f;
        timerActive = false;
        runCoroutine = false;
        readyLove = false;
        readyCoroutineLove = false;
        readyCoroutineGrow = false;
        StateHumanBaby = true;
        RunBabyCoroutine = false;
        ForeverLove = false;
        starthunt = false;
        swordObj.SetActive(false);
        runCoroutineRunForFood = false;
        defWinter = false;
        thisIsClass = "Человек, всеядное";
        LoveId = UnityEngine.Random.Range(1, 32000);
        defaultTag = "Human";

        physic = GetComponent<Rigidbody2D>();

        grassi = GameObject.FindGameObjectsWithTag("Stonebear");
        foxFoods = GameObject.FindGameObjectsWithTag("Frog");
        bearGrass = GameObject.FindGameObjectsWithTag("Food");
        //размножение
        genderBoy = UnityEngine.Random.Range(0, 2);
        timeSecToFirstLove = UnityEngine.Random.Range(4, 10);
        //размножение

     

        StartCoroutine(GrassRespawnCoroutine());
        if(RunBabyCoroutine == false)
        {
            StartCoroutine(AgeUp());
        }
    }
    private StatesHuman State
    {
        get { return (StatesHuman)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
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

    // Update is called once per frame
    void FixedUpdate()
    {
        infoClassText.text = "ЭТО: " + thisIsClass;
        infoFoodText.text = "СЫТОСТЬ: " + food + "/" + maxfood;
        infoEatText.text = "ЗДОРОВЬЕ " + hp + "/" + maxfood;
        if (ForeverLove == false)
        {
            infoSpText.text = "СП: " + "В АКТИВНОМ ПОИСКЕ";
            infoKpText.text = "КП: ";
        }
        else 
        {
            infoSpText.text = "СП: " + "ЖЕНАТ/ЗАМУЖЕМ";
            if (wife != null)
            {
                infoKpText.text = "КП: " + "X:" + wife.transform.position.x + " Y:" + wife.transform.position.y + " Z:" + wife.transform.position.z;
            }
        }
        infoThisCoordinatesText.text = "X:" + this.gameObject.transform.position.x + " Y:" + this.gameObject.transform.position.y + " Z:" + this.gameObject.transform.position.z;
        infoDominatorText.text = "ПРИРУЧЕНО: " + summonChicken + " КУРИЦ, " + summonBear + " МЕДВЕДЕЙ, " + summonTiger + " ТИГРОВ";

        if (backComp.winterHat == true) //проверка на защиту от холода
        {
            defWinter = true;
        }
        if (backComp.tigerCap == true) //проверка на защиту от холода
        {
            speed = 2f;
        }

        if (StateHumanBaby == true)
        {
            State = StatesHuman.runBaby;
            starthunt = false;
            food = maxfood;
            swordObj.SetActive(false);
        }
        if (genderBoy == 0)
        {
            if (StateHumanBaby == false)
            {
                if (starthunt == false)
                {
                    State = StatesHuman.runHumanMan;
                }
                if(starthunt == true)
                {
                    State = StatesHuman.humanManAttack;
                }
            }
        }
        if (genderBoy == 1)
        {
            if (StateHumanBaby == false)
            {
                if (starthunt == false)
                {
                    State = StatesHuman.runHumanWoman;
                }
                if (starthunt == true)
                {
                    State = StatesHuman.humanWomanAttack;
                }
            }
        }

        if (this.gameObject.tag == "HumanMale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("HumanFemale");
        }
        if (this.gameObject.tag == "HumanFemale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("HumanMale");
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
                if (StateHumanBaby == false)
                {
                    lovePictObj.SetActive(true);
                }
            }
            else
            {
                lovePictObj.SetActive(false);
            }

        }
        if (StateHumanBaby == false)
        {

            SetTagByGender();  //дать себе тэг по полу

            if (readyLove == true)
            {
                if (this.gameObject.tag == "HumanMale" || this.gameObject.tag == "HumanFemale")
                {
                    if (FindClosestLove() != null)
                    {                      
                            targetLove = FindClosestLove().transform;                                            
                    }
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


        if (backComp.currentLoot < 10 && readyLove == false)
        {
            if (StateHumanBaby == false)
            {

                starthunt = true;
                if (runCoroutineRunForFood == false)
                {
                    StartCoroutine(RunForFood());
                }
            }
        }
        else
        {
            starthunt = false;
        }

       if(ForeverLove == true)
        {
            closestLove = wife;
        }

        //poedanie is iwentarya
        if (food <= 75)
        {
            if (backComp.currentLoot != 0)
            {
                if(backComp.countMeet >= 1)
                {
                    backComp.countMeet--;
                    if (food <= 75)
                    {
                        food = food + 25;
                    }
                    else
                    {
                        food = maxfood;
                    }
                }
                if (backComp.countGrass >= 1)
                {
                    backComp.countGrass--;
                    if (food <= 75)
                    {
                        food = food + 25;
                    }
                    else
                    {
                        food = maxfood;
                    }
                }
                if (backComp.countPoisonGrass >= 1)
                {
                    backComp.countPoisonGrass--;
                    hp = hp - 10;
                }
                if (backComp.countBerry >= 1)
                {
                    backComp.countBerry--;
                    if (food <= 75)
                    {
                        food = food + 25;
                    }
                    else
                    {
                        food = maxfood;
                    }
                }
                if (backComp.countPoisonBerry >= 1)
                {
                    backComp.countPoisonBerry--;
                    hp = hp - 10;
                }
                if (backComp.countEgg >= 1)
                {
                    backComp.countEgg--;
                    if (food <= 75)
                    {
                        food = food + 25;
                    }
                    else
                    {
                        food = maxfood;
                    }
                }
            }
            
        }

        if (readyLove == true)
        {
            starthunt = false;
        }

    }
    
    private void Update()
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
        if (starthunt == false)
        {
            if (runCoroutine == false)
            {
                if (readyLove == false)
                {
                    StartCoroutine(Timing());
                }
            }
        }
        else
        {
            if (readyLove == false)
            {
                StopCoroutine(Timing());
                if (targetFood.transform.position.y > targetGrass.transform.position.y)
                {
                    if (targetFood != null)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, targetFood.position, speed * Time.deltaTime);
                    }
                }
                else
                {
                    if (targetGrass != null)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, targetGrass.position, speed * Time.deltaTime);
                    }

                }
            }
        }

        //размножение
        if (readyCoroutineLove == false)
        {
            if (food >= 10)
            {
                if (StateHumanBaby == false)
                {

                    StartCoroutine(FallInLoveColdown());
                }
            }
            else
            {
                StopCoroutine(FallInLoveColdown());
                readyLove = false;
            }
        }


       

        
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        //if (coll.gameObject.tag == "HumanMale" || coll.gameObject.tag == "HumanFemale" || coll.gameObject.tag == "Human")
        //{
        //    if (coll.gameObject.GetComponent<HumanMove>().ForeverLove == false)
        //    {
        //        if (coll.gameObject.GetComponent<HumanMove>().LoveId != this.gameObject.GetComponent<HumanMove>().LoveId)
        //        {
        //            coll.gameObject.GetComponent<HumanMove>().closestLove = null;
        //            coll.gameObject.GetComponent<HumanMove>().StopCoroutine(FallInLoveColdown());
        //            coll.gameObject.GetComponent<HumanMove>().readyLove = false; ;
        //        }
        //    }
        //}

        if (this.gameObject.tag == "HumanMale")
        {
            if (coll.gameObject.tag == "HumanFemale" && coll.gameObject.GetComponent<HumanMove>().ForeverLove == false)
            {
               
                if (ForeverLove == false)
                {
                    Debug.Log("Ура ребенок");
                    readyLove = false;

                    this.gameObject.tag = "Human";
                    coll.gameObject.tag = "Human";
                    coll.gameObject.GetComponent<HumanMove>().readyLove = false;

                    Instantiate(boar, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);


                    coll.gameObject.GetComponent<HumanMove>().ForeverLove = true;
                    coll.gameObject.GetComponent<HumanMove>().LoveId = this.gameObject.GetComponent<HumanMove>().LoveId;
                    ForeverLove = true;
                    if (ForeverLove == true)
                    {
                        wife = coll.gameObject;
                        coll.gameObject.GetComponent<HumanMove>().wife = this.gameObject;
                    }
                }
               
                
            }
            //shenati
            if (coll.gameObject.tag == "HumanFemale" && coll.gameObject.GetComponent<HumanMove>().ForeverLove == true)
            {
                if (ForeverLove == true)
                {
                    if (coll.gameObject.GetComponent<HumanMove>().LoveId == this.gameObject.GetComponent<HumanMove>().LoveId)
                    {
                        Debug.Log("Ура ребенок");
                        readyLove = false;

                        this.gameObject.tag = "Human";
                        coll.gameObject.tag = "Human";
                        coll.gameObject.GetComponent<HumanMove>().readyLove = false;

                        Instantiate(boar, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);
                    }
                }


            }
        }
        if (this.gameObject.tag == "HumanFemale")
        {
            if (coll.gameObject.tag == "HumanMale" &&  coll.gameObject.GetComponent<HumanMove>().ForeverLove == false)
            {
               

                if (ForeverLove == false)
                {
                    Debug.Log("Ура ребенок");
                    readyLove = false;
                    this.gameObject.tag = "Human";
                    coll.gameObject.tag = "Human";
                    coll.gameObject.GetComponent<HumanMove>().readyLove = false;
                    Instantiate(boar, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);


                    coll.gameObject.GetComponent<HumanMove>().ForeverLove = true;
                    coll.gameObject.GetComponent<HumanMove>().LoveId = this.gameObject.GetComponent<HumanMove>().LoveId;
                    ForeverLove = true;
                    if (ForeverLove == true)
                    {
                        wife = coll.gameObject;
                        coll.gameObject.GetComponent<HumanMove>().wife = this.gameObject;
                    }

                }
                
            }
            //shenati
            if (coll.gameObject.tag == "HumanMale" && coll.gameObject.GetComponent<HumanMove>().ForeverLove == true)
            {
                if (ForeverLove == true)
                {
                    if (coll.gameObject.GetComponent<HumanMove>().LoveId == this.gameObject.GetComponent<HumanMove>().LoveId)
                    {
                        Debug.Log("Ура ребенок");
                        readyLove = false;
                        this.gameObject.tag = "Human";
                        coll.gameObject.tag = "Human";
                        coll.gameObject.GetComponent<HumanMove>().readyLove = false;
                        Instantiate(boar, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);
                    }

                } 

            }

            /////приручение курицы
            
            /////приручение медведя
            if (coll.gameObject.tag == "Bear")
            {
                if (coll.gameObject.GetComponent<BearMove>().helmOfDominator == false)
                {
                    if (this.gameObject.GetComponent<Backpack>().countGrass >= 1)
                    {
                        this.gameObject.GetComponent<Backpack>().countGrass--;
                        coll.gameObject.GetComponent<BearMove>().helmOfDominator = true;
                    }
                    if (coll.gameObject.GetComponent<BearMove>().helmOfDominator == false)
                    {
                        if (this.gameObject.GetComponent<Backpack>().countBerry >= 1)
                        {
                            this.gameObject.GetComponent<Backpack>().countBerry--;
                            coll.gameObject.GetComponent<BearMove>().helmOfDominator = true;
                        }
                    }
                    if (coll.gameObject.GetComponent<BearMove>().helmOfDominator == false)
                    {
                        if (this.gameObject.GetComponent<Backpack>().countMeet >= 1)
                        {
                            this.gameObject.GetComponent<Backpack>().countMeet--;
                            coll.gameObject.GetComponent<BearMove>().helmOfDominator = true;
                        }
                    }

                }
            }
            /////приручение тигра
            if (coll.gameObject.tag == "Tiger")
            {
                if (coll.gameObject.GetComponent<TigerMove>().helmOfDominator == false)
                {                                    
                        if (this.gameObject.GetComponent<Backpack>().countMeet >= 1)
                        {
                            this.gameObject.GetComponent<Backpack>().countMeet--;
                            coll.gameObject.GetComponent<TigerMove>().helmOfDominator = true;
                        }         

                }
            }
        }

      
    }


    private IEnumerator AgeUp()
    {
        RunBabyCoroutine = true;
        yield return new WaitForSeconds(12f);
        StateHumanBaby = false;
    }
    private IEnumerator Tired()
    {
        if (runTired == false)
        {
            runTired = true;
            food = food - UnityEngine.Random.Range(2, 4);
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



}
public enum StatesHuman
{
    //список состояний
    runBaby,
    runHumanMan,
    runHumanWoman,
    humanManAttack,
    humanWomanAttack

}
