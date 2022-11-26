using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterScript : MonoBehaviour
{
    public GameObject biomSummer;
    public GameObject biomWinter;

    public static bool timeSummer = true;
    private bool timeCoroutineActive = false;


    private void Start()
    {
        Application.targetFrameRate = 300;
    }
    private void FixedUpdate()
    {
        if(timeCoroutineActive == false)
        {
            StartCoroutine(ChangeTimeYears());
        }


        if(timeSummer == true)
        {
            biomSummer.SetActive(true);
            biomWinter.SetActive(false);
        }
        if (timeSummer == false)
        {
            biomSummer.SetActive(false);
            biomWinter.SetActive(true);
        }
    }

    private IEnumerator ChangeTimeYears()
    {
        timeCoroutineActive = true;
        yield return new WaitForSeconds(40f);
        if(timeSummer == true)
        {
            timeSummer = false;
        }
        else
        {
            timeSummer = true;
        }
        timeCoroutineActive = false;
    }
}
