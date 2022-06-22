using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -10f)
        {
            float randomX = Random.Range(-5f, 5f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    // When Enemy enters the scene and collides with 'other' object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the 'other' object is the player
        if (other.tag == "Player")
        {
            // Get the player component
            Player player = other.transform.GetComponent<Player>();

            // If you have a player component
            if (player != null)
            {
                // Call the Damage() method on the player object
                player.Damage();
            }
            // After handling the object collided with, Destroy Enemy (aka me - the object this script is attached to)
            Destroy(this.gameObject);
        }

        // If the 'other' object is a laser
        if (other.tag == "Laser")
        {
            // Grab the laser component
            Laser laser = other.transform.GetComponent<Laser>();

            // If you have a laser component
            if (laser != null)
            {
                // Destroy me
                Destroy(other.gameObject);
            }
        }
        // Make sure Enemy is destroyed if they've collided into anything
        Destroy(this.gameObject);
    }
}