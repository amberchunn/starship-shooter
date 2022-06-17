using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _speed = 4.5f;
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7.0f)
        {
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
                        player.TripleShotActive();
                        Destroy(this.gameObject);
                    }
            }
        }
}
