using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float currentPlayerX = transform.position.x;
        float currentPlayerY = transform.position.y;
        float xBoundLeft = -11.3f;
        float xBoundRight = 11.3f;
        float yBoundTop = 4.5f;
        float yBoundBottom = -4.5f;

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) *_speed * Time.deltaTime);

        if (currentPlayerY > yBoundTop) {
            transform.position = new Vector3(currentPlayerX, yBoundBottom, 0);
        } else if (currentPlayerY < yBoundBottom) {
            transform.position = new Vector3(currentPlayerX, yBoundTop, 0);
        }

        if (currentPlayerX < xBoundLeft) {
            transform.position = new Vector3 (xBoundRight, currentPlayerY, 0);
        } else if (currentPlayerX > xBoundRight) {
            transform.position = new Vector3 (xBoundLeft, currentPlayerY, 0);
        }
    }

    void FireLaser()
    {
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;
        if (_lives < 1) {
            Destroy(this.gameObject);
        }
    }
}