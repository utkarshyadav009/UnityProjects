using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{

    public float CoinRotationSpeed = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,50.0f*Time.deltaTime,0);
    }
}
