using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarGenerate : MonoBehaviour
{
    public GameObject boar;

    private float randomX;
    private float randomY;

    private int randomCountBoar;



    private void Start()
    {
        // GameObject newlama = Instantiate<GameObject>(lama);
        //newlama.name = "SomeNewName";


        randomCountBoar = UnityEngine.Random.Range(14, 25);

        while (randomCountBoar != 0)
        {
            randomX = UnityEngine.Random.Range(-118f, 130f);
            randomY = UnityEngine.Random.Range(-79f, 85f);

            Instantiate(boar, new Vector3(randomX, randomY, -3.47f), Quaternion.identity);
            randomCountBoar--;
        }
    }






}