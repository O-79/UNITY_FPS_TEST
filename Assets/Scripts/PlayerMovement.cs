using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController CONTROLLER;

    public float SPEED = 24f;
    public float GRAVITY = -48f;
    //public float FALL = -16f;
    public float JUMP = 5f;

    public Transform CAMERA;
    public Transform GROUND_CHECK;
    public float GROUND_DISTANCE = 0.25f;
    public LayerMask GROUND_MASK;

    Vector3 VELOCITY;

    bool GROUNDED;
    bool MOVING;

    private Vector3 PREV_POS = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        CONTROLLER = GetComponent<CharacterController>();

        // setting velocity to small negative number to make sure player stays hugging ground
        //VELOCITY.y = -2f;
    }

    // Update is called once per frame
    void Update()
    {
        // check if on ground
        GROUNDED = Physics.CheckSphere(GROUND_CHECK.position, GROUND_DISTANCE, GROUND_MASK);
        //Debug.Log($"Grounded: {GROUNDED}"); //

        // setting velocity to small negative number to make sure player stays hugging ground
        if (GROUNDED && VELOCITY.y < 0)
        {
            VELOCITY.y = -2f;
        }

        // getting X and Z inputs
        float X = Input.GetAxisRaw("Horizontal");
        float Z = Input.GetAxisRaw("Vertical");

        // Get the camera's yaw (horizontal rotation only)
        Vector3 cameraForward = Quaternion.Euler(0f, CAMERA.eulerAngles.y, 0f) * Vector3.forward; // ADDED
        Vector3 cameraRight = Quaternion.Euler(0f, CAMERA.eulerAngles.y, 0f) * Vector3.right; // ADDED

        // move vector, ignoring any vertical influence from the camera
        Vector3 MOVE = (cameraRight * X + cameraForward * Z).normalized; // MODIFIED

        // move the player
        CONTROLLER.Move(MOVE * SPEED * Time.deltaTime);

        // normalize if there is movement
        /*if (MOVE != Vector3.zero)
        {
            MOVE.Normalize();
        }*/

        //VELOCITY.x = MOVE.x * SPEED;
        //VELOCITY.z = MOVE.z * SPEED;

        // check if player can jump
        if (Input.GetButtonDown("Jump") && GROUNDED)
        {
            // formula to determine velocity needed to reach jump height
            VELOCITY.y = Mathf.Sqrt(JUMP * -2f * GRAVITY);
        }

        // falling
        VELOCITY.y += GRAVITY * Time.deltaTime;
        //VELOCITY.y = FALL * Time.deltaTime;

        // change y axis of player
        CONTROLLER.Move(VELOCITY * Time.deltaTime);

        /*if (PREV_POS != gameObject.transform.position)
        {
            MOVING = true;
        }
        else
        {
            MOVING = false;
        }

        PREV_POS = gameObject.transform.position;*/
    }
}
