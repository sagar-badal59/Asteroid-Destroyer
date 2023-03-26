using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public Sprite icon;

    public virtual void Use()
    {
        Debug.Log(name + "was used.");
    }
    public virtual PowerUpType getPowerUpType()
    {
        return PowerUpType.none;
    }
}

public enum PowerUpType { Blaster, Barrier,none }
