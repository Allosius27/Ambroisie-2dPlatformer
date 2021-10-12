using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    #region Properties

    public Animator Anim => anim;

    #endregion

    #region UnityInspector

    [SerializeField] private Animator anim;

    #endregion
}
