using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lamaGenerate : MonoBehaviour
{
    public GameObject lama;

    private float randomX;
    private float randomY;

    private int randomCountLama;
    private int runCourutineYes = 1;
    private float randomTimeGenerate = 0f;


  private  void Start()
    {
       // GameObject newlama = Instantiate<GameObject>(lama);
        //newlama.name = "SomeNewName";
        

        randomCountLama = UnityEngine.Random.Range(35, 45);

        while (randomCountLama != 0)
        {
            randomX = UnityEngine.Random.Range(-118f, 130f);
            randomY = UnityEngine.Random.Range(-79f, 85f);

            Instantiate(lama, new Vector3(randomX, randomY, -3.47f), Quaternion.identity);
            randomCountLama--;
        }
    }


   


}
