using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f; // �÷��̾� �̵��ӵ�
    [SerializeField] private float _jumpSpeed = 8.0f; // �÷��̾� �����ӵ�
    [SerializeField] private float Gravity = 20.0f; // �߷�


    private CharacterController _characterController; // ĳ���� ��Ʈ�ѷ�
    private Vector3 MoveDir = Vector3.zero; // ĳ������ �����̴� ����.


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>(); // ĳ���� ��Ʈ�ѷ� ������Ʈ ����
    }

    void FixedUpdate()
    {
        Move();
        Debug.Log(_characterController.isGrounded);
    }

    private void Move()
    {
        if(_characterController == null) return;

        // ĳ���Ͱ� �ٴڿ� �پ� �ִ� ��� true�� ��ȯ.
        if(_characterController.isGrounded)
        {
            Debug.Log("�Է�");
            // Ű���忡 ���� X, Z �� �̵������� ���� �����Ѵ�.
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            // ������Ʈ�� �ٶ󺸴� �չ������� �̵������� ������ �����Ѵ�.
            MoveDir = _characterController.transform.TransformDirection(MoveDir);

            // �ٶ󺸴� �������� �ӵ� ����.
            MoveDir *= _moveSpeed;

            // ĳ���� ����.
            if (Input.GetButton("Jump"))
                MoveDir.y = _jumpSpeed;
        }
        else // ĳ���Ͱ� �ٴڿ� �پ� ���� ���� ���.
        {
            MoveDir.y -= Gravity * Time.deltaTime;
        }

        _characterController.Move(MoveDir * Time.deltaTime);
    }
}