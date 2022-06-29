using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;

    private void Update()
    {
        if (_isGameOver == true && Input.GetKeyDown(KeyCode.R))
            {
                // Current Game Scene
                SceneManager.LoadScene(1);
            }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }

}
