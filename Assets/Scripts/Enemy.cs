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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {
            Laser laser = other.transform.GetComponent<Laser>();
            if (laser != null)
            {
            Destroy(other.gameObject);
            }
        }
            Destroy(this.gameObject);
    }
}