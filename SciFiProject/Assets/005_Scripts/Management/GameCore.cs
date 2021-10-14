using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : AllosiusDev.Singleton<GameCore>
{
    #region Fields

    private GameCanvasManager gameCanvasManager;
    private PlayerStats playerStats;

    private BabyFactoryEntrance babyFactoryEntrance;
    private RingEntrance ringEntrance;

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
    [SerializeField] private CameraCtrl babiesCameraCtrl;

    [Space]

    [SerializeField] private GameObject worldHub;
    [Space]
    [SerializeField] private GameObject worldShootMiniGame;
    [SerializeField] private GameObject shootMiniGamePlayerSpawnPoint;
    [Space]
    [SerializeField] private GameObject worldBabiesMiniGame;
    [SerializeField] private GameObject babiesFactoryMiniGamePlayerSpawnPoint;

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        gameCanvasManager = FindObjectOfType<GameCanvasManager>();
        playerStats = FindObjectOfType<PlayerStats>();

        babyFactoryEntrance = FindObjectOfType<BabyFactoryEntrance>();
        ringEntrance = FindObjectOfType<RingEntrance>();
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

    public void SetStateBabiesMiniGame(bool value)
    {
        mainCameraCtrl.gameObject.SetActive(!value);
        babiesCameraCtrl.gameObject.SetActive(value);
        babiesCameraCtrl.SetPlayer(BabiesFactoryMiniGameManager.Instance.ColorMachine);

        worldHub.SetActive(!value);
        worldBabiesMiniGame.SetActive(value);

        GetGameCanvasManager().BabiesFactoryTimer.SetActive(value);

        Player _player = playerStats.GetComponent<Player>();
        _player.canControl = !value;

        _player.graphics.GetComponent<SpriteRenderer>().enabled = !value;
        _player.graphics.GetComponent<Animator>().enabled = !value;

        if(value == true)
        {
            _player.transform.position = babiesFactoryMiniGamePlayerSpawnPoint.transform.position;
        }
        else
        {
            _player.transform.position = babyFactoryEntrance.transform.position;
        }

        BabiesFactoryMiniGameManager.Instance.ColorMachine.GetComponent<ColorMachine>().SetColorSquare();

    }

    public void SetStateShootMiniGame(bool value)
    {
        MainCameraCtrl.gameObject.SetActive(!value);
        ShootCameraCtrl.gameObject.SetActive(value);

        WorldHub.SetActive(!value);
        WorldShootMiniGame.SetActive(value);

        GetGameCanvasManager().HealthBar.gameObject.SetActive(!value);
        GetGameCanvasManager().ShootHealthBar.gameObject.SetActive(value);
        GetGameCanvasManager().ShootJobExpBar.gameObject.SetActive(value);


        Player _player = playerStats.GetComponent<Player>();
        _player.canControl = !value;

        _player.graphics.GetComponent<SpriteRenderer>().enabled = !value;
        _player.graphics.GetComponent<Animator>().enabled = !value;

        if (value == true)
        {
            PlayerShoot _playerShoot = playerStats.GetComponent<PlayerShoot>();
            if (_player.isFemale)
            {
                GameObject _turret = Instantiate(_playerShoot.PrefabPlayerFemaleTower);
                _turret.transform.SetParent(_playerShoot.TurretPoint);
                _turret.transform.localPosition = Vector3.zero;
                _turret.transform.rotation = Quaternion.identity;
                _playerShoot.currentPrefabPlayerTower = _turret;
            }
            else
            {
                GameObject _turret = Instantiate(_playerShoot.PrefabPlayerMaleTower);
                _turret.transform.SetParent(_playerShoot.TurretPoint);
                _turret.transform.localPosition = Vector3.zero;
                _turret.transform.rotation = Quaternion.identity;
                _playerShoot.currentPrefabPlayerTower = _turret;
            }

            _playerShoot.currentPrefabPlayerTower.GetComponent<PlayerVisual>().Anim.SetLayerWeight(1, 1);

            _player.transform.position = shootMiniGamePlayerSpawnPoint.transform.position;
            playerStats.SetShootHealth(playerStats.baseShootHealth);
            GetGameCanvasManager().ShootHealthBar.SetMaxBarValue(playerStats.ShootHealth);
        }
        else
        {

            PlayerShoot _playerShoot = playerStats.GetComponent<PlayerShoot>();
            Destroy(_playerShoot.currentPrefabPlayerTower);


            _player.transform.position = ringEntrance.transform.position;
        }

        shootMiniGameActive = value;
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
