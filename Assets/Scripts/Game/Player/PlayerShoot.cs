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
        GameObject bullet = Instantiate(bulletPrefab, gunOffSet.position, gunOffSet.rotation);

        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.linearVelocity = gunOffSet.up * bulletSpeed;
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