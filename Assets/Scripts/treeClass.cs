using UnityEngine;
using System.Collections;

public class treeClass : MonoBehaviour
{
    public bool prolificness; //урожайность
    public bool eatable;   //съедобность

    public int randomNumber;
    public float timer = 16.00f;
    public bool timerRun = false;

    public Animation appleanim;
    public GameObject apple;

    void Example()
    {
        gameObject.tag = "Trees";
    }
    void Start()
    {


        randomNumber = UnityEngine.Random.Range(1, 3);
        if (randomNumber == 1)
        {
            prolificness = false;
        }
        else
        {
            prolificness = true;
        }
    }
     void FixedUpdate()
    {
        if(prolificness == true)
        {
            apple.SetActive(true);
            appleanim.Play("appleanim");
            prolificness = false;
            timerRun = true;
        }
        if(timerRun == true)
        {
           
            timer -= Time.deltaTime;
        }
        if(timer <=0.1f)
        {
            timer = 0f;
           
            
        }
        if(timer == 0f)
        {
            if (timerRun == true)
            {
                timerRun = false;
                apple.tag = "Food";
            }

        }
    }
}