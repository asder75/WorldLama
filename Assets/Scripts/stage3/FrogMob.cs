using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogMob : MonoBehaviour
{
    //компоненты
    private SpriteRenderer sprite;
    private Animator anim;
    public GameObject frogObject;
    private Rigidbody2D rb;

    public static FrogMob InstanceTwo { get; set; }



    private void Awake()
    {
        InstanceTwo = this;
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        State = StatesFrog.idleFrog;
    }
    private StatesFrog State
    {
        get { return (StatesFrog)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    void FixedUpdate()
    {

    }
    void Update()
    {
    }

}
public enum StatesFrog
{
    //список состояний
    idleFrog,
    jumpFrog,

}

