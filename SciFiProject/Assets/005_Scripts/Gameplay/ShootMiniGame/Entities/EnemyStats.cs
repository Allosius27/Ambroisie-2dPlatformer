using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    #region Properties

    public float Health { get; protected set; }
    public float MaxHealth => maxHealth;
    public float Speed => speed;
    public float Damage => damage;
    public int PrestigePointsAtGained => prestigePointsAtGained;
    public int ExpJobAtGained => expJobAtGained;

    #endregion

    #region UnityInspector

    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private int prestigePointsAtGained;
    [SerializeField] private int expJobAtGained;

    #endregion

    private void Awake()
    {
        Health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            ShootMiniGameManager.Instance.prestigePointsGained += prestigePointsAtGained;
            ShootMiniGameManager.Instance.expPointsGained += expJobAtGained;

            ShootMiniGameManager.Instance.playerStats.ChangeShootJobExp(ShootMiniGameManager.Instance.expPointsGained);
            ShootMiniGameManager.Instance.expPointsGained = 0;

            GameCore.Instance.SetPrestigeAmount(ShootMiniGameManager.Instance.playerStats.ChangePrestigePoints(ShootMiniGameManager.Instance.prestigePointsGained));
            ShootMiniGameManager.Instance.prestigePointsGained = 0;

            Destroy(gameObject);
        }

    }
}
