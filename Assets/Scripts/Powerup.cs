using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupID = 0;
    private Player _player;

    [SerializeField]
    private AudioClip _powerupSound;

    void Start ()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.0f) {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                Player player = other.transform.GetComponent<Player>();
                AudioSource.PlayClipAtPoint(_powerupSound, transform.position);

                if (player != null)
                {
                    switch (_powerupID)
                    {
                        case 0:
                            player.TripleShotActive();
                            break;
                        case 1:
                            player.SpeedBoostActive();
                            break;
                        case 2:
                            player.ShieldsUpActive();
                            break;
                        default:
                            player.TripleShotActive();
                            break;
                    }
                }
                Destroy(this.gameObject);
            }
        }
}
