using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ItemPowers : MonoBehaviour
{
    private int _powerID = 0;
    private Player _player;


    void Start ()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

    }
    void Update() {
        // Move power item left at constant speed
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        // If the player collides with the power item
            if (other.tag == "Player") {

                // Grab reference to player object
                Player player = other.transform.GetComponent<Player>();

                // Switch statement checks each _powerID
                // If the _powerID is equal to the power item ID
                // Call corresponding method on the player object
                if (player != null) {
                    switch (_powerID) {
                        case 0:
                            player.bigMushroom();
                            break;
                        case 1:
                            player.starmanActive();
                            break;
                        case 2:
                            player.fireFlowerActive();
                            break;
                        default:
                            player.oneUpMushroom();
                            break;
                    }
                }
        // If player never collides with power item destroy power item
                Destroy(this.gameObject);
            }
        }
}
