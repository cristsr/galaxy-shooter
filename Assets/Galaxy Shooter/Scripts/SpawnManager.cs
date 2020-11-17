using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShip;
    
    [SerializeField] 
    private GameObject[] powerUps;

    private Coroutine _enemySpawn;
    private Coroutine _powerUpSpawn;

    // Start is called before the first frame update

    public void StartSpawning()
    {
        _enemySpawn = StartCoroutine(EnemySpawn());
        _powerUpSpawn = StartCoroutine(PowerUpSpawn());
    }

    public void StopSpawning()
    {
        StopCoroutine(_enemySpawn);
        StopCoroutine(_powerUpSpawn);
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            var xPos = Random.Range(-8, 8);
            Instantiate(enemyShip, new Vector3(xPos, 8f), Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator PowerUpSpawn()
    {
        while (true)
        {
            var randomPowerUp = Random.Range(0, powerUps.Length);
            var xPos = Random.Range(-8, 8);
            Instantiate(powerUps[randomPowerUp], new Vector3(xPos, 8f), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
