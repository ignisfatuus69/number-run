using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationChecker : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerMovement playerMovement;
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
        if (currentSum==obstacleNumberValue)
        {
            correctAnswers += 1;
        }
        else
        {
            gameManager.GameOver();
        }
    }

}
