using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Controller : MonoBehaviour
{
    public bool holdSpeed;

    public abstract void Tick(Ship ship);
}
