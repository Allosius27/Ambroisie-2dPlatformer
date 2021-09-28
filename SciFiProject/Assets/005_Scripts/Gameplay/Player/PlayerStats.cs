using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Fields

    #endregion

    #region Properties

    public float Mood => mood;
    public float Health => health;

    #endregion

    #region UnityInspector

    [SerializeField] private float mood;
    [SerializeField] private float health;

    #endregion

    #region Behaviour

    public float ChangeHealth(float amount)
    {
        this.health += amount;
        return this.health;
    }

    public float ChangeMood(float amount)
    {
        this.mood += amount;
        return this.mood;
    }


    #endregion
}
