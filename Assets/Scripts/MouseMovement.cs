using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float MOUSE_SENS = 384f;

    float ROT_X = 0f;
    float ROT_Y = 0f;

    public float ROT_X_CLAMP_U = -90f; // looking up decreases the angle (min clamp)
    public float ROT_X_CLAMP_D = 90f;  // looking down increases it      (max clamp)

    // Start is called before the first frame update
    void Start()
    {
        // cursor -> middle of screen + invis
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // mouse inputs
        float MOUSE_X = Input.GetAxis("Mouse X") * MOUSE_SENS * Time.deltaTime;
        float MOUSE_Y = Input.GetAxis("Mouse Y") * MOUSE_SENS * Time.deltaTime;

        // rotate around x axis (looking up and down) -> use mouse y input
        ROT_X -= MOUSE_Y;

        // stop rotation from looking backwards (from above or below)
        ROT_X = Mathf.Clamp(ROT_X, ROT_X_CLAMP_U, ROT_X_CLAMP_D);

        // rotate around y axis (looking left and right) -> use mouse x input
        ROT_Y += MOUSE_X; // no need to clamp (we want to see all 360 degrees on XZ plane)

        // translate to a quaternion to avoid gimbal lock
        transform.localRotation = Quaternion.Euler(ROT_X, ROT_Y, 0f);
    }
}
