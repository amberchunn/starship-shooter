using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.5f;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private int _lives = 3;
    private float _canFire = -1f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool _isTripleShotActive = false;
    private SpawnManager _spawnManager;
    // private PowerupManager _powerupManager;

    void Start()
    {
        // Start player at center
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        // _powerupManager = GameObject.Find("Powerup Manager").GetComponent<PowerupManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Out of Order");
        }
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
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

    }

    public void Damage()
    {
        _lives--;
        if (_lives < 1) {
            // _powerupManager.SetPowerupSpawn();
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive() {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
}