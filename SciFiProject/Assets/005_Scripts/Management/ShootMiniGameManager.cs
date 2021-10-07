using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMiniGameManager : AllosiusDev.Singleton<ShootMiniGameManager>
{
    #region Properties

    public GameCanvasManager gameCanvasManager { get; protected set; }
    public PlayerStats playerStats { get; protected set; }
    public int prestigePointsGained { get; set; }

    public int expPointsGained { get; set; }

    public List<LevelRank> LevelRankTitle => levelRanks;

    #endregion

    #region UnityInspector

    [SerializeField] private List<LevelRank> levelRanks = new List<LevelRank>();

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        gameCanvasManager = FindObjectOfType<GameCanvasManager>();

        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Start()
    {
        gameCanvasManager.ShootJobExpBar.SetMaxBarValue(playerStats.currentShootJobExpRequired);
        gameCanvasManager.ShootJobExpBar.SetBarValue(playerStats.currentShootJobExp);
        SetCurrentExpRank(playerStats.currentShootJobExp, playerStats.currentShootJobExpRequired);

        SetCurrentLevelRankTitle(playerStats.currentShootJobLevel);
        SetCurrentLevelBulletSprite(playerStats.currentShootJobLevel);
    }

    public void SetCurrentLevelRankTitle(int playerJobLevel)
    {
        gameCanvasManager.ShootJobExpLabelText.text = levelRanks[playerJobLevel].title + " :";
    }

    public void SetCurrentLevelBulletSprite(int playerJobLevel)
    {
        playerStats.GetComponent<PlayerShoot>().currentBulletSprite = levelRanks[playerJobLevel].bulletSprite;
    }

    public void SetCurrentExpRank(float playerCurrentJobExp, float playerCurrentJobExpRequired)
    {
        gameCanvasManager.ShootJobExpBar.SetBarValue(playerCurrentJobExp);
        int _playerCurrentJobExp = (int)(playerCurrentJobExp);
        int _playerCurrentJobExpRequired = (int)(playerCurrentJobExpRequired);
        gameCanvasManager.ShootJobExpAmountText.text = _playerCurrentJobExp.ToString() + "/" + _playerCurrentJobExpRequired;
    }

    #endregion
}
