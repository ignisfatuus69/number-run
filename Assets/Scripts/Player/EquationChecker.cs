using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquationChecker : MonoBehaviour
{
    public System.Action<int> OnCurrentSumDeducted;
    public System.Action<int> OnCurrentSumAdded;
    [SerializeField] AudioClip correctSFX;
    [SerializeField] GameManager gameManager;
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Text textObject;
    [SerializeField] LayerMask obstacleLayerMask;

    private Coroutine raycastCoroutine;
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
        if (additive>0) OnCurrentSumAdded?.Invoke(additive);
        if (additive < 0) OnCurrentSumDeducted?.Invoke(-additive);

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
            CameraShake.Instance.ShakeCamera(5f, .1f);
        }
        else
        {
            gameManager.GameOver();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20, obstacleLayerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            UpdateTextEquation();
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }
  

    private void UpdateTextEquation()
    {
        textObject.text = currentSum.ToString() + " + " + currentAdditive.ToString();
    }
}
