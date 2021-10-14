using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Fields

    private float maxHealth;

    PlayerShoot playerShoot;

    #endregion

    #region Properties

    public float Mood => mood;
    public float Health => health;
    public int PrestigePoints => prestigePoints;

    public float BaseExpRequired => baseExpRequired;
    public float MultiplierExpPerLevel => multiplierExpPerLevel;
    public int JobMaxLevel => jobMaxLevel;

    public float Strength => strength;
    public float MultiplierStrengthPerLevel => multiplierExpPerLevel;

    public float baseShootHealth { get; set; }
    public float ShootHealth => shootHealth;
    public float currentShootJobExp { get; set; }
    public float currentShootJobExpRequired { get; set; }
    public int currentShootJobLevel { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private float mood;
    [SerializeField] private float health;
    [SerializeField] private int prestigePoints;

    [Space]

    [SerializeField] private float baseExpRequired;
    [SerializeField] private float multiplierExpPerLevel;
    [SerializeField] private int jobMaxLevel;

    [Space]

    [SerializeField] private float strength;
    [SerializeField] private float multiplierStrengthPerLevel = 1.0f;

    [Space]

    [SerializeField] private float shootHealth;

    #endregion

    #region Behaviour

    private void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();

        baseShootHealth = shootHealth;

        currentShootJobExpRequired = baseExpRequired;

        maxHealth = this.health;
    }

    private void Start()
    {
        GameCore.Instance.SetPrestigeAmount(prestigePoints);
    }

    public float ChangeHealth(float amount)
    {
        this.health += amount;
        return this.health;
    }

    public void TakeDamage(float amount)
    {
        if(ChangeHealth(amount) <= 0)
        {
            GetComponent<Player>().animator.SetTrigger("death");
            GetComponent<Player>().canControl = false;
        }
    }

    public void PlayerResurect()
    {
        GetComponent<Player>().animator.SetTrigger("resurect");
        this.health = maxHealth;
        GetComponent<Player>().canControl = true;
    }

    public float ChangeMood(float amount)
    {
        this.mood += amount;
        return this.mood;
    }

    public int ChangePrestigePoints(int amount)
    {
        this.prestigePoints += amount;
        return this.prestigePoints;
    }

    public void ChangeShootJobExp(int amount)
    {
        if (this.currentShootJobExp < currentShootJobExpRequired)
        {
            this.currentShootJobExp += amount;

            ShootLevelUp();

            ShootMiniGameManager.Instance.SetCurrentExpRank(this.currentShootJobExp, currentShootJobExpRequired);
        }
        else
        {
            this.currentShootJobExp = currentShootJobExpRequired;
            ShootMiniGameManager.Instance.SetCurrentExpRank(this.currentShootJobExp, currentShootJobExpRequired);
        }
    }

    public void ShootLevelUp()
    {
        if (this.currentShootJobExp >= currentShootJobExpRequired && currentShootJobLevel < JobMaxLevel)
        {
            currentShootJobLevel++;
            ShootMiniGameManager.Instance.SetCurrentLevelRankTitle(currentShootJobLevel);
            ShootMiniGameManager.Instance.SetCurrentLevelBulletSprite(currentShootJobLevel);

            ChangeStrength(multiplierStrengthPerLevel);

            playerShoot.ChangeBulletSpeed(playerShoot.MultiplierBulletSpeedPerLevel);
            playerShoot.ChangeBaseShootingCooldownTime(playerShoot.MultiplierBaseShootingCooldownTimePerLevel);

            float remnantExp = this.currentShootJobExp - currentShootJobExpRequired;
            this.currentShootJobExp = 0;
            currentShootJobExpRequired = currentShootJobExpRequired * multiplierExpPerLevel;
            ShootMiniGameManager.Instance.gameCanvasManager.ShootJobExpBar.SetMaxBarValue(currentShootJobExpRequired);
            if (remnantExp > 0)
            {
                ChangeShootJobExp((int)(remnantExp));
            }
        }
    }

    public void ChangeStrength(float amount)
    {
        this.strength = this.strength * amount;
    }

    public void SetShootHealth(float amount)
    {
        this.shootHealth = amount;
    }

    private float ChangeShootHealth(float amount)
    {
        this.shootHealth += amount;
        return this.shootHealth;
    }

    public void TakeShootDamage(float amount)
    {
        if(ChangeShootHealth(amount) <= 0)
        {
            StartCoroutine(TimerEndShootMiniGame());
        }
        GameCore.Instance.GetGameCanvasManager().ShootHealthBar.SetBarValue(shootHealth);
    }

    public IEnumerator TimerEndShootMiniGame()
    {
        WaveSpawner waveSpawner = FindObjectOfType<WaveSpawner>();
        waveSpawner.ReinitWaves();

        yield return new WaitForSeconds(1f);

        var Enemies = FindObjectsOfType<Enemy>();

        GameCore.Instance.SetStateShootMiniGame(false);

        for (int i = 0; i < Enemies.Length; i++)
        {
            Destroy(Enemies[i].gameObject);
        }

    }

    #endregion
}
