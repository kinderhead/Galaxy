using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stardata
{
    public string starName;
    public Vector3 pos;

    public Stardata(string name, Vector3 pos)
    {
        starName = name;
        this.pos = pos;
    }
}
