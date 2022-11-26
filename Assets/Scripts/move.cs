using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    public Cinemachine.CinemachineVirtualCamera vcam;

    public GameObject lamaMobik;


    public static move Instance { get; set; }
    private void Awake()
    {

        Instance = this;
    }


       private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

   private  void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        
    }

     void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            vcam.m_Lens.OrthographicSize = 15;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = 0;
        }
       
        if (Input.GetKeyDown(KeyCode.V))
        {
           
            Instantiate(lamaMobik);
           
            
        }
       

        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0.1)
        {
            if (vcam.m_Lens.OrthographicSize >= 2)
            {
                vcam.m_Lens.OrthographicSize--;
            }
        }
        if (mw < -0.1)
        {
            if (vcam.m_Lens.OrthographicSize <= 25)
            {
                vcam.m_Lens.OrthographicSize++;
            }
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }




}
