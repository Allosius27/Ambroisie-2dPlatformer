using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabiesFactoryMiniGameManager : AllosiusDev.Singleton<BabiesFactoryMiniGameManager>
{
    #region Fields

    private float baseCountTime;

    private Vector3 baseColorTapisPosition;

    private float baseColorTapisMoveSpeed;


    #endregion

    #region Properties

    public bool colorTapisMoving { get; set; }
    public Transform colorTapisTarget { get; set; }

    public GameObject ColorTapis => colorTapis;

    public int NumberOfColorsMachinesActived => numberOfColorsMachinesActived;
    public List<ColorMachine> ColorsMachines => colorsMachines;

    public ColorsTouchs ColorsTouchs => colorsTouchs;
    public ColorsCapsules ColorsCapsules => colorsCapsules;


    public PlayerStats playerStats { get; protected set; }

    public int prestigePointsGained { get; set; }

    public int expPointsGained { get; set; }

    public float MultiplierCountTimePerLevel => multiplierCountTimePerLevel;


    public BabiesMakeCtrl BabiesMakeCtrl => babiesMakeCtrl;

    public List<BabiesFactoryLevelRank> LevelRankTitle => levelRanks;


    #endregion

    #region UnityInspector

    [SerializeField] private AllosiusDev.AudioData sfxBabiesSort;
    [SerializeField] private AllosiusDev.AudioData sfxMoveTapis;

    [Space]

    [SerializeField] private GameObject colorTapis;
    [SerializeField] private float colorTapisMoveSpeed;

    [Space]

    [SerializeField] private int numberOfColorsMachinesActived;
    [SerializeField] private List<ColorMachine> colorsMachines = new List<ColorMachine>();

    [SerializeField] private ColorsTouchs colorsTouchs;

    [SerializeField] private ColorsCapsules colorsCapsules;

    [SerializeField] private float countTime;
    [SerializeField] private float multiplierCountTimePerLevel = 1.0f;

    [Space]

    [SerializeField] private BabiesMakeCtrl babiesMakeCtrl;

    [SerializeField] private List<BabiesFactoryLevelRank> levelRanks = new List<BabiesFactoryLevelRank>();

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        baseCountTime = countTime;

        baseColorTapisPosition = colorTapis.transform.position;

        baseColorTapisMoveSpeed = colorTapisMoveSpeed;

        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Start()
    {
        GameCore.Instance.GetGameCanvasManager().BabiesJobExpBar.SetMaxBarValue(playerStats.currentBabiesFactoryJobExpRequired);
        GameCore.Instance.GetGameCanvasManager().BabiesJobExpBar.SetBarValue(playerStats.currentBabiesFactoryJobExp);
        SetCurrentExpRank(playerStats.currentBabiesFactoryJobExp, playerStats.currentBabiesFactoryJobExpRequired);

        SetCurrentLevelRankTitle(playerStats.currentBabiesFactoryJobLevel);
    }

    public void SetCurrentLevelRankTitle(int playerJobLevel)
    {
        GameCore.Instance.GetGameCanvasManager().BabiesJobExpLabelText.text = levelRanks[playerJobLevel].title + " :";
    }

    public void SetCurrentExpRank(float playerCurrentJobExp, float playerCurrentJobExpRequired)
    {
        GameCore.Instance.GetGameCanvasManager().BabiesJobExpBar.SetBarValue(playerCurrentJobExp);
        int _playerCurrentJobExp = (int)(playerCurrentJobExp);
        int _playerCurrentJobExpRequired = (int)(playerCurrentJobExpRequired);
        GameCore.Instance.GetGameCanvasManager().BabiesJobExpAmountText.text = _playerCurrentJobExp.ToString() + "/" + _playerCurrentJobExpRequired;
    }

    public void ChangeNumberOfColorsMachinesActived(int newNumber)
    {
        numberOfColorsMachinesActived = newNumber;
        for (int i = 0; i < numberOfColorsMachinesActived; i++)
        {
            if (i < ColorsMachines.Count)
            {
                colorsMachines[i].gameObject.SetActive(true);
                colorsMachines[i].SetColorSquare();
            }
        }
    }

    public void ChangeCountTime(float amount)
    {
        baseCountTime *= amount;
    }

    public void ReinitColorTapisPosition()
    {
        colorTapis.transform.position = baseColorTapisPosition;
    }

    public void ReinitCountTime()
    {
        countTime = baseCountTime;
    }

    public void MoveTapis()
    {
        colorsCapsules.currentIndexColorCapsule++;

        /*while (colorsCapsules.ListColorsCapsules[colorsCapsules.currentIndexColorCapsule].GetComponent<ColorCapsule>().isFilled && 
            colorsCapsules.currentIndexColorCapsule < colorsCapsules.ListColorsCapsules.Count)
        {
            colorsCapsules.currentIndexColorCapsule++;
        }*/

        if(colorsCapsules.currentIndexColorCapsule >= colorsCapsules.ListColorsCapsules.Count)
        {
            GameCore.Instance.GetGameCanvasManager().LaunchFadeImage();
            ColorsCapsules.ReinitColorsCapsulesColors();
            ReinitColorTapisPosition();
            colorsCapsules.currentIndexColorCapsule = 0;
            //colorsCapsules.currentIndexColorCapsule = colorsCapsules.ListColorsCapsules.Count - 1;
            //return;
        }

        colorTapisTarget = colorsCapsules.ListColorsCapsules[colorsCapsules.currentIndexColorCapsule].transform;
        colorTapisMoving = true;
        StartCoroutine(PlaySfxMoveTapis());
        colorTapisMoveSpeed = baseColorTapisMoveSpeed;
    }

    private void Update()
    {
        if(GameCore.Instance.babiesFactoryMiniGameActive)
        {
            countTime -= Time.deltaTime;
            UpdateCountText();
        }

        if(colorTapisMoving)
        {
            Vector3 dir = colorTapisTarget.position - colorTapis.transform.position;
            dir.Normalize();
            colorTapis.transform.position = new Vector3(colorTapis.transform.position.x + colorTapisMoveSpeed * Time.deltaTime * dir.x, colorTapis.transform.position.y, colorTapis.transform.position.z);
            //transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);



            // Si l'ennemi est quasiment arrivé à sa destination
            if (Vector3.Distance(colorTapis.transform.position, colorTapisTarget.position) < 0.5f)
            {
                colorTapisMoveSpeed = 0;
                colorTapisMoving = false;
                AllosiusDev.AudioManager.Stop(sfxMoveTapis.sound);

                MachinesCanShoot();
            }
        }
    }

    public void MachinesCanShoot()
    {
        for (int i = 0; i < colorsMachines.Count; i++)
        {
            ColorsMachines[i].canShoot = true;
        }
    }

    IEnumerator PlaySfxMoveTapis()
    {
        AllosiusDev.AudioManager.Play(sfxMoveTapis.sound);

        yield return new WaitForSeconds(sfxMoveTapis.sound.Clip.length);

        if(colorTapisMoving)
        {
            StartCoroutine(PlaySfxMoveTapis());
        }
    }

    private void UpdateCountText()
    {
        TimeSpan time = TimeSpan.FromSeconds(countTime);

        string str = time.ToString(@"mm\:ss");

        if(countTime <= 0)
        {
            countTime = 0;

            EndBabiesFactoryMiniGame();
        }

        GameCore.Instance.GetGameCanvasManager().BabiesFactoryTimer.transform.GetChild(0).GetComponent<Text>().text = str;

    }

    public void EndBabiesFactoryMiniGame()
    {
        var bullets = FindObjectsOfType<ColorMachineBullet>();
        if(bullets.Length > 0)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                Destroy(bullets[i].gameObject);
            }
        }

        AllosiusDev.AudioManager.Play(sfxBabiesSort.sound);

        babiesMakeCtrl.babiesSort = true;
        GameCore.Instance.BabiesCameraCtrl.SetPlayer(babiesMakeCtrl.transform.parent.gameObject);
    }


    #endregion
}
