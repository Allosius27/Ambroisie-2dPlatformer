using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    #region Fields

    private PlayerStats playerStats;

    #endregion

    #region Properties

    public bool is_firing { get; protected set; }

    #endregion

    #region UnityInspector

    [SerializeField] private Transform firePoint;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float baseShootingCooldownTime;

    #endregion

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();

        is_firing = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Shoot") && GameCore.Instance.shootMiniGameActive)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (is_firing == true)
        {
            // shooting logic

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.transform.SetParent(firePoint);
            bullet.transform.localPosition = Vector3.zero;
            bullet.GetComponent<PlayerBullet>().damage = playerStats.Strength;
            bullet.GetComponent<PlayerBullet>().direction = new Vector3(1, 0, 0);
            bullet.GetComponent<PlayerBullet>().speed = bulletSpeed; ;
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());

            is_firing = false;
            StartCoroutine(ShootCoolDownTime());
        }
    }

    IEnumerator ShootCoolDownTime()
    {
        yield return new WaitForSeconds(baseShootingCooldownTime);

        is_firing = true;

    }
}
