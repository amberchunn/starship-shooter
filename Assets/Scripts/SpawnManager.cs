using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyFireContainer;
    [SerializeField]
    private GameObject _spawnManager;
    [SerializeField]
    private GameObject _enemyPrefab;
    private float _enemyDropTime = 5.0f;
    private float _spawnDelay = 2.5f;
    [SerializeField]
    private GameObject[] _powerups;
    private bool _stopSpawning = false;

    public void StartSpawning ()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_spawnDelay);
        while(_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, transform.position + new Vector3(Random.Range(-7f, 7f), 7, 0),Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(_enemyDropTime);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        yield return new WaitForSeconds(_spawnDelay);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7f, 7f), 7, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], new Vector3 (Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds (Random.Range (3, 8));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
