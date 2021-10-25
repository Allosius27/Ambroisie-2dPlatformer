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

    public bool capsuleMoodInstance { get; set; }
    public PlayerStats PlayerStats => playerStats;
    public int shooterJobLevel { get; set; }

    public bool shopMenuIsOpen { get; set; }

    public bool shootMiniGameActive { get; set; }
    public bool babiesFactoryMiniGameActive { get; set; }
    public bool dopesMiniGameActive { get; set; }

    public CameraCtrl MainCameraCtrl => mainCameraCtrl;
    public CameraCtrl ShootCameraCtrl => shootCameraCtrl;
    public CameraCtrl BabiesCameraCtrl => babiesCameraCtrl;

    public GameObject WorldHub => worldHub;
    public GameObject WorldShootMiniGame => worldShootMiniGame;
    public GameObject ShootMiniGamePlayerSpawnPoint => shootMiniGamePlayerSpawnPoint;

    #endregion

    #region UnityInspector

    [SerializeField] private AllosiusDev.AudioData mainThemeMusic;
    [SerializeField] private AllosiusDev.AudioData sfxExitMiniGame;

    [Space]

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
        AllosiusDev.AudioManager.Play(mainThemeMusic.sound);

        gameCanvasManager.MoodBar.SetMaxBarValue(playerStats.Mood);
        gameCanvasManager.HealthBar.SetMaxBarValue(playerStats.Health);

        worldShootMiniGame.SetActive(false);
        worldBabiesMiniGame.SetActive(false);
        worldHub.SetActive(true);
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
        

        if(playerStats.Mood >= 20)
        {
            countTimer += amount;
        }
        else
        {
            if (babiesFactoryMiniGameActive == false && shootMiniGameActive == false && dopesMiniGameActive == false)
            {
                countTimer += amount;
            }
        }

        if (countTimer >= moodTimeInterval && resetMoodPlayer == false)
        {
            countTimer = 0.0f;
            float newPlayerMoodValue = playerStats.ChangeMood(moodLostPerTimeInterval);
            gameCanvasManager.MoodBar.SetBarValue(newPlayerMoodValue);
            
        }

        if (playerStats.Mood <= 0 && playerStats.Health >= playerStats.MaxHealth && capsuleMoodInstance == false && playerStats.canRegenerate)
        {
            capsuleMoodInstance = true;
            StartCoroutine(TimerResetMoodPlayer());
        }

        if (resetMoodPlayer)
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

    public void SetStateDopeMiniGame(bool value)
    {
        GetGameCanvasManager().LaunchFadeImage();

        worldHub.SetActive(!value);

        GetGameCanvasManager().Dopes.gameObject.SetActive(value);

        Player _player = playerStats.GetComponent<Player>();
        _player.canControl = !value;
        _player.canFall = !value;

        _player.graphics.GetComponent<SpriteRenderer>().enabled = !value;
        _player.graphics.GetComponent<Animator>().enabled = !value;

        GetGameCanvasManager().Dopes.SetBackgroundActive(value);

        InitNpcCtrl();

        if (value)
        {
            DopeMiniGameManager.Instance.ReinitCountTime();

            GetGameCanvasManager().Dopes.SetDopesSlidersActives();
        }
        else
        {
            AllosiusDev.AudioManager.Play(sfxExitMiniGame.sound);

            for (int i = 0; i < GetGameCanvasManager().Dopes.DopesSliders.Count; i++)
            {
                GetGameCanvasManager().Dopes.DopesSliders[i].ReinitValues();
            }
            
        }

        dopesMiniGameActive = value;
    }

    public void SetStateBabiesMiniGame(bool value)
    {
        GetGameCanvasManager().LaunchFadeImage();

        worldHub.SetActive(!value);
        worldBabiesMiniGame.SetActive(value);

        mainCameraCtrl.gameObject.SetActive(!value);
        babiesCameraCtrl.gameObject.SetActive(value);
        babiesCameraCtrl.SetPlayer(BabiesFactoryMiniGameManager.Instance.ColorsMachines[0].gameObject);


        GetGameCanvasManager().BabiesFactoryTimer.SetActive(value);
        GetGameCanvasManager().BabiesJobExpBar.gameObject.SetActive(value);

        Player _player = playerStats.GetComponent<Player>();
        _player.canControl = !value;

        _player.graphics.GetComponent<SpriteRenderer>().enabled = !value;
        _player.graphics.GetComponent<Animator>().enabled = !value;

        BabiesFactoryMiniGameManager.Instance.ColorsTouchs.gameObject.SetActive(value);

        InitNpcCtrl();

        if (value == true)
        {
            _player.transform.position = babiesFactoryMiniGamePlayerSpawnPoint.transform.position;

            for (int i = 0; i < BabiesFactoryMiniGameManager.Instance.ColorsMachines.Count; i++)
            {
                BabiesFactoryMiniGameManager.Instance.ColorsMachines[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < BabiesFactoryMiniGameManager.Instance.NumberOfColorsMachinesActived; i++)
            {
                if (i < BabiesFactoryMiniGameManager.Instance.ColorsMachines.Count)
                {
                    BabiesFactoryMiniGameManager.Instance.ColorsMachines[i].gameObject.SetActive(true);
                    BabiesFactoryMiniGameManager.Instance.ColorsMachines[i].SetColorSquare();
                }
            }
                
            BabiesFactoryMiniGameManager.Instance.ColorsTouchs.ActiveColorsTouchs(false);
            BabiesFactoryMiniGameManager.Instance.ColorsTouchs.SetColorsTouchs();

            BabiesFactoryMiniGameManager.Instance.ColorsCapsules.ReinitColorsCapsulesColors();
            BabiesFactoryMiniGameManager.Instance.ReinitColorTapisPosition();
            BabiesFactoryMiniGameManager.Instance.ColorsCapsules.currentIndexColorCapsule = 0;

            BabiesFactoryMiniGameManager.Instance.ReinitCountTime();

            BabiesFactoryMiniGameManager.Instance.MachinesCanShoot();
        }
        else
        {
            AllosiusDev.AudioManager.Play(sfxExitMiniGame.sound);

            BabiesFactoryMiniGameManager.Instance.BabiesMakeCtrl.ReinitPosition();

            _player.transform.position = babyFactoryEntrance.transform.position;
        }


        babiesFactoryMiniGameActive = value;

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

        InitNpcCtrl();

        if (value == true)
        {
            WaveSpawner waveSpawner = FindObjectOfType<WaveSpawner>();
            waveSpawner.state = WaveSpawner.SpawnState.COUNTING;

            

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
            AllosiusDev.AudioManager.Play(sfxExitMiniGame.sound);

            PlayerShoot _playerShoot = playerStats.GetComponent<PlayerShoot>();
            Destroy(_playerShoot.currentPrefabPlayerTower);


            _player.transform.position = ringEntrance.transform.position;
        }

        shootMiniGameActive = value;
    }

    public void InitNpcCtrl()
    {
        var pnjs = FindObjectsOfType<NpcCtrl>();
        for (int i = 0; i < pnjs.Length; i++)
        {
            pnjs[i].InitAnim();
        }
    }

    IEnumerator TimerResetMoodPlayer()
    {
        yield return new WaitForSeconds(0.25f);

        playerStats.GetComponent<Player>().canControl = false;

        yield return new WaitForSeconds(0.5f);

        GameObject capsule = Instantiate(playerStats.GetComponent<Player>().PrefabMoodCapsule,
            playerStats.GetComponent<Player>().RegenCapsulePoint.position, playerStats.GetComponent<Player>().RegenCapsulePoint.rotation);

        yield return new WaitForSeconds(timeToWaitBeforeResetMoodPlayer);
        resetMoodPlayer = true;

        //playerStats.GetComponent<Player>().canControl = true;

    }

    #endregion
}
