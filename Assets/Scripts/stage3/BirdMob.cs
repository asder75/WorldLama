using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdMob : MonoBehaviour
{
    //компоненты
    private SpriteRenderer sprite;
    private Animator anim;
    public GameObject birdObject;
    private Rigidbody2D rb;

    public static BirdMob InstanceFive { get; set; }



    private void Awake()
    {
        InstanceFive = this;
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        State = StatesBird.flyBird;
    }
    private StatesBird State
    {
        get { return (StatesBird)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    void FixedUpdate()
    {

    }
    void Update()
    {
    }

}
public enum StatesBird
{
    //список состояний
    flyBird

}

