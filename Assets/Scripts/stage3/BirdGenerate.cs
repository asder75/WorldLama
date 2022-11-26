using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGenerate : MonoBehaviour
{
    public GameObject bird;

    private float randomX;
    private float randomY;

    private int randomCountBird;



    private void Start()
    {
        // GameObject newlama = Instantiate<GameObject>(lama);
        //newlama.name = "SomeNewName";


        randomCountBird = UnityEngine.Random.Range(15, 22);

        while (randomCountBird != 0)
        {
            randomX = UnityEngine.Random.Range(-118f, 130f);
            randomY = UnityEngine.Random.Range(-79f, 85f);

            Instantiate(bird, new Vector3(randomX, randomY, -3.47f), Quaternion.identity);
            randomCountBird--;
        }
    }






}
