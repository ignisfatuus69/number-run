using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject playAgainButton;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject spawners;
    public void GameOver()
    {
        player.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        spawners.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        SceneManager.LoadScene("SampleScene");
        playAgainButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        spawners.gameObject.SetActive(true);
    }
}
