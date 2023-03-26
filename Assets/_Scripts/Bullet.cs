using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] SpriteRenderer icon;
    private Rigidbody2D rb;

    [SerializeField] Blaster blaster;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetIcon(this.blaster.icon);
    }

    public void Project(Vector2 direction)
    {
        Vector2 _dir = direction + blaster.direction;
        rb.AddForce(_dir * blaster.speed);

        Destroy(gameObject, blaster.lifetime);
    }
    public void SetBlaster(Blaster _blaster)
    {
        this.blaster = _blaster;
        SetIcon(this.blaster.icon);
    }
    public void SetIcon(Sprite _icon)
    {
        icon.sprite = _icon;
    }
    public BlasterType GetBlasterFireType()
    {
        return blaster.blasterType;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collided");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
