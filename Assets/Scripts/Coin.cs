using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private EquationChecker ScoreText;

    private void Start()
    {
        ScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<EquationChecker>();
    }

    private void Update()
    {
        gameObject.transform.Rotate(0, 0, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //ScoreText.ScorePlusOne();
        Destroy(gameObject);
    }
}
