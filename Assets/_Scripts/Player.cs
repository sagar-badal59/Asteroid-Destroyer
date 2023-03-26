using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private float thrustSpeed = 1f;
    [SerializeField]
    private float turnSpeed = 1f;

    private bool thrusting;
    private float turnDirection;

    private Rigidbody2D rb;

    [SerializeField] Vector3 startPos, finalPos;

    CinemachineShot cinemachine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        StartCoroutine(MoveToPosition());
    }

    private void Update()
    {
        if (!GameManager.Instance.canControlPlayer) return;
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f;
        }
        else
        {
            turnDirection = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    IEnumerator MoveToPosition()
    {
        transform.position = startPos;
        Debug.Log("Distance" + Vector3.Distance(transform.position, finalPos));
        while (Vector3.Distance(transform.position, finalPos) > 0.5)
        {
            Debug.Log("In While");
            transform.position = Vector2.Lerp(transform.position, finalPos, Time.deltaTime * thrustSpeed);
            yield return null;
        }
        StopCoroutine(MoveToPosition());
        GameManager.Instance.StartGame();

    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rb.AddForce(transform.up * thrustSpeed);
        }

        if (turnDirection != 0)
        {
            rb.AddTorque(turnDirection * turnSpeed);
        }
    }
    public void SetBullet(Bullet bullet)
    {
        bulletPrefab = bullet;
    }
    public void UpdateBlaster(Blaster _blaster)
    {
        bulletPrefab.SetBlaster(_blaster);
    }
    private void Shoot()
    {
        Debug.Log("Instantiate Bullet");

        switch (bulletPrefab.GetBlasterFireType())
        {
            case BlasterType.Single:
                FireSingle();
                break;
            case BlasterType.Burst:
                FireBurst();
                break;
        }
    }
    private void FireSingle()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }
    private void FireBurst()
    {
        for (int i = 0; i < 3; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, transform.position + (transform.up * i), transform.rotation);
            bullet.Project(transform.up);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;

            gameObject.SetActive(false);

            GameManager.Instance.PlayerDied();
        }
    }
}
