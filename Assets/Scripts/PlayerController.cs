using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Controller
{
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode hold = KeyCode.LeftShift;

    public override void Tick(Ship ship)
    {
        var hullBar = GameObject.Find("Health").GetComponent<HealthBar>();
        hullBar.max = (int) ship.data.maxHullHP;
        hullBar.current = (int) Math.Ceiling(ship.data.hullHP);

        var shieldBar = GameObject.Find("Shield").GetComponent<HealthBar>();
        shieldBar.max = (int)ship.data.maxShieldHP;
        shieldBar.current = (int)Math.Ceiling(ship.data.shieldHP);

        if (Input.GetKey(hold))
        {
            holdSpeed = true;
        }
        else
        {
            holdSpeed = false;
        }

        if (Input.GetKey(forward))
        {
            ship.Forward();
        }
        else if (Input.GetKey(backward))
        {
            ship.Back();
        }

        if (Input.GetMouseButton(0) && !IsPointerOverUIObject())
        {
            Cursor.lockState = CursorLockMode.Locked;

            if (Input.GetAxis("Mouse X") > 0)
            {
                ship.Right();
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                ship.Left();
            }

            if (Input.GetAxis("Mouse Y") > 0)
            {
                ship.Up();
            }
            else if (Input.GetAxis("Mouse Y") < 0)
            {
                ship.Down();
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
