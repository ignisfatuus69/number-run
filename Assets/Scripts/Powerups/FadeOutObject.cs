using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutObject : MonoBehaviour
{
    [SerializeField] string powerUpName;
    public float powerUpDuration;
    private float secondsUntilFade;
    private float rateOfFade;
    public float startingRateOfFade=0.2f;
    private float currentObjectTime;
    [SerializeField] MeshRenderer objectToFade;
    [SerializeField] PowerUpTracker powerUpTrackerObj;

    private void Start()
    {
        powerUpTrackerObj.OnPowerUpAdded += StartFadeCountdown;
    }
    private void OnEnable()
    {
        //StopAllCoroutines();
        //secondsUntilFade = powerUpDuration /3;
        //rateOfFade = startingRateOfFade;
        //currentObjectTime = powerUpDuration;
        //StartCoroutine(CountDown());
    }

    public void StartFadeCountdown(PowerUp powerup)
    {
        if (!powerUpTrackerObj.ContainsPowerUp(powerUpName)) return;
        secondsUntilFade = powerUpDuration / 3;
        rateOfFade = startingRateOfFade;
        currentObjectTime = powerUpDuration;
        StartCoroutine(CountDown());
        objectToFade.enabled = true;
    }

    IEnumerator CountDown()
    {
        while (currentObjectTime > secondsUntilFade)
        {
            currentObjectTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(FadeObject());
        yield return new WaitForSeconds(secondsUntilFade);
        StopAllCoroutines();
        objectToFade.enabled = false;
    }

    IEnumerator FadeObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(rateOfFade);
            objectToFade.enabled = true;
            yield return new WaitForSeconds(rateOfFade);
            Debug.Log("testing");
            objectToFade.enabled = false;
        }
    }
}
