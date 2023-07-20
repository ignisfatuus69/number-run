using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquationChecker : MonoBehaviour
{
    [SerializeField] AudioClip correctSFX;
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Text textObject;
    public int currentSum { get; private set; } = 0;
    public int correctAnswers { get; private set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckEquation(int additive, int obstacleNumberValue)
    {
        currentSum += additive;
       // textObject.text = currentSum.ToString();
        if (currentSum==obstacleNumberValue)
        {
            correctAnswers += 1;
            AudioManager.Instance.PlayOneShot(correctSFX);
        }
        else
        {
            gameManager.GameOver();
        }
    }

}
