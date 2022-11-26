using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassGenerate : MonoBehaviour
{
    public GameObject grass;
    public GameObject lama;

    private float randomCoordinateXGrass;
    private float randomCoordinateYGrass;
    private int randomCountOfGrass;
    private int launchingTheCoroutine = 1;
    private float randomGenerationTimeOfGrass = 0f;

    private float randomCoordinateXLama;
    private float randomCoordinateYLama;
    private int randomCountOfLama;
    private int launchingTheCoroutineTwo = 1;
    private float randomGenerationTimeOfLama = 0f;



    private   void Start()
    {

        randomCountOfGrass = UnityEngine.Random.Range(220, 260);



        while (randomCountOfGrass != 0)
        {
            randomCoordinateXGrass = UnityEngine.Random.Range(-118f, 130f);
            randomCoordinateYGrass = UnityEngine.Random.Range(-79f, 85f);

            Instantiate(grass, new Vector3(randomCoordinateXGrass, randomCoordinateYGrass, -3.47f), Quaternion.identity);
            randomCountOfGrass--;
        }
        if(randomCountOfGrass == 0)
        {
            while (randomCountOfLama != 0)
            {
                randomCoordinateXLama = UnityEngine.Random.Range(-118f, 130f);
                randomCoordinateYLama = UnityEngine.Random.Range(-79f, 85f);

                Instantiate(grass, new Vector3(randomCoordinateXLama, randomCoordinateYLama, -3.47f), Quaternion.identity);
                randomCountOfLama--;
            }
        }
    }

   

}


