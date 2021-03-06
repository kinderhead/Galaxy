using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shipdata
{
    public List<Component> components;
    public Vector3 pos;
    public bool warpEnabled = true;

    public float acceleration = 0;
    public float rotationAcceleration = 0;

    public int maxSpeed = 0;
    public int maxRotationSpeed = 0;

    public float maxHullHP = 0;
    public float maxShieldHP = 0;

    public HeatDamageSeverity tolerableHeatDamage = HeatDamageSeverity.None;
    public RadiationLevelSeverity tolerableRadiation = RadiationLevelSeverity.None;

    // Do not reset
    float _hullHP;
    float _shieldHP;

    [System.NonSerialized]
    Ship ship;

    public float hullHP { get => _hullHP; set
        {
            _hullHP = value;
        }
    }
    public float shieldHP { get => _shieldHP; set
        {
            RenderShieldChange();
            _shieldHP = value;
        }
    }

    bool firstLoad = true;

    public Shipdata()
    {
        hullHP = maxHullHP;
        shieldHP = maxShieldHP;
    }

    public Shipdata(List<Component> components)
    {
        this.components = components;
    }

    public void Damage(float num, bool hullOnly = false)
    {
        if (hullHP == 0)
        {
            return;
        }

        if (shieldHP >= num && !hullOnly)
        {
            shieldHP -= num;
            try
            {
                ship.shield.Hit();
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("This is real bad");
                Debug.LogException(e);
            }
        }
        else
        {
            if (!hullOnly)
            {
                num -= shieldHP;
                if (shieldHP != 0)
                {
                    ship.shield.Hit();
                }
                shieldHP = 0;
            }

            hullHP -= num;
            if (hullHP <= 0)
            {
                hullHP = 0;
                ship.Destroy();
            }
        }
    }

    public GameObject Create()
    {
        GameObject obj = GameObject.Instantiate(Main.shipPrefab) as GameObject;
        obj.GetComponent<Ship>().data = this;
        ship = obj.GetComponent<Ship>();

        LoadComponents();

        return obj;
    }

    public void LoadComponents()
    {
        acceleration = 0;
        rotationAcceleration = 0;
        maxSpeed = 0;
        maxRotationSpeed = 0;
        warpEnabled = true;
        maxShieldHP = 0;
        maxHullHP = 0;
        tolerableHeatDamage = HeatDamageSeverity.None;
        tolerableRadiation = RadiationLevelSeverity.None;

        foreach (Component component in components)
        {
            component.Apply(this);
        }

        if (hullHP > maxHullHP)
        {
            hullHP = maxHullHP;
        }

        if (shieldHP > maxShieldHP)
        {
            shieldHP = maxShieldHP;
        }

        if (firstLoad)
        {
            hullHP = maxHullHP;
            shieldHP = maxShieldHP;
        }
        firstLoad = false;
    }

    void RenderShieldChange()
    {

    }
}
