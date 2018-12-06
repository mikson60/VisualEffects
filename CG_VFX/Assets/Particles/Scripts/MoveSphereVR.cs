using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MoveSphereVR : MonoBehaviour {

    public float speed;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float moveUp = 0.0f;
        if (Input.GetKeyDown("space"))
        {
            moveUp = 100.0f;
        }

        Vector3 movement = new Vector3(moveHorizontal, moveUp, moveVertical);

        rb.AddForce(movement * speed);

    }
}
