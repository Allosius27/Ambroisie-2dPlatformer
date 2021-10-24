using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public Collectible collectible;

    public float timeToRespawn = 30.0f;

    private void Start()
    {
        ActiveCollectible();
    }

    public void RelaunchTimerSpawn()
    {
        StartCoroutine(TimerSpawn());
    }

    public IEnumerator TimerSpawn()
    {
        yield return new WaitForSeconds(timeToRespawn);

        ActiveCollectible();
    }

    public void ActiveCollectible()
    {
        collectible.gameObject.SetActive(true);
        collectible.collectibleSpawner = this;
    }

    /*public void SpawnCollectible()
    {
        Collectible collectible = Instantiate(prefabCollectible);
        collectible.transform.SetParent(this.transform);
        collectible.transform.localPosition = Vector3.zero;
        collectible.transform.rotation = Quaternion.identity;
        collectible.collectibleSpawner = this;
    }*/
}
