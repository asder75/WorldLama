using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGenerate : MonoBehaviour
{
    public GameObject human;

    private float randomX;
    private float randomY;

    private int randomCountHuman;



    private void Start()
    {
        // GameObject newlama = Instantiate<GameObject>(lama);
        //newlama.name = "SomeNewName";


        randomCountHuman = UnityEngine.Random.Range(18, 25);

        while (randomCountHuman != 0)
        {
            randomX = UnityEngine.Random.Range(-118f, 130f);
            randomY = UnityEngine.Random.Range(-79f, 85f);

            Instantiate(human, new Vector3(randomX, randomY, -3.47f), Quaternion.identity);
            randomCountHuman--;
        }
    }

}
