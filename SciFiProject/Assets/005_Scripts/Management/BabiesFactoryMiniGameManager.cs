using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabiesFactoryMiniGameManager : AllosiusDev.Singleton<BabiesFactoryMiniGameManager>
{
    #region Properties

    public GameObject ColorMachine => colorMachine;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject colorMachine;

    #endregion
}
