using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquationChecker : MonoBehaviour
{
    public System.Action OnCurrentSumDeducted;
    public System.Action OnCurrentSumAdded;
    [SerializeField] AudioClip correctSFX;
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Text textObject;
    public bool isImmuneToEquation = false;
    public int currentSum { get; private set; } = 0;
    public int correctAnswers { get; private set; } = 0;
    public int currentAdditive;
    public void AddSum(int additive)
    {
        currentSum += additive;
        //cap sum
        if (currentSum < 0) currentSum = 0;
        textObject.text = (currentSum.ToString());
        if (additive>0) OnCurrentSumAdded?.Invoke();
        if (additive < 0) OnCurrentSumDeducted?.Invoke();

    }

    public void CheckEquation(int additive, int obstacleNumberValue, int obstacleAdditive)
    {
        AddSum(additive);
        textObject.text = currentSum.ToString();
        if (isImmuneToEquation) return;
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
