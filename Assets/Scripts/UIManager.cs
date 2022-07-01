using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _resetLevelText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private SpawnManager _spawnManager;

    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if (_gameManager == null)
        {
            Debug.Log("Game Manager Not Found");
        }
    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score " + playerScore.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];

        if (currentLives < 1)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _resetLevelText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        yield return new WaitForSeconds(2.5f);

        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);

            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}