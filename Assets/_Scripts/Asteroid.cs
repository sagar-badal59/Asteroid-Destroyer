using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    public float size = 1f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    [SerializeField]
    private float speed = 50f;
    [SerializeField]
    private float lifetime = 30f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        sr.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360);
        transform.localScale = Vector3.one * size;

        rb.mass = size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * speed);

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if ((size * 0.5f) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            GameManager.Instance.AsteroidDestroyed(this);
            Destroy(gameObject);
        }
    }
    private void CreateSplit()
    {
        Vector2 pos = transform.position;
        pos += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, pos, transform.rotation);
        half.size = size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }
}
