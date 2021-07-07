using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Component
{
    public string name;
    public string description;
    public RarityLevel rarity;
    public bool canRemove = true;
    public bool visible = true;

    public Component() { }

    public abstract void Apply(ref Shipdata ship);
}
