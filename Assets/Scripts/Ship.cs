using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float acceleration = 0;
    public float rotationAcceleration = 0;

    public int maxSpeed = 0;
    public int maxRotationSpeed = 0;

    public bool warpEnabled = true;

    public Controller controller;

    float curSpeed = 0;
    float curHorizontalRotation = 0;
    float curVerticalRotation = 0;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        controller.Tick(this);

        Vector3 rot = transform.rotation.eulerAngles;
        rot.y += curHorizontalRotation * Time.deltaTime;
        rot.z += curVerticalRotation * Time.deltaTime;

        transform.rotation = Quaternion.Euler(rot);

        rb.velocity = transform.right * curSpeed;

        if (curHorizontalRotation > 0)
        {
            curHorizontalRotation -= rotationAcceleration;
        }
        else if (curHorizontalRotation < 0)
        {
            curHorizontalRotation += rotationAcceleration;
        }

        if (curVerticalRotation > 0)
        {
            curVerticalRotation -= rotationAcceleration;
        }
        else if (curVerticalRotation < 0)
        {
            curVerticalRotation += rotationAcceleration;
            //Debug.Log(curVerticalRotation);
        }

        if (curSpeed > 0)
        {
            curSpeed -= acceleration;
        }
        else if (curSpeed < 0)
        {
            curSpeed += acceleration;
        }

        if (curHorizontalRotation == 0 && curVerticalRotation == 0)
        {
            
        }
    }

    public void Forward()
    {
        if (Math.Abs(curSpeed) < maxSpeed)
        {
            curSpeed += acceleration * 2;
        }
    }

    public void Back()
    {
        if (Math.Abs(curSpeed) < maxSpeed)
        {
            curSpeed -= acceleration * 2;
        }
    }

    public void Left()
    {
        if (Math.Abs(curHorizontalRotation) < maxRotationSpeed)
        {
            curHorizontalRotation -= rotationAcceleration * 2;
        }
    }

    public void Right()
    {
        if (Math.Abs(curHorizontalRotation) < maxRotationSpeed)
        {
            curHorizontalRotation += rotationAcceleration * 2;
        }
    }

    public void Up()
    {
        if (Math.Abs(curVerticalRotation) < maxRotationSpeed)
        {
            curVerticalRotation += rotationAcceleration * 2;
        }
    }

    public void Down()
    {
        if (Math.Abs(curVerticalRotation) < maxRotationSpeed)
        {
            curVerticalRotation -= rotationAcceleration * 2;
        }
    }

    public void RollRight()
    {

    }

    public void RollLeft()
    {

    }
}
