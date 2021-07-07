using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Galaxy loadedGalaxy;

    public static GameObject shipPrefab;
    public GameObject prefab;

    private void Awake()
    {
        shipPrefab = prefab;
        Components.Load();
        loadedGalaxy = new Galaxy(1, "main");
    }
}
