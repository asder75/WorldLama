using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearGenerate : MonoBehaviour
{
    public GameObject bear;

    private float randomX;
    private float randomY;

    private int randomCountBear;



    private void Start()
    {
        // GameObject newlama = Instantiate<GameObject>(lama);
        //newlama.name = "SomeNewName";


        randomCountBear = UnityEngine.Random.Range(10, 15);

        while (randomCountBear != 0)
        {
            randomX = UnityEngine.Random.Range(-118f, 130f);
            randomY = UnityEngine.Random.Range(-79f, 85f);

            Instantiate(bear, new Vector3(randomX, randomY, -3.47f), Quaternion.identity);
            randomCountBear--;
        }
    }






}