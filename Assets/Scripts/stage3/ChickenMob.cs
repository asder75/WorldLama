using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenMob : MonoBehaviour
{
    //компоненты
    private SpriteRenderer sprite;
    private Animator anim;
    public GameObject chickenObject;
    private Rigidbody2D rb;

    public static ChickenMob InstanceThree { get; set; }



    private void Awake()
    {
        InstanceThree = this;
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        State = StatesChicken.runChick;
    }
    private StatesChicken State
    {
        get { return (StatesChicken)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    void FixedUpdate()
    {

    }
    void Update()
    {
    }

}
public enum StatesChicken
{
    //список состояний
    runChick

}

