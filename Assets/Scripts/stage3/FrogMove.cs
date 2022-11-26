using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogMove : Animals
{
    public Text hpText;
    public Text eatText;

    private SpriteRenderer spriteRenderer;

    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    public GameObject birdObj;
    public GameObject boarObj;

    public bool isAttacking = false;
    //размножение
    public GameObject lovePictObj;
    public Transform targetLove;
    public GameObject[] loveObjects;
    public GameObject closestLove;


    public GameObject frog;
    public GameObject humanObj;
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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = spriteUp; // set the sprite to sprite1

        runningToTheRight = false;
        runningToTheLeft = false;
        runningUp = false;
        runningDown = false;
        food = 25;
        maxfood = 25;
        hp = 25;
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
        thisIsClass = "Лягушка, травоядное";
        defaultTag = "Frog";

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

        if (food >= 8)
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

        if (this.gameObject.tag == "FrogMale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("FrogFemale");
            if (food < 8)
            {
                food = 8;
            }
        }
        if (this.gameObject.tag == "FrogFemale")
        {
            loveObjects = GameObject.FindGameObjectsWithTag("FrogMale");
            if (food < 8)
            {
                food = 8;
            }
        }

        //размножение
        if (readyCoroutineLove == false)
        {
            if (food >= 8)
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
            if (this.gameObject.tag == "FrogMale" || this.gameObject.tag == "FrogFemale")
            {
                if (FindClosestLove() != null)
                {
                    targetLove = FindClosestLove().transform;
                }
            }
        }
            if (runCoroutine == false)
        {
            if (isAttacking == false)
            {
                StartCoroutine(Timing());
            }
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
            if (food <= 7)
        {
            if (hungerCheck == 0)
            {
                StartCoroutine(Starve());
            }
            this.GetComponent<FrogFollow>().enabled = true;
            this.GetComponent<CapsuleCollider2D>().enabled = true; //
        }
        else
        {
            if (isAttacking == false)
            {
                if (readyLove == false)
                {
                    this.GetComponent<CapsuleCollider2D>().enabled = false;
                }
                else
                {
                    this.GetComponent<CapsuleCollider2D>().enabled = true;
                }
            }
            else
            {
                this.GetComponent<CapsuleCollider2D>().enabled = true;
            }
        }

        if(hp <=0)
        {
            GoDie();
        }
        
        if(food <= 0)
        {

            this.GetComponent<FrogFollow>().enabled = true;
        }
        else
        {
            if (food > 7)
            {
                this.GetComponent<FrogFollow>().enabled = false;
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
            SetMotionVectors();

            if (timerActive == true)
            {
                if (runningToTheRight == true)
                {

                    transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
                    spriteRenderer.sprite = spriteRight;

                    if (runningUp == true || runningDown == true)
                    {
                        spriteRenderer.sprite = spriteRight;
                    }
                    //spriteRenderer.flipX = true;
                }
                if (runningToTheLeft == true)
                {

                    transform.position = Vector3.MoveTowards(transform.position, transform.position + dirTwo, speed * Time.deltaTime);
                    spriteRenderer.sprite = spriteLeft;

                    if (runningUp == true || runningDown == true)
                    {
                        spriteRenderer.sprite = spriteLeft;
                    }
                    //spriteRenderer.flipX = false;
                }

                if (runningUp == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + moveInput, speed * Time.deltaTime);
                    if (runningToTheLeft == false)
                    {
                        spriteRenderer.sprite = spriteUp;
                    }
                    if (runningToTheLeft == true)
                    {
                        spriteRenderer.sprite = spriteLeft;
                    }
                    if (runningToTheRight == false)
                    {
                        spriteRenderer.sprite = spriteUp;
                    }
                    if (runningToTheRight == true)
                    {
                        spriteRenderer.sprite = spriteRight;
                    }

                }
                if (runningDown == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + moveInputTwo, speed * Time.deltaTime);
                    if (runningToTheRight == false)
                    {
                        spriteRenderer.sprite = spriteDown;
                    }
                    if (runningToTheRight == true)
                    {
                        spriteRenderer.sprite = spriteRight;
                    }
                    if (runningToTheLeft == false)
                    {
                        spriteRenderer.sprite = spriteDown;
                    }
                    if (runningToTheLeft == true)
                    {
                        spriteRenderer.sprite = spriteLeft;
                    }
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
    }

    private IEnumerator Timing()
    {
        runCoroutine = true;

        runningRightOrLeft = UnityEngine.Random.Range(1, 4);
        runningUpOrDown = UnityEngine.Random.Range(1, 4);
        timerActive = true;
        timer = UnityEngine.Random.Range(0.1f, 1.5f);
        yield return new WaitForSeconds(timer);
        runningRightOrLeft = 1;
        yield return new WaitForSeconds(2f);

        GraduallyStarve();

        timerActive = false;
        runCoroutine = false;


    }

    public override IEnumerator RunRightOrLeft()
    {
        yield return new WaitForSeconds(0.5f);

    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bird")
        {
             birdObj = coll.gameObject;
            Attack();
            birdObj.GetComponent<BirdMove>()?.Eat();
            Destroy(this.gameObject);

        }

        if (coll.gameObject.tag == "Boar")
        {
            boarObj = coll.gameObject;
            Attack();
            boarObj.GetComponent<BoarMove>()?.Eat();
            Destroy(this.gameObject);

        }
        if (coll.gameObject.tag == "Human" || coll.gameObject.tag == "HumanFemale" || coll.gameObject.tag == "HumanMale")
        {
            humanObj = coll.gameObject;
            Attack();
            if (coll.gameObject.GetComponent<Backpack>().currentLoot < 10)
            {
                humanObj.GetComponent<Backpack>().countMeet++;
            }
            Destroy(this.gameObject);

        }
        if (this.gameObject.tag == "FrogMale")
        {
            if (coll.gameObject.tag == "FrogFemale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;

                this.gameObject.tag = "Frog";
                coll.gameObject.tag = "Frog";
                coll.gameObject.GetComponent<FrogMove>().readyLove = false;

                Instantiate(frog, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);
            }
        }
        if (this.gameObject.tag == "FrogFemale")
        {
            if (coll.gameObject.tag == "FrogMale")
            {
                Debug.Log("Ура ребенок");
                readyLove = false;
                this.gameObject.tag = "Frog";
                coll.gameObject.tag = "Frog";
                coll.gameObject.GetComponent<FrogMove>().readyLove = false;
                Instantiate(frog, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.47f), Quaternion.identity);

            }
        }
    }
    public void Attack()
    {
        isAttacking = true;
    }
  

}
    
  
