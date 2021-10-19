using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCtrl : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private Animator anim;

    [SerializeField] private int layerIndex;
    [SerializeField] private int transitionAnimIndex;

    #endregion

    private void Start()
    {
        anim.SetLayerWeight(layerIndex, 1);
        anim.SetInteger("Transition", transitionAnimIndex);
        anim.SetTrigger("ChangeAnim");
    }
}
