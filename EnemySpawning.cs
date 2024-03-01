using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    [SerializeField]
    private float spawnCooldown;
    [SerializeField]
    private float spawnCooldownTick;
    [SerializeField]
    private float minSpawnCooldown = 2.5f;

    public GameObject spawns;

    public GameObject enemyFolder;
    public GameObject[] enemies;

    void Start() {
        StartCoroutine(EnemySpawnLoop());
    }

    IEnumerator EnemySpawnLoop() {
        //spawns an enemy in a random location and waits every interval (cooldown)
        while (true) {
            yield return new WaitForSeconds(spawnCooldown);

            int randomEnemy = Random.Range(0, enemies.Length);
            int randomSpawn = Random.Range(0, spawns.transform.childCount);

            SpawnEnemy(enemies[randomEnemy], spawns.transform.GetChild(randomSpawn).gameObject);

            DecreaseCooldown();
        }
    }

    void SpawnEnemy(GameObject enemy, GameObject spawn) {
        //spawns an enemy at a certain spawn location
        Instantiate(enemy, spawn.transform.position, Quaternion.identity, enemyFolder.transform);
    }

    void DecreaseCooldown() {
        //decreases the cooldown by the tick count and follows a minimum cooldown
        if(minSpawnCooldown < spawnCooldown) {
            spawnCooldown -= spawnCooldownTick;
        }
        else {
            spawnCooldown = minSpawnCooldown;
        }
    }
}
