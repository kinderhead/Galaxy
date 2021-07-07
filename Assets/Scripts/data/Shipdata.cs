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

    public Shipdata()
    {

    }

    public Shipdata(List<Component> components)
    {
        this.components = components;
    }

    public GameObject Create()
    {
        GameObject obj = Object.Instantiate(Main.shipPrefab) as GameObject;

        LoadComponents(obj.GetComponent<Ship>());

        return obj;
    }

    public void LoadComponents(Ship obj)
    {
        foreach (Component component in components)
        {
            component.Apply(ref obj.data);
        }
    }
}
