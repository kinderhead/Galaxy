using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Galaxy
{
    public int starCount;
    public List<Stardata> stars;
    public List<Shipdata> shipData;

    [System.NonSerialized]
    public List<Ship> ships;

    public string galaxyName;

    public Galaxy(int stars, string name)
    {
        starCount = stars;
        galaxyName = name;
        this.stars = new List<Stardata>();
        shipData = new List<Shipdata>();
        ships = new List<Ship>();

        for (int i = 0; i < stars; i++)
        {
            Debug.Log("Creating star");
            CreateStar();
        }

        var obj = new Shipdata(new List<Component>(new Component[] { Components.baseComponent })).Create();
        ships.Add(obj.GetComponent<Ship>());
        GameObject.Find("Main Camera").transform.SetParent(obj.transform);
        obj.GetComponent<Ship>().controller = obj.GetComponent<PlayerController>();
    }

    void CreateStar()
    {
        stars.Add(new Stardata("test", new Vector3()));
    }
}
