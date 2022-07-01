using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private float _rotationSpeed;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;


    void Start ()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * ( _rotationSpeed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        Destroy(other.gameObject, -2f);

        if (other.tag == "Laser")
        {
            _spawnManager.StartSpawning();
        }
        if (other.tag == "Player")
        {
            _uiManager.UpdateLives(0);
            _spawnManager.OnPlayerDeath();
        }

        Destroy(this.gameObject, -2.5f);

    }
}
