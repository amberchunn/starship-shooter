using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.5f;
    private int _speedMultiplier = 2;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private int _lives = 3;
    private float _canFire = -1f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    // [SerializeField]
    // private GameObject _speedBoostPrefab;
    // [SerializeField]
    // private GameObject _shieldsUpPrefab;
    private SpawnManager _spawnManager;
    public bool _isTripleShotActive = false;
    public bool _isSpeedBoostActive = false;
    public bool _isShieldsUpActive = false;
    void Start()
    {
        // Start player at center
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

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
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed = _speed * _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine ()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = _speed / _speedMultiplier;
        _isSpeedBoostActive = false;
    }
    public void ShieldsUpActive()
    {
        _isShieldsUpActive = true;
        Debug.Log("Shields Up are Active");
        StartCoroutine(ShieldsUpPowerDownRoutine());
    }
    IEnumerator ShieldsUpPowerDownRoutine ()
    {
        yield return new WaitForSeconds(5.0f);
        _isShieldsUpActive = false;
    }
}