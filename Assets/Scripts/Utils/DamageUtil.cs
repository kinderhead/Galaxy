using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUtil : MonoBehaviour
{
    public int heat;
    public int radiation;
    public float changeAmount = 1;

    bool runningCoroutine = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!runningCoroutine)
        {
            StartCoroutine(Calculate());
        }
    }

    IEnumerator Calculate()
    {
        runningCoroutine = true;

        foreach (Ship ship in Main.GetGalaxy().ships)
        {
            var p1 = GetComponent<Collider>().ClosestPointOnBounds(ship.gameObject.transform.position);
            var p2 = ship.GetComponentInChildren<Collider>().ClosestPointOnBounds(transform.position);

            var dist = Vector3.Distance(p1, p2);

            if (dist < heat * changeAmount)
            {
                var heatDamage = Math.Abs(heat - (dist / changeAmount));
                ship.UpdateHeat(this, (int)heatDamage);
            }
            else
            {
                ship.UpdateHeat(this, 0);
            }

            if (dist < radiation * changeAmount)
            {
                var radiationDamage = Math.Abs(radiation - (dist / changeAmount));
                ship.UpdateRadiation(this, (int)radiationDamage);
            }
            else
            {
                ship.UpdateRadiation(this, 0);
            }

            yield return ship;
        }

        runningCoroutine = false;
    }

    public static RadiationLevelSeverity GetRadiationValue(int num)
    {
        if (num == 0)
        {
            return RadiationLevelSeverity.None;
        }
        else if (num > 1 && num < 50)
        {
            return RadiationLevelSeverity.Light;
        }
        else if (num > 50 && num < 100)
        {
            return RadiationLevelSeverity.Medium;
        }
        else if (num > 100 && num < 500)
        {
            return RadiationLevelSeverity.Heavy;
        }
        else
        {
            return RadiationLevelSeverity.Extreme;
        }
    }

    public static HeatDamageSeverity GetHeatValue(int num)
    {
        if (num == 0)
        {
            return HeatDamageSeverity.None;
        }
        else if (num > 1 && num < 50)
        {
            return HeatDamageSeverity.Minor;
        }
        else if (num > 50 && num < 100)
        {
            return HeatDamageSeverity.Light;
        }
        else if (num > 100 && num < 500)
        {
            return HeatDamageSeverity.Medium;
        }
        else if (num > 500 && num < 1000)
        {
            return HeatDamageSeverity.Heavy;
        }
        else
        {
            return HeatDamageSeverity.Extreme;
        }
    }
}
