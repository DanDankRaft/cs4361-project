using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;
    public float gravity = 1;
    public float maxGravity = 10;
    public float jumpHeight = 1;
    private float currentGravity = 0;

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement = transform.TransformDirection(movement);
        movement *= speed;

        //handle gravity and jumping

        if(controller.isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
                Debug.Log("Jump!");

            currentGravity = Input.GetButtonDown("Jump") ? jumpHeight * gravity / Mathf.Sqrt(2) : currentGravity;
        }
        else
        {
            currentGravity = Math.Max(currentGravity - (gravity * Time.deltaTime), -maxGravity);
        }


        movement.y = currentGravity;
        movement *= Time.deltaTime;

        controller.Move(movement);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
