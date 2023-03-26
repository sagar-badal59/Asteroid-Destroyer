using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIcon1 : MonoBehaviour
{
    [SerializeField] SpriteRenderer icon;

    [SerializeField] PowerUp powerUp;

    public void InitializeData(PowerUp _powerUp)
    {
        powerUp = _powerUp;
        icon.sprite = powerUp.icon;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.LogError("Power Up Collided with Player");
            //PowerUpManager.Instance.setPowerUpToPlayer(this);
            PowerUpManager.Instance.setPowerUpToPlayer(powerUp);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
