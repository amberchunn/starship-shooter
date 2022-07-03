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
    [SerializeField]
    private int _score = 0;
    private UIManager _uiManager;
    private float _canFire = -1f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _shieldVisual;
    private bool _isShieldsUpActive = false;
    [SerializeField]
    private GameObject[] _engineFailure;
    private int effectedEngine;
    [SerializeField]
    private GameObject _explosionPrefab;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserSound;
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if(_spawnManager == null || _uiManager == null || _audioSource)
        {
            Debug.LogError("Object Not Found");
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

        AudioSource.PlayClipAtPoint(_laserSound, transform.position);
    }

    public void Damage()
    {
        if (_isShieldsUpActive == true) {
        {
            _shieldVisual.SetActive(false);
            _isShieldsUpActive = false;
            return;
        }
    }
        _lives--;

        // Damage Levels

        switch (_lives)
        {
            case 3:
                break;
            case 2:
                effectedEngine = Random.Range(0,2);
                _engineFailure[effectedEngine].SetActive(true);
                break;
            case 1:
                if (effectedEngine == 0)
                {
                    _engineFailure[1].SetActive(true);
                } else
                {
                    _engineFailure[0].SetActive(true);
                }
                break;
            case 0:
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                _spawnManager.OnPlayerDeath();
                break;
        }

        _uiManager.UpdateLives(_lives);

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
        _speed = _speed * _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine ()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = _speed / _speedMultiplier;
    }
    public void ShieldsUpActive()
    {
        _isShieldsUpActive = true;
        _shieldVisual.SetActive(true);
    }
    public void IncreaseScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}