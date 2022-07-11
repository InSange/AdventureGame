using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f; // 플레이어 이동속도
    [SerializeField] private float _jumpSpeed = 8.0f; // 플레이어 점프속도
    [SerializeField] private float Gravity = 20.0f; // 중력


    private CharacterController _characterController; // 캐릭터 컨트롤러
    private Vector3 MoveDir = Vector3.zero; // 캐릭터의 움직이는 방향.


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>(); // 캐릭터 컨트롤러 컴포넌트 연결
    }

    void FixedUpdate()
    {
        Move();
        Debug.Log(_characterController.isGrounded);
    }

    private void Move()
    {
        if(_characterController == null) return;

        // 캐릭터가 바닥에 붙어 있는 경우 true를 반환.
        if(_characterController.isGrounded)
        {
            Debug.Log("입력");
            // 키보드에 따른 X, Z 축 이동방향을 새로 결정한다.
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            // 오브젝트가 바라보는 앞방향으로 이동방향을 돌려서 조정한다.
            MoveDir = _characterController.transform.TransformDirection(MoveDir);

            // 바라보는 방향으로 속도 적용.
            MoveDir *= _moveSpeed;

            // 캐릭터 점프.
            if (Input.GetButton("Jump"))
                MoveDir.y = _jumpSpeed;
        }
        else // 캐릭터가 바닥에 붙어 있지 않은 경우.
        {
            MoveDir.y -= Gravity * Time.deltaTime;
        }

        _characterController.Move(MoveDir * Time.deltaTime);
    }
}