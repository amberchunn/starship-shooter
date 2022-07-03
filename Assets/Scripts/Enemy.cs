using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    private Animator _anim;
    public GameObject _explosionPrefab;
    [SerializeField]
    private AudioClip _boomSound;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player Object Not Found");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animation Object Not Found");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -10f)
        {
            float randomX = Random.Range(-5f, 5f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource.PlayClipAtPoint(_boomSound, transform.position);

        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = .25f;
            Destroy(this.gameObject, 2.25f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                int _enemyPoints = Random.Range(3, 20);
                _player.IncreaseScore(_enemyPoints);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = .25f;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.25f);
        }


    }
}