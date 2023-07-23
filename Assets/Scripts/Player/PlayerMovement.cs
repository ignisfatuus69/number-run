using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public bool isMovingSideward { get; private set; } = false;
    public float distanceTravelled { get; private set; } = 0;
    public Vector3 Direction;
    public Rigidbody rigidBody;
    public float sideMovementSpeed;
    [SerializeField] private float swipeSensitivity;
    [SerializeField] private float forwardSpeed;
    private int xAxis = 0;
    void Update()
    {


        //if (Input.GetMouseButtonDown(0))
        //{
        //    isMovingSideward = true;
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    isMovingSideward = false;
        //}


        if (isMovingSideward)
        {
            Debug.Log("Mouse X:" + Input.GetAxis("Mouse X"));
            //Direction.x = UnityEngine.InputSystem.EnhancedTouch.Touch.fingers[0].screenPosition.x * sideMovementSpeed*Time.deltaTime;
            //   Direction.x = Mathf.Lerp(Direction.x, UnityEngine.InputSystem.EnhancedTouch.Touch.fingers[0].screenPosition.x, Time.deltaTime * sideMovementSpeed);

            Direction = Vector3.ClampMagnitude(Direction, 1f);
        }
            this.transform.position += new Vector3(0f, 0f,  Time.deltaTime * forwardSpeed);
        
    }

    public void MovePlayer()
    {
        isMovingSideward = true;
    }

    public void StopPlayer()
    {
        isMovingSideward = false;
    }

    private void FixedUpdate()
    {
      
            if (isMovingSideward)
            {
            Vector3 displacement = new Vector3(Direction.x,0f,0f) * Time.fixedDeltaTime;
            distanceTravelled += forwardSpeed * Time.deltaTime;
            rigidBody.velocity = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSensitivity,0f,0f) + displacement;
            }
            else
            {
                rigidBody.velocity = Vector3.zero;
            }
        
        
    }
}
