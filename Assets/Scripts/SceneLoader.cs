using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Title Screen");
        Time.timeScale = 1;
    }
}
