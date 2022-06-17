using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _spawnDropTime = 5.0f;
    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    void Update()
    {

    }

    IEnumerator EnemySpawnRoutine()
    {
        while(_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, transform.position + new Vector3(Random.Range(-7f, 7f), 7, 0),Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator PowerupSpawnRoutine()
    {
        while (_stopSpawning == false)
            {
                GameObject newPowerup = Instantiate(_tripleShotPowerupPrefab, transform.position + new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3, 8));
            }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
