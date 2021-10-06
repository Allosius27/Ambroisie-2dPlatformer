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

    public bool shopMenuIsOpen { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private int moodTimeInterval;
    [SerializeField] private int moodLostPerTimeInterval;

    [Space]

    [SerializeField] private float timeToWaitBeforeResetMoodPlayer;
    [SerializeField] private float moodResetSpeed;

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
