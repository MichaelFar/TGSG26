using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;

    //gravity value
    public float gravity = -9.81f;

    //for the ground check
    public Transform groundCheck;
    //makes invisible sphere at bottom of player 
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    

    // Update is called once per frame
    void Update()
    {
        //Makes a sphere that checks for the groundDistance and if collides with anything within the groundMask then isGrounded is true
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //gets the input for horizontal and vertical movement for player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //takes input from player and makes player move
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //gravity for the freefall of the player
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
