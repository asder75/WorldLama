using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TigerMob : MonoBehaviour
{
    //компоненты
    private SpriteRenderer sprite;
    private Animator anim;
    public GameObject tigerObject;
    private Rigidbody2D rb;

    public static TigerMob InstanceSix { get; set; }



    private void Awake()
    {
        InstanceSix = this;
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        State = StatesTiger.runTiger;
    }
    private StatesTiger State
    {
        get { return (StatesTiger)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    void FixedUpdate()
    {

    }
    void Update()
    {
    }

}
public enum StatesTiger
{
    //список состояний
    runTiger

}