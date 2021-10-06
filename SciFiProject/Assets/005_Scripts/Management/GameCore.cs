using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : AllosiusDev.Singleton<GameCore>
{
    #region Fields

    private GameCanvasManager gameCanvasManager;
    private PlayerStats playerStats;

    private float countTimer;

    private bool resetMoodPlayer;

    #endregion

    #region Properties

    public int shooterJobLevel { get; set; }

    public bool shopMenuIsOpen { get; set; }

    public bool shootMiniGameActive { get; set; }

    public CameraCtrl MainCameraCtrl => mainCameraCtrl;
    public CameraCtrl ShootCameraCtrl => shootCameraCtrl;

    public GameObject WorldHub => worldHub;
    public GameObject WorldShootMiniGame => worldShootMiniGame;
    public GameObject ShootMiniGamePlayerSpawnPoint => shootMiniGamePlayerSpawnPoint;

    #endregion

    #region UnityInspector

    [SerializeField] private int moodTimeInterval;
    [SerializeField] private int moodLostPerTimeInterval;

    [Space]

    [SerializeField] private float timeToWaitBeforeResetMoodPlayer;
    [SerializeField] private float moodResetSpeed;

    [Space]

    [SerializeField] private CameraCtrl mainCameraCtrl;
    [SerializeField] private CameraCtrl shootCameraCtrl;

    [Space]

    [SerializeField] private GameObject worldHub;
    [SerializeField] private GameObject worldShootMiniGame;
    [SerializeField] private GameObject shootMiniGamePlayerSpawnPoint;

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
        gameCanvasManager.MoodBar.SetMaxBarValue(playerStats.Mood);
        gameCanvasManager.HealthBar.SetMaxBarValue(playerStats.Health);
    }

    private void Update()
    {
        AddCountTime(Time.deltaTime);
    }

    public GameCanvasManager GetGameCanvasManager()
    {
        return gameCanvasManager;
    }

    public void SetPrestigeAmount(int amount)
    {
        gameCanvasManager.SetPrestigeAmountText(amount);
    }

    public void AddCountTime(float amount)
    {
        countTimer += amount;

        if (countTimer >= moodTimeInterval && resetMoodPlayer == false)
        {
            countTimer = 0.0f;
            float newPlayerMoodValue = playerStats.ChangeMood(moodLostPerTimeInterval);
            gameCanvasManager.MoodBar.SetBarValue(newPlayerMoodValue);
            if(newPlayerMoodValue <= 0)
            {
                StartCoroutine(TimerResetMoodPlayer());
            }
        }

        if(resetMoodPlayer)
        {
            countTimer = 0.0f;
            float newPlayerMoodValue = playerStats.ChangeMood(moodResetSpeed);
            gameCanvasManager.MoodBar.SetBarValue(newPlayerMoodValue);
            if (newPlayerMoodValue >= gameCanvasManager.MoodBar.slider.maxValue)
            {
                resetMoodPlayer = false;
            }
        }
    }

    IEnumerator TimerResetMoodPlayer()
    {
        playerStats.GetComponent<Player>().canControl = false;

        resetMoodPlayer = true;

        yield return new WaitForSeconds(timeToWaitBeforeResetMoodPlayer);

        playerStats.GetComponent<Player>().canControl = true;

    }

    #endregion
}
