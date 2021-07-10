using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ship : MonoBehaviour
{
    public Shipdata data;

    public Controller controller;
    public ShieldHelper shield;

    float curSpeed = 0;
    float curHorizontalRotation = 0;
    float curVerticalRotation = 0;

    public HeatDamageSeverity curHeat;
    public int heatValue;
    public RadiationLevelSeverity curRadiation;
    public int radiationValue;

    Rigidbody rb;

    Dictionary<object, int> heatUpdates = new Dictionary<object, int>();
    Dictionary<object, int> radiationUpdates = new Dictionary<object, int>();

    public void Damage(float num)
    {
        data.Damage(num);
    }

    public void Destroy()
    {
        Debug.Log("Ship destroyed");
    }

    public void UpdateHeat(object source, int amount)
    {
        if (!heatUpdates.ContainsKey(source))
        {
            heatUpdates.Add(source, amount);
        }
        else
        {
            heatUpdates[source] = amount;
        }

        RecalculateHeatAndRadiation();
    }

    public void UpdateRadiation(object source, int amount)
    {
        if (!radiationUpdates.ContainsKey(source))
        {
            radiationUpdates.Add(source, amount);
        }
        else
        {
            radiationUpdates[source] = amount;
        }

        RecalculateHeatAndRadiation();
    }

    void RecalculateHeatAndRadiation()
    {
        heatValue = 0;
        radiationValue = 0;

        int val = 0;
        foreach (int amount in heatUpdates.Values)
        {
            if (val < amount)
            {
                val = amount;
            }
        }
        heatValue = val;

        foreach (int amount in radiationUpdates.Values)
        {
            radiationValue += amount;
        }

        curHeat = DamageUtil.GetHeatValue(heatValue);
        curRadiation = DamageUtil.GetRadiationValue(radiationValue);
    }

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
            curHorizontalRotation -= data.rotationAcceleration;
        }
        else if (curHorizontalRotation < 0)
        {
            curHorizontalRotation += data.rotationAcceleration;
        }

        if (curVerticalRotation > 0)
        {
            curVerticalRotation -= data.rotationAcceleration;
        }
        else if (curVerticalRotation < 0)
        {
            curVerticalRotation += data.rotationAcceleration;
            //Debug.Log(curVerticalRotation);
        }

        if (curSpeed > 0)
        {
            if (!controller.holdSpeed)
            {
                curSpeed -= data.acceleration;
            }
        }
        else if (curSpeed < 0)
        {
            if (!controller.holdSpeed)
            {
                curSpeed += data.acceleration;
            }
        }

        if ((int) curHeat > (int) data.tolerableHeatDamage)
        {
            Debug.Log("Taking heat damage");
            Damage(Time.deltaTime * 15f);
        }
    }

    public void Forward()
    {
        if (Math.Abs(curSpeed) < data.maxSpeed)
        {
            curSpeed += data.acceleration * 2;
        }
    }

    public void Back()
    {
        if (Math.Abs(curSpeed) < data.maxSpeed)
        {
            curSpeed -= data.acceleration * 2;
        }
    }

    public void Left()
    {
        if (Math.Abs(curHorizontalRotation) < data.maxRotationSpeed)
        {
            curHorizontalRotation -= data.rotationAcceleration * 2;
        }
    }

    public void Right()
    {
        if (Math.Abs(curHorizontalRotation) < data.maxRotationSpeed)
        {
            curHorizontalRotation += data.rotationAcceleration * 2;
        }
    }

    public void Up()
    {
        if (Math.Abs(curVerticalRotation) < data.maxRotationSpeed)
        {
            curVerticalRotation += data.rotationAcceleration * 2;
        }
    }

    public void Down()
    {
        if (Math.Abs(curVerticalRotation) < data.maxRotationSpeed)
        {
            curVerticalRotation -= data.rotationAcceleration * 2;
        }
    }

    public void RollRight()
    {

    }

    public void RollLeft()
    {

    }
}
