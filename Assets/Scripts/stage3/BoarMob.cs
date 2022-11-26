using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoarMob : MonoBehaviour
{
    //компоненты
    private SpriteRenderer sprite;
    private Animator anim;
    public GameObject boarObj;
    private Rigidbody2D rb;

    public static BoarMob InstanceSeven { get; set; }



    private void Awake()
    {
        InstanceSeven = this;
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        State = StatesBoar.runBoar;
    }
    private StatesBoar State
    {
        get { return (StatesBoar)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    void FixedUpdate()
    {

    }
    void Update()
    {
    }

}
public enum StatesBoar
{
    //список состояний
    runBoar

}

