using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Galaxy loadedGalaxy;

    public static GameObject shipPrefab;
    public GameObject prefab;

    static Main globalMain;

    private void Awake()
    {
        shipPrefab = prefab;
        Components.Load();
        loadedGalaxy = new Galaxy(1, "main");
        globalMain = this;
    }

    public static Galaxy GetGalaxy()
    {
        return globalMain.loadedGalaxy;
    }
}
