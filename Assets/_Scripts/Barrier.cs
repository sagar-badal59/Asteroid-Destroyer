using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "PowerUp/Barrier")]
public class Barrier : PowerUp
{
    public int power;
    public readonly PowerUpType powerUpType = PowerUpType.Barrier;

    public override PowerUpType getPowerUpType()
    {
        return powerUpType;
    }
}
