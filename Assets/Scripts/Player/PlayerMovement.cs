using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isMovingSideward { get; private set; } = false;
    public float distanceTravelled { get; private set; } = 0;
    private Vector3 Direction;
    public Rigidbody rigidBody;
    [SerializeField] public float sideMovementSpeed;
    [SerializeField] public float swipeSensitivity;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private int distanceTravelledToSpdIncrease=150;
    [SerializeField] private float spdIncrement = 0.25f;

    private bool hasInreasedSpeed = false;

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            isMovingSideward = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMovingSideward = false;
        }


        if (isMovingSideward)
        {
            Direction.x = Mathf.Lerp(Direction.x, Input.GetAxis("Mouse X"), Time.deltaTime * sideMovementSpeed);

            Direction = Vector3.ClampMagnitude(Direction, 1f);
        }
            this.transform.position += new Vector3(0f, 0f,  Time.deltaTime * forwardSpeed);
        
    }
    
    private void FixedUpdate()
    {
        distanceTravelled += forwardSpeed * Time.deltaTime;
        IncreaseSpeedOnDistanceTravelled();

        if (isMovingSideward)
            {
            Vector3 displacement = new Vector3(Direction.x,0f,0f) * Time.fixedDeltaTime;
            rigidBody.velocity = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSensitivity,0f,0f) + displacement;
            }
            else
            {
                rigidBody.velocity = Vector3.zero;
            }
        
    }

    private void IncreaseSpeedOnDistanceTravelled()
    {
        if (hasInreasedSpeed) return;
        if (distanceTravelled <= distanceTravelledToSpdIncrease) return;
        if ((Mathf.RoundToInt(distanceTravelled)) % distanceTravelledToSpdIncrease == 0)
            forwardSpeed += spdIncrement;
        hasInreasedSpeed = true;
        StartCoroutine(IncreaseSpeedCooldown());
    }

    IEnumerator IncreaseSpeedCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        hasInreasedSpeed = false;
    }
}
