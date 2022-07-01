using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0f, 1f)]
    public float maxLength;


    public LayerMask groundedLayer;

    private enum SetMoveType
    {
        Direct,
        LookCam,
        BackWalk
    }

    [SerializeField] private SetMoveType setMoveType = SetMoveType.Direct;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float turnSpeed = 200f;
    [SerializeField] private bool isGrounded = false;

    private readonly float walkScale = 0.33f;
    private readonly float backWalkScale = 0.55f;
    private readonly float backRunScale = 0.88f;
    private readonly float interpolation = 10f;

    private float currentV = 0;
    private float currentH = 0;
    private Vector3 currentDirection = Vector3.zero;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Animator playerAnimator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        switch (setMoveType)
        {
            case SetMoveType.Direct:
                DirectMove();
                break;
            case SetMoveType.LookCam:
                LookCamMove();
                break;
            case SetMoveType.BackWalk:
                BackWalkMove();
                break;
        }
    }

    private void DirectMove()
    {
        float v = playerInput.verticalInput;
        float h = playerInput.horizontalInput;

        if(!playerInput.runInput)
        {
            v *= walkScale;
            h *= walkScale;
        }

        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

        Vector3 direction = new Vector3(currentH, 0, currentV);

        if(direction != Vector3.zero)
        {
            currentDirection = Vector3.Slerp(currentDirection, direction, Time.deltaTime * interpolation);

            transform.forward = currentDirection;

            playerRigidbody.MovePosition(playerRigidbody.position + currentDirection * Time.deltaTime * moveSpeed);

            playerAnimator.SetFloat("MoveSpeed", currentDirection.magnitude);
        }

        Jump();
    }

    private void LookCamMove()
    {
        float v = playerInput.verticalInput;
        float h = playerInput.horizontalInput;

        Transform mainCam = Camera.main.transform;

        if(!playerInput.runInput)
        {
            v *= walkScale;
            h *= walkScale;
        }

        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

        Vector3 direction = mainCam.forward * currentV + mainCam.right * currentH;

        direction.y = 0;

        if (direction != Vector3.zero)
        {
            currentDirection = Vector3.Slerp(currentDirection, direction, Time.deltaTime * interpolation);

            transform.forward = currentDirection;

            playerRigidbody.MovePosition(playerRigidbody.position + currentDirection * Time.deltaTime * moveSpeed);

            playerAnimator.SetFloat("MoveSpeed", currentDirection.magnitude);
        }

        Jump();
    }

    private void BackWalkMove()
    {
        float v = playerInput.verticalInput;
        float h = playerInput.horizontalInput;
        bool run = playerInput.runInput;

        if(v<0)
        {
            if(!run)
            {
                v *= backWalkScale;
            }
            else
            {
                v *= backRunScale;
            }
        }
        else if(!run)
        {
            v *= walkScale;
        }

        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

        playerRigidbody.MovePosition(transform.position + (transform.forward * currentV * moveSpeed * Time.deltaTime));

        transform.Rotate(0, currentH * turnSpeed * Time.deltaTime, 0);

        playerAnimator.SetFloat("MoveSpeed", currentV);

        Jump();
    }

    private void Jump()
    {
        if(playerInput.jumpInput && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnimator.SetBool("Jump", playerInput.jumpInput);
        }
    }

    private void CheckGround()
    {
#if UNITY_EDITOR
        Debug.DrawRay(transform.position + new Vector3(0, 0.05f, 0), Vector3.down * maxLength, Color.red);
#endif
        if(Physics.Raycast(transform.position + new Vector3(0, 0.05f, 0), Vector3.down, maxLength, groundedLayer))
        {
            Debug.Log("감지!");
            isGrounded = true;
        }
        else
        {
            Debug.Log("감지되지 않음!");
            isGrounded = false;
        }
    }

}