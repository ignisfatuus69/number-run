using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 Direction;
    public Rigidbody rigidBody;
    [SerializeField] private float sideMovementSpeed;
    [SerializeField] private float swipeSensitivity;
    [SerializeField] private float forwardSpeed;

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {

            Direction.x = Mathf.Lerp(Direction.x, Input.GetAxis("Mouse X"), Time.deltaTime * sideMovementSpeed);

            Direction = Vector3.ClampMagnitude(Direction, 1f);

            this.transform.position = new Vector3(0f, 0f, Mathf.SmoothStep(this.transform.position.z, +100f, Time.deltaTime * forwardSpeed));
        }
    }
    
    private void FixedUpdate()
    {
      
            if (isMoving)
            {
                Vector3 displacement = new Vector3(Direction.x,0f,0f) * Time.fixedDeltaTime;
            
                
                    rigidBody.velocity = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSensitivity,0f,0f) + displacement;
            }
            else
            {
                rigidBody.velocity = Vector3.zero;
            }
        
        
    }
}