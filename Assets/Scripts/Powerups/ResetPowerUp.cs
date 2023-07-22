using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResetPowerUp : MonoBehaviour
{
    [SerializeField] int deductive=5;
    [SerializeField] int min=5;
    [SerializeField] int max=20;
    [SerializeField] Text text;

    private void OnEnable()
    {
        initializeDeductive();
    }
    public void initializeDeductive()
    {
        deductive = Random.Range(min, max + 1);
        deductive *= -1;
        text.text = deductive.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInChildren<EquationChecker>().AddSum(deductive);
        this.gameObject.SetActive(false);
    }
}
