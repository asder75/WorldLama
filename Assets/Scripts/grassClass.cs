using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class grassClass : MonoBehaviour
{
    public bool eatable;
    public bool seed;

    public int randomNumber;
    public float timer;
    public bool timerRun = false;
    public int stage; //стадия растения

    public GameObject grassObj;
    public GameObject strawberry;
    public GameObject badStrawberry;
    private SpriteRenderer spriteRenderer;
    //спрайты неплодонос. неядовитого растения
    public Sprite sprite1stage;
    public Sprite sprite2stage;
    public Sprite sprite3stage;
    public Sprite sprite4stage;
    //спрайты неплодонос. ядовитого растения
    public Sprite badSprite1stage;
    public Sprite badSprite2stage;
    public Sprite badSprite3stage;
    public Sprite badSprite4stage;
    //спрайты плодоносящего неядовитого
    public Sprite seedSprite3stage;
    //спрайты плодоносящего ядовитого
    public Sprite seedBadSprite3stage;

    public Text infoClassText;
    public Text infoThisCoordinatesText;
    void Start()
    {
       

        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = sprite1stage; // set the sprite to sprite1



        timerRun = true;//запустить таймер роста растений
        stage = 1;
        timer = 10f;

        randomNumber = UnityEngine.Random.Range(1, 6);
        if (randomNumber == 1)
        {
            eatable = false;         //шанс 1 к 5 что будет несъедобным
        }
        else
        {
            eatable = true;
        }

        randomNumber = UnityEngine.Random.Range(1, 6);
        if (randomNumber == 1)
        {
            seed = false;         //шанс 1 к 5 что будет неплодоносным
        }
        else
        {
            seed = true;
        }



    }
    void FixedUpdate()
    {
        ReplaceSprite();
       
        GrowPlant();

       
        if (WinterScript.timeSummer == false)
        {
            if (stage == 1)
            {
                timer = 5f;
            }
        }


            infoClassText.text = "ЭТО: РАСТЕНИЕ";
        infoThisCoordinatesText.text = "X:" + this.gameObject.transform.position.x + " Y:" + this.gameObject.transform.position.y + " Z:" + this.gameObject.transform.position.z;
    }
    public void ReplaceSprite()
    {
        if (eatable == true)
        {
            if (stage == 1)
            {
                spriteRenderer.sprite = sprite1stage;
            }
            if (stage == 2)
            {
                spriteRenderer.sprite = sprite2stage;
            }
            if (stage == 3)
            {
                if (seed == false)
                {
                    spriteRenderer.sprite = sprite3stage;
                }
                else
                {
                    spriteRenderer.sprite = seedSprite3stage;
                }
            }
            if (stage == 4)
            {
                spriteRenderer.sprite = sprite4stage;
            }
        }
        if(eatable == false)
        {
            if (stage == 1)
            {
                spriteRenderer.sprite = badSprite1stage;
            }
            if (stage == 2)
            {
                spriteRenderer.sprite = badSprite2stage;
            }
            if (stage == 3)
            {
                if (seed == false)
                {
                    spriteRenderer.sprite = badSprite3stage;
                }
                else
                {
                    spriteRenderer.sprite = seedBadSprite3stage;
                }
            }
            if (stage == 4)
            {
                spriteRenderer.sprite = badSprite4stage;
            }
        }
    }
    public void GrowPlant(float randomX = 0f,float randomY = 0f)
        {
        

        if (timerRun == true)
        {

            timer -= Time.deltaTime;
        }
        if (timer <= 0.1f)
        {
            timer = 0f;
        }
        if (timer == 0f)
        {
            if (timerRun == true)
            {
                timerRun = false;
                stage++;
                if (stage == 2)
                {
                    grassObj.GetComponent<PolygonCollider2D>().enabled = true; //
                    grassObj.tag = "Food";

                    
                        timer = 10f;
                        timerRun = true;
                    
                  
                    
                    
                }
                if (stage == 3)
                {
                    if (WinterScript.timeSummer == true)
                    {
                        timer = 25; //25f
                        timerRun = true;
                    }
                    if(WinterScript.timeSummer == false)
                    {
                        timer = 6f;
                        timerRun = true;
                    }

                    
                        randomX = UnityEngine.Random.Range(-1.5f, 1.5f);
                        randomY = UnityEngine.Random.Range(-1.5f, 1.5f);

                    //создание семени
                   Instantiate(grassObj, new Vector3(grassObj.transform.position.x + randomX, grassObj.transform.position.y + randomY, -3.47f), Quaternion.identity);



                    if (seed == true)
                    {
                        if (eatable == true)
                        {
                            strawberry.SetActive(true);
                        }
                        if (eatable == false)
                        {
                            badStrawberry.SetActive(true);
                        }
                    }
                    

                }
                }
                if (stage == 4)
                {
                    timer = 10f;
                    timerRun = true;
                    
                }
                if (stage == 5)
                {
                    Destroy(this.gameObject);
                }
            }
    }

    


}