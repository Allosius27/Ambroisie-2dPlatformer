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

    public bool canRegenerate { get; set; }
    public float Mood => mood;
    public float Health => health;
    public float MaxHealth => maxHealth;
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

    public float currentBabiesFactoryJobExp { get; set; }
    public float currentBabiesFactoryJobExpRequired { get; set; }
    public int currentBabiesFactoryJobLevel { get; set; }

    public float currentDopeJobExp { get; set; }
    public float currentDopeJobExpRequired { get; set; }
    public int currentDopeJobLevel { get; set; }

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
        canRegenerate = true;

        playerShoot = GetComponent<PlayerShoot>();

        baseShootHealth = shootHealth;

        currentShootJobExpRequired = baseExpRequired;
        currentBabiesFactoryJobExpRequired = baseExpRequired;
        currentDopeJobExpRequired = baseExpRequired;

        maxHealth = this.health;
    }

    private void Start()
    {
        GameCore.Instance.SetPrestigeAmount(prestigePoints);

        DopeMiniGameManager.Instance.SetCurrentNumberDopesSlidersActives(currentDopeJobLevel);
    }

    public float ChangeHealth(float amount)
    {
        this.health += amount;
        if(this.health <= 0)
        {
            this.health = 0;
        }
        GameCore.Instance.GetGameCanvasManager().HealthBar.SetBarValue(this.health);
        return this.health;
    }

    public void TakeDamage(float amount)
    {
        if(ChangeHealth(amount) <= 0)
        {
            Player player = GetComponent<Player>();
            player.animator.SetTrigger("death");
            player.canControl = false;
            canRegenerate = false;
            GameObject capsule = Instantiate(player.PrefabHealCapsule, player.RegenCapsulePoint.position, player.RegenCapsulePoint.rotation);
        }
    }

    public void PlayerResurect()
    {
        GetComponent<Player>().canControl = true;
        GetComponent<Player>().animator.SetTrigger("resurect");
        this.health = maxHealth;
        GameCore.Instance.GetGameCanvasManager().HealthBar.SetBarValue(this.health);
        canRegenerate = true;
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

    public void ChangeBabiesFactoryJobExp(int amount)
    {
        if (this.currentBabiesFactoryJobExp < currentBabiesFactoryJobExpRequired)
        {
            this.currentBabiesFactoryJobExp += amount;

            BabiesFactoryLevelUp();

            BabiesFactoryMiniGameManager.Instance.SetCurrentExpRank(this.currentBabiesFactoryJobExp, currentBabiesFactoryJobExpRequired);
        }
        else
        {
            this.currentBabiesFactoryJobExp = currentBabiesFactoryJobExpRequired;
            BabiesFactoryMiniGameManager.Instance.SetCurrentExpRank(this.currentBabiesFactoryJobExp, currentBabiesFactoryJobExpRequired);
        }
    }

    public void BabiesFactoryLevelUp()
    {
        if (this.currentBabiesFactoryJobExp >= currentBabiesFactoryJobExpRequired && currentBabiesFactoryJobLevel < JobMaxLevel)
        {
            currentBabiesFactoryJobLevel++;
            BabiesFactoryMiniGameManager.Instance.SetCurrentLevelRankTitle(currentBabiesFactoryJobLevel);

            BabiesFactoryMiniGameManager.Instance.ChangeCountTime(BabiesFactoryMiniGameManager.Instance.MultiplierCountTimePerLevel);
            BabiesFactoryMiniGameManager.Instance.ChangeNumberOfColorsMachinesActived(BabiesFactoryMiniGameManager.Instance.LevelRankTitle[currentBabiesFactoryJobLevel].newNumberOfColorsMachinesActived);
            for (int i = 0; i < BabiesFactoryMiniGameManager.Instance.ColorsMachines.Count; i++)
            {
                BabiesFactoryMiniGameManager.Instance.ColorsMachines[i].ChangeRandomColors(BabiesFactoryMiniGameManager.Instance.LevelRankTitle[currentBabiesFactoryJobLevel].rankColors);
            }
            

            float remnantExp = this.currentBabiesFactoryJobExp - currentBabiesFactoryJobExpRequired;
            this.currentBabiesFactoryJobExp = 0;
            currentBabiesFactoryJobExpRequired = currentBabiesFactoryJobExpRequired * multiplierExpPerLevel;
            GameCore.Instance.GetGameCanvasManager().BabiesJobExpBar.SetMaxBarValue(currentBabiesFactoryJobExpRequired);
            if (remnantExp > 0)
            {
                ChangeBabiesFactoryJobExp((int)(remnantExp));
            }
        }
    }

    public void ChangeDopeJobExp(int amount)
    {
        if (this.currentDopeJobExp < currentDopeJobExpRequired)
        {
            this.currentDopeJobExp += amount;

            DopeLevelUp();

            DopeMiniGameManager.Instance.SetCurrentExpRank(this.currentDopeJobExp, currentDopeJobExpRequired);
        }
        else
        {
            this.currentDopeJobExp = currentDopeJobExpRequired;
            DopeMiniGameManager.Instance.SetCurrentExpRank(this.currentDopeJobExp, currentDopeJobExpRequired);
        }
    }

    public void DopeLevelUp()
    {
        if (this.currentDopeJobExp >= currentDopeJobExpRequired && currentDopeJobLevel < JobMaxLevel)
        {
            currentDopeJobLevel++;
            DopeMiniGameManager.Instance.SetCurrentLevelRankTitle(currentDopeJobLevel);

            DopeMiniGameManager.Instance.SetCurrentNumberDopesSlidersActives(currentDopeJobLevel);

            DopeMiniGameManager.Instance.ChangeCountTime(DopeMiniGameManager.Instance.MultiplierCountTimePerLevel);
            for (int i = 0; i < GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders.Count; i++)
            {
                GameCore.Instance.GetGameCanvasManager().Dopes.DopesSliders[i].ChangeSpeed();
            }


            float remnantExp = this.currentDopeJobExp - currentDopeJobExpRequired;
            this.currentDopeJobExp = 0;
            currentDopeJobExpRequired = currentDopeJobExpRequired * multiplierExpPerLevel;
            GameCore.Instance.GetGameCanvasManager().Dopes.DopeJobExpBar.SetMaxBarValue(currentDopeJobExpRequired);
            if (remnantExp > 0)
            {
                ChangeDopeJobExp((int)(remnantExp));
            }
        }
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
