using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Fields

    #endregion

    #region Properties

    public int Mood => mood;
    public int Health => health;

    #endregion

    #region UnityInspector

    [SerializeField] private int mood;
    [SerializeField] private int health;

    #endregion

    #region Behaviour

    public int ChangeHealth(int amount)
    {
        this.health += amount;
        return this.health;
    }

    public int ChangeMood(int amount)
    {
        this.mood += amount;
        return this.mood;
    }


    #endregion
}
