using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AllosiusDev.AudioData pickupCollectibleSfx;

    public CollectibleSpawner collectibleSpawner { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity typeCollision = collision.gameObject.GetComponent<Entity>();

        if (typeCollision == null)
        {
            return;
        }

        if (typeCollision.type == Entity.Type.Player)
        {
            AllosiusDev.AudioManager.Play(pickupCollectibleSfx.sound);
            collectibleSpawner.RelaunchTimerSpawn();
            gameObject.SetActive(false);
        }
    }
}
