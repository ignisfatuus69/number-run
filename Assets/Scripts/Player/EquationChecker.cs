using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquationChecker : MonoBehaviour
{
    public System.Action OnCurrentSumDeducted;
    public System.Action OnCurrentSumAdded;
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

    private void Start()
    {
        obstacleSpawner.OnObstaclesSpawn += EnableObstacleRaycast;
    }
    public void AddSum(int additive)
    {
        currentSum += additive;
        //cap sum
        if (currentSum < 0) currentSum = 0;
        textObject.text = (currentSum.ToString());
        if (additive>0) OnCurrentSumAdded?.Invoke();
        if (additive < 0) OnCurrentSumDeducted?.Invoke();

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
        }
        else
        {
            gameManager.GameOver();
        }
    }
    private void FixedUpdate()
    {
        //RaycastHit hit
        //// Does the ray intersect any objects excluding the player layer
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, obstacleLayerMask))
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
        //    Debug.Log("Did Hit");
        //    UpdateTextEquation();
        //    StopCoroutine(raycastCoroutine);
        //}
        //else
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        //    Debug.Log("Did not Hit");
        //}
    }
    private void EnableObstacleRaycast()
    {
        Debug.Log("Activate raycast");
        raycastCoroutine = StartCoroutine(ActivateObstacleRayCast());
    }
    IEnumerator ActivateObstacleRayCast()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, obstacleLayerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Did Hit");
                UpdateTextEquation();
                StopCoroutine(raycastCoroutine);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }

    private void UpdateTextEquation()
    {
        textObject.text = currentSum.ToString() + " + " + currentAdditive.ToString();
    }
}
