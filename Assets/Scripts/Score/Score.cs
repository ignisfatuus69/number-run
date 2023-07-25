    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private int scoreAmount;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] CoinInventory coinInventory;
    [SerializeField] EquationChecker equationChecker;
    [SerializeField] Text scoreText;
    // Update is called once per frame
    void Update()
    {
        UpdateScore();   
    }

    private void UpdateScore()
    {
        scoreAmount = Mathf.RoundToInt(playerMovement.distanceTravelled) + (equationChecker.correctAnswers*10) + coinInventory.coinAmount;
        scoreText.text = "Score:" + scoreAmount.ToString();
    }
}
