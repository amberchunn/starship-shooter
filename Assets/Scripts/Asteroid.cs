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

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _boomSound;
    void Start ()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * ( _rotationSpeed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_boomSound, transform.position);

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
