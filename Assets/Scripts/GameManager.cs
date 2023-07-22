using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject textObj;
    [SerializeField] GameObject playAgainButton;
    public void GameOver()
    {
        player.gameObject.SetActive(false);
        textObj.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        playAgainButton.gameObject.SetActive(false);
    }
}
