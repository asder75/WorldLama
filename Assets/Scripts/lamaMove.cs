using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lamaMove : Animals
{
    public static lamaMove InstanceNew { get; set; }

    public int hungerMoreCheck = 0;
    //компоненты
        private SpriteRenderer sprite;
        public Text hpText;
        public Text eatText;
     //типы бега

        private float coordinateX;
        private float coordinateXTwo;
        private int launchingTheCoroutine = 1; //0 - бег юнита неактивен, 1 - бег юнита активен
    //
        public GameObject skullLama;
        public GameObject lama;
        public GameObject cloud;

    public GameObject tigerObj;
    public bool isAttacking = false;


    //размножение
    public GameObject lovePictObj;
    public Transform targetLove;
    public GameObject[] loveObjects;
    public GameObject closestLove;

    //размножение

    public Text infoClassText;
    public Text infoFoodText;
    public Text infoEatText;
    public Text infoThisCoordinatesText;
    private void Awake()
    {
        InstanceNew = this;
        sprite = GetComponentInChildren<SpriteRenderer>();
 
    }
   private  void Start()
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
        speed = 1f;
        readyLove = false;
        readyCoroutineLove = false;
        readyCoroutineGrow = false;
        thisIsClass = "Лама, травоядное";
        defaultTag = "Lamster";



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
    private void Update()
    {
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

            if (this.gameObject.tag == "LamsterMale")
            {
                loveObjects = GameObject.FindGameObjectsWithTag("LamsterFemale");
            }
            if (this.gameObject.tag == "LamsterFemale")
            {
                loveObjects = GameObject.FindGameObjectsWithTag("LamsterMale");
            }

        SetTagByGender();  //дать себе тэг по полу

        if (readyLove == true)
            {
                if (this.gameObject.tag == "LamsterMale" || this.gameObject.tag == "LamsterFemale")
                {
                if (FindClosestLove() != null)
                {
                    targetLove = FindClosestLove().transform;
                }
                }
            }
        

        //размножение
        Vector3 dir = transform.right * 1; 
        Vector3 dirTwo = transform.right * -1; 
        Vector3 moveInput = transform.up * 1;
        Vector3 moveInputTwo = transform.up * -1;

        
        if (readyLove == false)//размножение
        {
            if (runningToTheRight == true)
            {

                transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
                sprite.flipX = true;
            }
            if (runningToTheLeft == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + dirTwo, speed * Time.deltaTime);
                sprite.flipX = false;
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
        else //размножение
        {
            if (targetLove != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetLove.position + moveInputTwo, speed * Time.deltaTime);
            }

        }
    }
    private void FixedUpdate()
    {
        infoClassText.text = "ЭТО: " + thisIsClass;
        infoFoodText.text = "СЫТОСТЬ: " + food + "/" + maxfood;
        infoEatText.text = "ЗДОРОВЬЕ " + hp + "/" + maxfood;
        infoThisCoordinatesText.text = "X:" + this.gameObject.transform.position.x + " Y:" + this.gameObject.transform.position.y + " Z:" + this.gameObject.transform.position.z;

        DisplayHp();
        DisplayEat();
        
       
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

        if (launchingTheCoroutine == 1)
        {
            StartCoroutine(Run());
        }

       if(food != 0)
        {
            cloud.SetActive(false);
        }

        if (food <= 30)
        {
            StartCoroutine(RunRightOrLeft());
            cloud.SetActive(true);
            
            if (coordinateX > coordinateXTwo) //движеится ли вправо
            {
                sprite.flipX = true;
            }
            if (coordinateX < coordinateXTwo) //движеится ли вправо
            {
                sprite.flipX = false;
            }

            launchingTheCoroutine = 0;
            runningToTheRight = false;
            runningToTheLeft = false;
            runningUp = false;
            runningDown = false;
            this.GetComponent<CapsuleCollider2D>().enabled = true;
            this.GetComponent<Pathfinding.AIPath>().enabled = true; //
            this.GetComponent<Pathfinding.AIDestinationSetter>().enabled = true; //
            StopCoroutine(Run());//new

            if (hungerMoreCheck == 0)
            {
                StartCoroutine(StarveFood());
            }

        }
        else
        {
            if (isAttacking == false)
            {
                if (readyLove == false)
                {
                    this.GetComponent<CapsuleCollider2D>().enabled = false; //
                }
                else
                {
                    this.GetComponent<CapsuleCollider2D>().enabled = true; //
                }
            }
            else
            {
                this.GetComponent<CapsuleCollider2D>().enabled = true; //
            }
        }
        //new
        if(food <= 0)
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
    }
   
    
    

    public void DisplayHp()
    {
        hpText.text = "" + hp;
       
    }
    public void DisplayEat()
    {
        eatText.text = "" + food;
    }

 

    private IEnumerator Run(int stateRightOrLeft = 0, int stateUpOrDown = 0, float randomSecRunXRight = 0f, float randomSecRunXLeft = 0f, float randomSecRunUp = 0f, float randomSecRunDown = 0f, int randomHungry = 0)
    {
        if(food <= 0)
        {
            stateRightOrLeft = 0;
            stateUpOrDown = 0;
            randomSecRunXRight = 0;
            randomSecRunXLeft = 0;
            randomSecRunUp = 0;
            randomSecRunDown = 0;
            randomHungry = 0;
        }
        stateRightOrLeft = UnityEngine.Random.Range(1, 3); //выбирается случайно куда бежать вправо или влево
        stateUpOrDown = UnityEngine.Random.Range(1, 4); //выбирается случайно бежать вверх/вниз/не менять координаты Y

        if (stateRightOrLeft == 1)     //если бежать вправо
        {
            launchingTheCoroutine = 0;

            randomSecRunXRight = UnityEngine.Random.Range(1f, 25f); //выбирается случайное кол-во секунд бега вправо
            runningToTheRight = true;  //в методе Update() начинает бежать вправо

            if (stateUpOrDown == 1)
            {
                randomSecRunUp = UnityEngine.Random.Range(1f, 25f);
                runningUp = true;
            }
            if (stateUpOrDown == 2)
            {
                randomSecRunDown = UnityEngine.Random.Range(1f, 25f);
                runningDown = true;
            }
            if (stateUpOrDown == 3)
            {
                runningUp = false;
                runningDown = false;
            }



            yield return new WaitForSeconds(randomSecRunXRight);
            runningToTheRight = false;
            runningToTheLeft = false;
            stateRightOrLeft = 0;

            stateUpOrDown = 0;
            runningUp = false;
            runningDown = false;

            randomHungry = UnityEngine.Random.Range(1, 10);
            if (food - randomHungry >= 0)
            {
                food = food - randomHungry;
            }
            else
            {
                food = 0;
            }
            launchingTheCoroutine = 1;


          



            //yield return new WaitForSeconds(1f);

        }
        if (stateRightOrLeft == 2)
        {
            launchingTheCoroutine = 0;


            randomSecRunXLeft = UnityEngine.Random.Range(1f, 25f);
            runningToTheLeft = true;


            if (stateUpOrDown == 1)
            {
                randomSecRunUp = UnityEngine.Random.Range(1f, 25f);
                runningUp = true;
            }
            if (stateUpOrDown == 2)
            {
                randomSecRunDown = UnityEngine.Random.Range(1f, 25f);
                runningDown = true;
            }
            if (stateUpOrDown == 3)
            {
                runningUp = false;
                runningDown = false;
            }


            yield return new WaitForSeconds(randomSecRunXLeft);
            runningToTheRight = false;
            runningToTheLeft = false;
            stateRightOrLeft = 0;


            stateUpOrDown = 0;
            runningUp = false;
            runningDown = false;


            randomHungry = UnityEngine.Random.Range(1, 10);
            if (food - randomHungry >= 0)
            {
                food = food - randomHungry;
            }
            else
            {
                food = 0;
            }

            launchingTheCoroutine = 1;



           

            //yield return new WaitForSeconds(1f);
        }





    }

    private IEnumerator StarveFood()
    {
        if (food <= 30)
        {
            hungerMoreCheck = 1;
            yield return new WaitForSeconds(2f);
            
            if (food <= 30)
            {
                if (food - 2 <= 0)
                {
                    food = 0;
                }
                else
                {
                    food = food - 2;
                }
            }
            yield return new WaitForSeconds(0.00000000000001f);
            hungerMoreCheck = 0;
        }


    }

    public override IEnumerator RunRightOrLeft()
    {
        coordinateX = this.gameObject.transform.position.x;
        yield return new WaitForSeconds(0.5f);
        coordinateXTwo = this.gameObject.transform.position.x;

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Tiger")
        {
            tigerObj = coll.gameObject;
            Attack();
            tigerObj.GetComponent<TigerMove>()?.Eat();
            Destroy(this.gameObject);

        }

        if (this.gameObject.tag == "LamsterMale")
        {
            if (coll.gameObject.tag == "LamsterFemale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;

                this.gameObject.tag = "Lamster";
                coll.gameObject.tag = "Lamster";
                coll.gameObject.GetComponent<lamaMove>().readyLove = false;

                Instantiate(lama, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);
            }
        }
        if (this.gameObject.tag == "LamsterFemale")
        {
            if (coll.gameObject.tag == "LamsterMale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;
                this.gameObject.tag = "Lamster";
                coll.gameObject.tag = "Lamster";
                coll.gameObject.GetComponent<lamaMove>().readyLove = false;
                Instantiate(lama, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);

            }
        }
    }
    public void Attack()
    {
        isAttacking = true;
    }

   

    }
