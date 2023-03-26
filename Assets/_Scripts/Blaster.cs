using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "PowerUp/Blaster")]
public class Blaster : PowerUp
{
    public readonly PowerUpType powerUpType = PowerUpType.Blaster;
    public BlasterType blasterType;
    public Vector2 direction;
    public int power;
    public int speed = 500;
    public float lifetime = 10f;

    public override PowerUpType getPowerUpType()
    {
        return powerUpType;
    }
    public Blaster GetPowerUp()
    {
        return this;
    }
}
public enum BlasterType { Single, Burst }
