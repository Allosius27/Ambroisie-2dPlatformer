using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    #region Fields

    private PlayerStats playerStats;

    

    #endregion

    #region Properties

    public GameObject PrefabPlayerMaleTower => prefabPlayerMaleTower;
    public GameObject PrefabPlayerFemaleTower => prefabPlayerFemaleTower;
    public GameObject currentPrefabPlayerTower { get; set; }

    public Transform TurretPoint => turretPoint;

    public bool is_firing { get; protected set; }

    public Sprite currentBulletSprite { get; set; }

    public float BulletSpeed => bulletSpeed;
    public float MultiplierBulletSpeedPerLevel => multiplierBulletSpeedPerLevel;

    public float BaseShootingCooldownTime => baseShootingCooldownTime;
    public float MultiplierBaseShootingCooldownTimePerLevel => multiplierBaseShootingCooldownTimePerLevel;

    #endregion

    #region UnityInspector

    [SerializeField] private AllosiusDev.AudioData sfxBulletShot;

    [Space]

    [SerializeField] private GameObject prefabPlayerMaleTower, prefabPlayerFemaleTower;

    [Space]

    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform turretPoint;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float multiplierBulletSpeedPerLevel;

    [SerializeField] private float baseShootingCooldownTime;
    [SerializeField] private float multiplierBaseShootingCooldownTimePerLevel;

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

            AllosiusDev.AudioManager.Play(sfxBulletShot.sound);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.transform.SetParent(firePoint);
            bullet.transform.localPosition = Vector3.zero;

            if (currentBulletSprite != null)
            {
                bullet.GetComponent<SpriteRenderer>().sprite = currentBulletSprite;
            }

            bullet.GetComponent<PlayerBullet>().damage = playerStats.Strength;
            bullet.GetComponent<PlayerBullet>().direction = new Vector3(1, 0, 0);
            bullet.GetComponent<PlayerBullet>().speed = bulletSpeed;
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());

            is_firing = false;
            StartCoroutine(ShootCoolDownTime());
        }
    }

    public void ChangeBulletSpeed(float amount)
    {
        this.bulletSpeed *= amount;
    }

    public void ChangeBaseShootingCooldownTime(float amount)
    {
        this.baseShootingCooldownTime *= amount;
    }

    IEnumerator ShootCoolDownTime()
    {
        yield return new WaitForSeconds(baseShootingCooldownTime);

        is_firing = true;

    }
}
