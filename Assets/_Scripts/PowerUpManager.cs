using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : SingletonMonoBehaviour<PowerUpManager>
{
    [SerializeField] float speed = 50f;
    [SerializeField] float lifetime = 30f;
    [SerializeField] int powerUpAppearPercentage = 100;
    [SerializeField] float powerUpduration = 10f;
    [SerializeField] Player _player;
    [SerializeField] Radar radarPrefab;
    [SerializeField] Blaster defaultBlaster;
    [SerializeField] List<PowerUp> scriptablePowerups;
    [SerializeField] PowerUpIcon1 _powerUpIcon1;




    private void Start()
    {
        _player.UpdateBlaster(defaultBlaster);
    }
    public void GeneratePowerUps(Transform _transform)
    {
        if (scriptablePowerups.Count == 0) return;
        int x = Random.Range(0, 100);
        if (x <= powerUpAppearPercentage)
        {
            Vector2 pos = _transform.position;
            pos += Random.insideUnitCircle * 0.5f;
            PowerUpIcon1 go = Instantiate(_powerUpIcon1, pos, _transform.rotation);
            go.InitializeData(scriptablePowerups[Random.Range(0, scriptablePowerups.Count)]);
            SetTrajectory(go.GetComponent<Rigidbody2D>(), Random.insideUnitCircle.normalized);
        }
    }
    public void SetTrajectory(Rigidbody2D rb,Vector2 direction)
    {
        rb.AddForce(direction * speed);

        Destroy(rb.gameObject, lifetime);
    }
    public void setPowerUpToPlayer(PowerUp powerUp)
    {
        switch (powerUp.getPowerUpType())
        {
            case PowerUpType.Blaster:
                Debug.Log("Bulllet Selected");
                var a = ((Blaster)powerUp);
                SetBlasterToPlayer(a);
                break;
            case PowerUpType.Barrier:
                Debug.Log("Shield Selected");
                var b = ((Barrier)powerUp);
                SetBarrierToPlayer(b);
                break;
            case PowerUpType.none:
                Debug.Log("None Selected");
                break;
        }
    }

    public void SetBlasterToPlayer(Blaster _blaster)
    {
        
        _player.UpdateBlaster(_blaster);
        ResetBullet();
    }
    public void SetBarrierToPlayer(Barrier _barrier)
    {
        radarPrefab.SetBarrier(_barrier);
        Radar radar = Instantiate(radarPrefab);
        radar.SetTarget(_player.transform);
    }

    public void ResetBullet()
    {
        StartCoroutine(ResetDefaultBullet());
    }
    IEnumerator ResetDefaultBullet()
    {
        yield return new WaitForSeconds(powerUpduration);
        _player.UpdateBlaster(defaultBlaster);

    }

}

