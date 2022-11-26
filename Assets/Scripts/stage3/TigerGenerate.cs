using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerGenerate : MonoBehaviour
{
    public GameObject tiger;

    private float randomX;
    private float randomY;

    private int randomCountTiger;



    private void Start()
    {
        // GameObject newlama = Instantiate<GameObject>(lama);
        //newlama.name = "SomeNewName";


        randomCountTiger = UnityEngine.Random.Range(7, 10);

        while (randomCountTiger != 0)
        {
            randomX = UnityEngine.Random.Range(53.43f, 104f);
            randomY = UnityEngine.Random.Range(-55.06f, -30.66f);

            Instantiate(tiger, new Vector3(randomX, randomY, -3.47f), Quaternion.identity);
            randomCountTiger--;
        }
    }






}
