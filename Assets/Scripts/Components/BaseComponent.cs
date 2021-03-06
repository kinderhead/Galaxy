using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseComponent : Component
{
    public BaseComponent()
    {
        name = "Base ship";
        description = "Base ship component";
        rarity = RarityLevel.Special;
        canRemove = false;
        visible = false;
    }

    public override void Apply(Shipdata ship)
    {
        ship.acceleration += 1;
        ship.rotationAcceleration += .5f;
        ship.maxSpeed += 75;
        ship.maxRotationSpeed += 100;

        ship.maxHullHP += 100;
        ship.maxShieldHP += 75;

        ship.tolerableHeatDamage = HeatDamageSeverity.Minor;
    }
}
