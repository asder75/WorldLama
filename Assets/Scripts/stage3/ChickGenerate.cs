using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickGenerate : MonoBehaviour
{
    public GameObject chicken;

    private float randomX;
    private float randomY;

    private int randomCountChicken;



    private void Start()
    {
        // GameObject newlama = Instantiate<GameObject>(lama);
        //newlama.name = "SomeNewName";


        randomCountChicken = UnityEngine.Random.Range(70, 80);

        while (randomCountChicken != 0)
        {
            randomX = UnityEngine.Random.Range(-118f, 130f);
            randomY = UnityEngine.Random.Range(-79f, 85f);

            Instantiate(chicken, new Vector3(randomX, randomY, -3.47f), Quaternion.identity);
            randomCountChicken--;
        }
    }






}
