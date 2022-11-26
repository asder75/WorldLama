using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogGenerate : MonoBehaviour
{
    public GameObject frog;

    private float randomX;
    private float randomY;

    private int randomCountFrog;



    private void Start()
    {
        // GameObject newlama = Instantiate<GameObject>(lama);
        //newlama.name = "SomeNewName";


        randomCountFrog = UnityEngine.Random.Range(30, 45);

        while (randomCountFrog != 0)
        {
            randomX = UnityEngine.Random.Range(-118f, 130f);
            randomY = UnityEngine.Random.Range(-79f, 85f);

            Instantiate(frog, new Vector3(randomX, randomY, -3.47f), Quaternion.identity);
            randomCountFrog--;
        }
    }


    



}
