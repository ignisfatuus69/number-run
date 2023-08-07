using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldPowerUp : PowerUp
{
    public System.Action<GameObject> OnShieldEnded;
    public bool isActive { get; private set; } = false;
    [SerializeField] float distanceBeforeActivation;
    [SerializeField] float duration;
    [SerializeField] GameObject particleEffect;
    [SerializeField] AudioClip powerUpSFX;
    [SerializeField] MeshRenderer meshRenderer;
    //Have these values set by the spawners

    [HideInInspector]public GameObject shieldObj;
    public EquationChecker equationCheckerObj;
    float currentDuration;

    private void OnEnable()
    {
        meshRenderer.enabled = true;
    }
    protected override void Effect(Collider other)
    {
        //stop all coroutines incase we have a current powerup on going
        isActive = true;
        StopAllCoroutines();
        currentDuration = duration;
        base.Effect(other);
        shieldObj.gameObject.SetActive(true);
        equationCheckerObj.isImmuneToEquation = true;
        StartCoroutine(DestroyObstacleWithLazer());
        StartCoroutine(CountDownToDuration());
    }

    private void OnTriggerEnter(Collider other)
    {
        Effect(other);
        meshRenderer.enabled = false;
        Instantiate(particleEffect, transform.position, transform.rotation);
        AudioManager.Instance.PlayOneShot(powerUpSFX);
    }

   // IEnumerator
    IEnumerator CountDownToDuration()
    {
        while (currentDuration > 0)
        {
            yield return new WaitForSeconds(0.1f);
            currentDuration -= 0.1f;
            if (currentDuration <= 0)
            {
                Debug.Log("Shield has worn off");
                equationCheckerObj.isImmuneToEquation = false;
                OnShieldEnded?.Invoke(this.gameObject);
                shieldObj.gameObject.SetActive(false);
                isActive = false;
                StopAllCoroutines();
            }
        }
    }

    IEnumerator DestroyObstacleWithLazer()
    {
        while (currentDuration > 0)
        {
            yield return new WaitForSeconds(0.1f);
            if (!equationCheckerObj.isImmuneToEquation) yield return null;
            if (equationCheckerObj?.correctObstacle != null)
            {
                if (equationCheckerObj.correctObstacle.isActiveAndEnabled)
                {
                    Vector2 TargetDirection = ((equationCheckerObj.correctObstacle.transform.position) -(equationCheckerObj.transform.position)).normalized;
                    Debug.DrawLine(equationCheckerObj.transform.position, equationCheckerObj.transform.TransformDirection(new Vector3(TargetDirection.x,TargetDirection.y,-1f)) , Color.red, 5f);
                    Debug.Log(equationCheckerObj.transform.TransformDirection(new Vector3(TargetDirection.x, TargetDirection.y, 1f)));
                    yield return new WaitForSeconds(1f);
                    equationCheckerObj.correctObstacle.PlayerInteraction(equationCheckerObj);
                    Debug.Log("Destroy obstacle");
                }
            }
        }
    }
}
