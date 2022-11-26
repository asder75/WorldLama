using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGenerate : MonoBehaviour
{
    public GameObject treeElka;
    public GameObject treeSavanna;
    public GameObject berlogaBear;

    private float randomX;
    private float randomY;

    private int randomCountTrees;


    private float randomXSavanna;
    private float randomYSavanna;

    private int randomCountTreesSavanna;


    private float randomXSberloga;
    private float randomYSberloga;

    private int randomCountBerloga;


    private void Start()
    {
        // GameObject newlama = Instantiate<GameObject>(lama);
        //newlama.name = "SomeNewName";


        randomCountTrees = UnityEngine.Random.Range(20, 30);

        while (randomCountTrees != 0)
        {
            randomX = UnityEngine.Random.Range(-102.81f, -14.9f);
            randomY = UnityEngine.Random.Range(32.47f, 79f);

            Instantiate(treeElka, new Vector3(randomX, randomY, -3.47f), Quaternion.identity);
            randomCountTrees--;
        }

        randomCountTreesSavanna = UnityEngine.Random.Range(10, 16);
        while (randomCountTreesSavanna != 0)
        {
            randomXSavanna = UnityEngine.Random.Range(53.43f, 104f);
            randomYSavanna = UnityEngine.Random.Range(-55.06f, -30.66f);

            Instantiate(treeSavanna, new Vector3(randomXSavanna, randomYSavanna, -3.47f), Quaternion.identity);
            randomCountTreesSavanna--;
        }

        randomCountBerloga = UnityEngine.Random.Range(15, 17);
        while (randomCountBerloga != 0)
        {
            randomXSberloga = UnityEngine.Random.Range(-118f, 130f);
            randomYSberloga = UnityEngine.Random.Range(-79f, 85f);

            Instantiate(berlogaBear, new Vector3(randomXSberloga, randomYSberloga, -3.47f), Quaternion.identity);
            randomCountBerloga--;
        }
    }






}