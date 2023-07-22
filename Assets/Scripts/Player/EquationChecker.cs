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
    public int currentAdditive;
    public void AddSum(int additive)
    {
        currentSum += additive;
        //cap sum
        if (currentSum < 0) currentSum = 0;
        textObject.text = (currentSum.ToString());
    }

    public void CheckEquation(int additive, int obstacleNumberValue, int obstacleAdditive)
    {
        currentSum += additive;
        textObject.text = currentSum.ToString();
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
