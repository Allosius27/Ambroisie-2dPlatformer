using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Fields

    #endregion

    #region Properties

    public float Mood => mood;
    public float Health => health;
    public int PrestigePoints => prestigePoints;

    public float BaseExpRequired => baseExpRequired;
    public float MultiplierExpPerLevel => multiplierExpPerLevel;
    public int JobMaxLevel => jobMaxLevel;

    public float Strength => strength;

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

    [Space]

    [SerializeField] private float shootHealth;

    #endregion

    #region Behaviour

    private void Awake()
    {
        baseShootHealth = shootHealth;

        currentShootJobExpRequired = baseExpRequired;
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

            if (this.currentShootJobExp >= currentShootJobExpRequired && currentShootJobLevel < JobMaxLevel)
            {
                currentShootJobLevel++;
                ShootMiniGameManager.Instance.SetCurrentLevelRankTitle(currentShootJobLevel);

                float remnantExp = this.currentShootJobExp - currentShootJobExpRequired;
                this.currentShootJobExp = 0;
                currentShootJobExpRequired = currentShootJobExpRequired * multiplierExpPerLevel;
                ShootMiniGameManager.Instance.gameCanvasManager.ShootJobExpBar.SetMaxBarValue(currentShootJobExpRequired);
                if (remnantExp > 0)
                {
                    ChangeShootJobExp((int)(remnantExp));
                }
            }

            ShootMiniGameManager.Instance.SetCurrentExpRank(this.currentShootJobExp, currentShootJobExpRequired);
        }
        else
        {
            this.currentShootJobExp = currentShootJobExpRequired;
            ShootMiniGameManager.Instance.SetCurrentExpRank(this.currentShootJobExp, currentShootJobExpRequired);
        }
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