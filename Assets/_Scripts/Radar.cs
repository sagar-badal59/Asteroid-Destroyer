using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] SpriteRenderer icon;

    [SerializeField] Barrier barrier;
    [SerializeField] Transform target;
    [SerializeField] int power=1;
    // Start is called before the first frame update
    public void SetBarrier(Barrier _barrier)
    {
        this.barrier = _barrier;
        this.power = this.barrier.power;
        SetIcon(this.barrier.icon);
    }
    public void SetIcon(Sprite _icon)
    {
        icon.sprite = _icon;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position !=target.position)
        {
            transform.position = target.position;
        }
    }
    public void SetTarget(Transform _transform)
    {
        target = _transform;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            if (power < 1) {
                Destroy(gameObject);
            } else {
                Destroy(collision.gameObject);
            }
            power--;
        }
    }

    
}
