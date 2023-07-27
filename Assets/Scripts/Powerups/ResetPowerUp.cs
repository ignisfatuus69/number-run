using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPowerUp : PowerUp
{
    System.Action OnResetPowerUp;
    [SerializeField] int deductive=5;
    [SerializeField] int min=5;
    [SerializeField] int max=20;
    [SerializeField] Text text;
    [SerializeField] GameObject particleEffect;
    [SerializeField] AudioClip resetSFX;

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
        Effect(other);
        Instantiate(particleEffect, transform.position, transform.rotation);
        AudioManager.Instance.PlayOneShot(resetSFX);
    }

    protected override void Effect(Collider other)
    {
        OnResetPowerUp?.Invoke();
        OnGameObjectCollision?.Invoke(this.gameObject);
        other.GetComponentInChildren<EquationChecker>().AddSum(deductive);
        this.gameObject.SetActive(false);
    }
}
