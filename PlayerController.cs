using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent onJump;
    public UnityEvent onLand;
    public GameObject groundCheck;
    public float moveSpeed = 7.0f;
    public float acceleration = 50.0f;
    public float jumpSpeed = 10.0f;
    Rigidbody rb;
    int moveDir;
    float moveX;

    bool _grounded;
    bool grounded
    {
        get { 
            return _grounded; 
        } set { 
            if (_grounded != value)
            {
                if (!_grounded && value)
                    onLand?.Invoke();
                else
                    hasDoubleJumped = false;
                _grounded = value;
            }
        }
    }

    bool hasDoubleJumped;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveDir = (int)Input.GetAxisRaw("Horizontal");
        if (moveDir != 0)
            moveX = Mathf.MoveTowards(moveX, moveDir * moveSpeed, Time.deltaTime * acceleration);
        else
            moveX = Mathf.MoveTowards(moveX, moveDir * moveSpeed, Time.deltaTime * acceleration * 2f);

        grounded = Physics.CheckSphere(groundCheck.transform.position, 0.2f, LayerMask.GetMask("Ground"));

        if(Input.GetButtonDown("Jump"))
        {   if(grounded)
            { 
                Jump(); 
            } 
            else if (!hasDoubleJumped)
            {
                Jump();
                hasDoubleJumped = true;
            }
            
        }
        Debug.Log(hasDoubleJumped);
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0);
        onJump?.Invoke();
    }
    void FixedUpdate()
    {
        if(rb.velocity.y<0.75f*jumpSpeed || !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime * 5.0f;
        }
        rb.velocity = new Vector3(moveX, rb.velocity.y, 0);   
    }
}
