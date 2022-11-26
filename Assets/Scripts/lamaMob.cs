using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lamaMob : MonoBehaviour
{
    //компоненты
    private SpriteRenderer sprite;
    private Animator anim;
    public GameObject lamaObject;
    private Rigidbody2D rb;

    public static lamaMob Instance { get; set; }



     private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
     void Start()
    {

        State = States.runLama; 
    }
     private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
     void FixedUpdate()
    {

    }
     void Update()
    {
    }

}
public enum States
{
    //список состояний
    idleLama,
    runLama,
    
}