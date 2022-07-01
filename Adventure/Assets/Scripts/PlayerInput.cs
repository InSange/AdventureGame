using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string verticalInputName = "Vertical";
    public string horizontalInputName = "Horizontal";

    public float verticalInput { get; private set; }
    public float horizontalInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool runInput { get; private set; }

    private void Update()
    {
        verticalInput = Input.GetAxis(verticalInputName);
        horizontalInput = Input.GetAxis(horizontalInputName);
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        runInput = Input.GetKey(KeyCode.LeftShift);
    }
}
