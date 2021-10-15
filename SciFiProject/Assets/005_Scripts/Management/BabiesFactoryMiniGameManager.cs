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
    public ColorMachine ColorMachine => colorMachine;

    public ColorsTouchs ColorsTouchs => colorsTouchs;
    public ColorsCapsules ColorsCapsules => colorsCapsules;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject colorTapis;
    [SerializeField] private float colorTapisMoveSpeed;

    [Space]

    [SerializeField] private ColorMachine colorMachine;

    [SerializeField] private ColorsTouchs colorsTouchs;

    [SerializeField] private ColorsCapsules colorsCapsules;

    [SerializeField] private float countTime;

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        baseCountTime = countTime;

        baseColorTapisPosition = colorTapis.transform.position;

        baseColorTapisMoveSpeed = colorTapisMoveSpeed;
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
        if(colorsCapsules.currentIndexColorCapsule >= colorsCapsules.ListColorsCapsules.Count)
        {
            colorsCapsules.currentIndexColorCapsule = colorsCapsules.ListColorsCapsules.Count - 1;
            return;
        }

        colorTapisTarget = colorsCapsules.ListColorsCapsules[colorsCapsules.currentIndexColorCapsule].transform;
        colorTapisMoving = true;
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
            if (Vector3.Distance(colorTapis.transform.position, colorTapisTarget.position) < 0.3f)
            {
                colorTapisMoveSpeed = 0;
                colorTapisMoving = false;
            }
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

        GameCore.Instance.SetStateBabiesMiniGame(false);
    }


    #endregion
}
