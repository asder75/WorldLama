using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFollow : MonoBehaviour
{
    public float speed = 1.3f;
    private Rigidbody2D physic;

    public Transform target;

    public GameObject[] grassi;   //new
    GameObject closest; //new

    public string nearest;  //new
    void Start()
    {
        grassi = GameObject.FindGameObjectsWithTag("Food");
        physic = GetComponent<Rigidbody2D>();
        //target = GameObject.FindGameObjectWithTag("Grass").GetComponent<Transform>();
    }
    GameObject FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in grassi)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    // Update is called once per frame
    void Update()
    {
        grassi = GameObject.FindGameObjectsWithTag("Food");
        nearest = FindClosestEnemy().name;
        target = FindClosestEnemy().transform;


        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


    }
}
