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

    [SerializeField] private PlayerStats playerStats;

    #endregion

    public void ShootAnim()
    {
        anim.SetTrigger("shoot");
    }

    public void PlayerResurect()
    {
        playerStats.PlayerResurect();
    }
}
