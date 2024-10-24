using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject BubblePrefabs;
    public Transform RootBubble;
    public float SpawmTime = 2;
    public Vector2 SpawnArenaMin;
    public Vector2 SpawnArenaMax;
    public int bubbleCount = 10;
    public void Start()
    {
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();
        audioManager.ChangeBackGroundMusic(2);
        StartCoroutine(SpawnBubbles());        
    }
    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            Vector2 spawnPotision = new Vector2
                (
                Random.Range(SpawnArenaMin.x, SpawnArenaMax.x),
                Random.Range(SpawnArenaMin.y, SpawnArenaMax.y)
                );

            Instantiate(BubblePrefabs, spawnPotision, Quaternion.identity,RootBubble);
            
            yield return new WaitForSeconds(SpawmTime);            
        }
    }
}
