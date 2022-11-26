using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearMob : MonoBehaviour
{
    //компоненты
    private SpriteRenderer sprite;
    private Animator anim;
    public GameObject bearObj;
    private Rigidbody2D rb;

    public static BearMob InstanceFive { get; set; }



    private void Awake()
    {
        InstanceFive = this;
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        State = StatesBear.runBear;
    }
    private StatesBear State
    {
        get { return (StatesBear)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    void FixedUpdate()
    {

    }
    void Update()
    {
    }

}
public enum StatesBear
{
    //список состояний
    runBear

}

