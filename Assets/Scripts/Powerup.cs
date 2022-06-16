using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    void Start()
    {

    }

    void Update()
    {
        // Move down at speed of 3
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // When leave screen, destroy us
        if (transform.position.y > 8f) {
            Destroy(this.gameObject);
        }
        // OnTriggerCollision
        // Only player can collect
    }
}
