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
    [SerializeField] CameraShake cameraShake;

    private Coroutine raycastCoroutine;
    public bool isApproachingObstacle { get; private set; } = false;
    public bool isImmuneToEquation = false;
    public int currentSum { get; private set; } = 0;
    public int correctAnswers { get; private set; } = 0;
    public int currentAdditive;
    public float distanceToObstacle { get; private set; } = 0;

    public Obstacle correctObstacle { get; private set; } = new Obstacle();
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
            cameraShake.ShakeCamera();
            
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 30, obstacleLayerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Mathf.Infinity, Color.green);
            Obstacle obstacleDetected = hit.collider.GetComponent<Obstacle>();
            distanceToObstacle = Vector3.Distance(this.transform.position, hit.transform.position);
            UpdateTextEquation(obstacleDetected);
            isApproachingObstacle = true;
            if (obstacleDetected.numberValue == (this.currentSum+obstacleDetected.additive)) this.correctObstacle = obstacleDetected;
        }
        //shoot raycast left
        if (Physics.Raycast(transform.position, transform.TransformDirection(-0.25f,0,1), out hit, 30, obstacleLayerMask))
        {
            Obstacle obstacleDetected = hit.collider.GetComponent<Obstacle>();
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-0.25f, 0, 1)) * 1000, Color.green);
            UpdateTextEquation(obstacleDetected);
            isApproachingObstacle = true;
            if (obstacleDetected.numberValue == (this.currentSum + obstacleDetected.additive)) this.correctObstacle = obstacleDetected;
        }
        //shoot raycast at right
        if (Physics.Raycast(transform.position, transform.TransformDirection(0.25f, 0, 1), out hit, 30, obstacleLayerMask))
        {
            Obstacle obstacleDetected = hit.collider.GetComponent<Obstacle>();
            UpdateTextEquation(obstacleDetected);
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0.25f, 0, 1)) * 1000, Color.green);
            isApproachingObstacle = true;

            if (obstacleDetected.numberValue == (this.currentSum + obstacleDetected.additive)) this.correctObstacle = obstacleDetected;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0.25f,0,1)) * 1000, Color.white);
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-0.25f,0,1)) * 1000, Color.white);
            isApproachingObstacle = false;
        }
    }
  

    private void UpdateTextEquation(Obstacle obstacleDetected)
    {
        textObject.text = currentSum.ToString() + " + " + obstacleDetected.additive.ToString();
    }
}
