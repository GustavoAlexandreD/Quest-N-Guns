using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private Transform gunOffSet;

    [SerializeField]
    private float timeBetweenShots;

    private bool fireContinuously;
    private float lastFireTime;
    private bool fireSingle;

    private PlayerDamage playerDamage;

    private void Awake()
    {
        // Pega o dano atual do player
        playerDamage = GetComponent<PlayerDamage>();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (fireContinuously || fireSingle)
        {
            float timeSinceLastFire = Time.time - lastFireTime;

            if (timeSinceLastFire >= timeBetweenShots)
            {
                FireBullet();

                lastFireTime = Time.time;
                fireSingle = false;
            }
        }
    }

    private void FireBullet()
    {
        GameObject bulletObj = Instantiate(
            bulletPrefab,
            gunOffSet.position,
            gunOffSet.rotation
        );

        // Movimento da bala
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        rb.linearVelocity = gunOffSet.up * bulletSpeed;

        // Passa o dano atual para a bala
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null && playerDamage != null)
        {
            bullet.SetDamage(playerDamage.CurrentDamage);
        }
    }

    private void OnFire(InputValue inputValue)
    {
        if (Time.timeScale == 0) return;

        fireContinuously = inputValue.isPressed;

        if (inputValue.isPressed)
        {
            fireSingle = true;
        }
    }
}
