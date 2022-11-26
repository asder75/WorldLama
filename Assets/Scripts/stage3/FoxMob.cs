using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxMob : MonoBehaviour
{
    //компоненты
    private SpriteRenderer sprite;
    private Animator anim;
    public GameObject foxObject;
    private Rigidbody2D rb;

    public static FoxMob InstanceFo { get; set; }



    private void Awake()
    {
        InstanceFo = this;
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        State = StatesFox.runFox;
    }
    private StatesFox State
    {
        get { return (StatesFox)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    void FixedUpdate()
    {

    }
    void Update()
    {
    }

}
public enum StatesFox
{
    //список состояний
    runFox

}

