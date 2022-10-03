using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 5f;
    public float runSpeed = 8f;
    float x;
    float z;
    Vector3 move;

    //Gravedad
    Vector3 velocity;
    public float gravity = -15f;

    public Transform groundCheck;
    float radius = 0.4f;
    public LayerMask mask;
    bool isGrounded = false;
    public float jumpForce = 1f;

    private float jumpValue;

    private void Start() {
        jumpValue = Mathf.Sqrt(jumpForce* -2 * gravity);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,radius, mask);
        
        if(isGrounded && velocity.y < 0){
            velocity.y = gravity;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        move = transform.right * x + transform.forward * z;
        // controller.Move(move*speed*Time.deltaTime);

        if(Input.GetButton("Jump") && isGrounded){
            velocity.y =jumpValue;
        }

        //Si shift se esta presionando aumenta la velocidad

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            controller.Move(move*runSpeed*Time.deltaTime);
        }
        else{
            controller.Move(move*speed*Time.deltaTime);
        }

        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
