using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    public int _powerupID = 0;

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
                            player._isShieldsUpActive = true;
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
