using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionPowerUp : PowerUp
{
    public System.Action<GameObject> OnSlowMotionEnded;
    [SerializeField] float distanceBeforeActivation = 5;
    [SerializeField] float slowDownValue;
    [SerializeField] public EquationChecker equationCheckerObj;
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] GameObject particleEffect;
    [SerializeField] AudioClip powerUpSFX;
    public bool isActive { get; private set; } = false;
    float originalSpeedValue;
    [SerializeField] MeshRenderer meshRenderer;
    private void OnEnable()
    {
        isActive = false;
        meshRenderer.enabled = true;
        currentDuration = duration;
    }

    IEnumerator EnableSlowMotion()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(0.1f);
            if (equationCheckerObj.isApproachingObstacle )
            {
                if (equationCheckerObj.distanceToObstacle > 0 && equationCheckerObj.distanceToObstacle <= distanceBeforeActivation)
                {
                    Time.timeScale = slowDownValue;
                   Time.fixedDeltaTime = Time.timeScale * 0.01f;
                    playerMovement.sideMovementSpeed = 100;
                    playerMovement.swipeSensitivity = 1250;
                }
                
            }
            else if (!equationCheckerObj.isApproachingObstacle)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
                playerMovement.sideMovementSpeed = 50;
                playerMovement.swipeSensitivity = 500;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Effect(other);
        meshRenderer.enabled = false;
        Instantiate(particleEffect, transform.position, transform.rotation);
        AudioManager.Instance.PlayOneShot(powerUpSFX);
    }
    protected override void Effect(Collider other)
    {
        base.Effect(other);
        isActive = true;
        StartCoroutine(EnableSlowMotion());
        StartCoroutine(CountDownToDuration());
    }

    IEnumerator CountDownToDuration()
    {
        while (currentDuration > 0)
        {
            yield return new WaitForSeconds(0.1f);
            currentDuration -= 0.1f;

            if (currentDuration <= 0)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
                equationCheckerObj.isImmuneToEquation = false;
                OnSlowMotionEnded?.Invoke(this.gameObject);
                StopAllCoroutines();
                isActive = false;
                playerMovement.sideMovementSpeed = 50;
                playerMovement.swipeSensitivity = 500;
                OnPowerUpEnded?.Invoke(this);
            }
        }
    }

}
