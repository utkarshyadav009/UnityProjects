using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Movement speed for the player
    public float rotationSpeed = 100f;  // Rotation speed for the player
    public float jumpSpeed = 10.0f;
    public UnityEvent onJump;
    public UnityEvent onLand;
    public UnityEvent CoinCollected;
    public GameObject groundCheck;

    public TMP_Text CoinText;


    int Coins;

    private Rigidbody rb;
    private bool _grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        transform.position += moveDirection;

        float rotationAngle = horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotationAngle, 0);
    }

    void HandleJump()
    {
        _grounded = Physics.CheckSphere(groundCheck.transform.position, 0.2f, LayerMask.GetMask("Ground"));

        if (Input.GetButtonDown("Jump"))
        {
            if (_grounded)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        onJump?.Invoke();
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < 0.75f * jumpSpeed || !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime * 5.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Coin")
        {
            Destroy(other.gameObject);
            CoinCollected?.Invoke();
            Coins++;
            CoinTextUpdate();
        }
    }

    void CoinTextUpdate()
    {
        CoinText.text = "Coins Collected: " + Coins;
    }
}