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
    public int PrestigePoints => prestigePoints;

    public float Strength => strength;

    #endregion

    #region UnityInspector

    [SerializeField] private float mood;
    [SerializeField] private float health;
    [SerializeField] private int prestigePoints;

    [Space]

    [SerializeField] private float strength;

    #endregion

    #region Behaviour

    private void Start()
    {
        GameCore.Instance.SetPrestigeAmount(prestigePoints);
    }

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

    public int ChangePrestigePoints(int amount)
    {
        this.prestigePoints += amount;
        return this.prestigePoints;
    }

    #endregion
}
