using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject pausePanel;
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }


    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
