using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutObject : MonoBehaviour
{
    public int objectTime;
    public int secondsUntilFade;
    public float rateOfFade;
    public float startingRateOfFade=0.2f;
    private float currentObjectTime;
    [SerializeField] MeshRenderer objectRenderer;
    private void OnEnable()
    {
        rateOfFade = startingRateOfFade;
        currentObjectTime = objectTime;
        StartCoroutine(FadeObject());
    }

    public void CountDownToFade()
    {

    }

    IEnumerator CountDown()
    {
        while (currentObjectTime > secondsUntilFade)
        {
            currentObjectTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(FadeObject());
    }

    IEnumerator FadeObject()
    {
        while (true)
        {
            objectRenderer.enabled = false;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("test");
            objectRenderer.enabled = true;
        }
    }
}
