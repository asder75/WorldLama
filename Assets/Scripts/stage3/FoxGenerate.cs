using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxGenerate : MonoBehaviour
{
    public GameObject fox;

    private float randomX;
    private float randomY;

    private int randomCountFox;



    private void Start()
    {
        // GameObject newlama = Instantiate<GameObject>(lama);
        //newlama.name = "SomeNewName";


        randomCountFox = UnityEngine.Random.Range(9, 17);

        while (randomCountFox != 0)
        {
            randomX = UnityEngine.Random.Range(-102.81f, -14.9f);
            randomY = UnityEngine.Random.Range(32.47f, 79f);

            Instantiate(fox, new Vector3(randomX, randomY, -3.47f), Quaternion.identity);
            randomCountFox--;
        }
    }






}
