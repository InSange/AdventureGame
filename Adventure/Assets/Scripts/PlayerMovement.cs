using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f; // �÷��̾� �̵��ӵ�
    [SerializeField] private float _jumpSpeed = 8.0f; // �÷��̾� �����ӵ�
    [SerializeField] private float Gravity = 20.0f; // �߷�
    [SerializeField] private float rotateSpeed = 3.0f; // �÷��̾� ȸ�� �ӵ�

    private Animator anim; //ĳ���� �ִϸ�����
    private CharacterController _characterController; // ĳ���� ��Ʈ�ѷ�
    private Vector3 MoveDir = Vector3.zero; // ĳ������ �����̴� ����.

    enum anim_State { Idle, Run, Flip, Float, Jump_end, Jump_start, Walk, Jump_loop }; // �ִϸ��̼� ���µ� ������
    string currentState; // �÷��̾� �ִϸ��̼� ������¸� ������ ����.

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>(); // ĳ���� ��Ʈ�ѷ� ������Ʈ ����
        anim = gameObject.GetComponent<Animator>(); // ĳ���� �ִϸ��̼� ������Ʈ ����
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
            // Ű���忡 ���� X, Z �� �̵������� ���� �����Ѵ�.
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            // ������Ʈ�� �ٶ󺸴� �չ������� �̵������� ������ �����Ѵ�.
            MoveDir = _characterController.transform.TransformDirection(MoveDir);
            // �ٶ󺸴� �������� �ӵ� ����.
            MoveDir *= _moveSpeed;
            // �÷��̾ �����̴� �������� �ٶ󺸴� ���� ȸ��.
            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed);

            if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical") != 0)
            {
                ChangeAnimationState(anim_State.Walk);
            }
            else
            {
                ChangeAnimationState(anim_State.Idle);
            }

            // ĳ���� ����.
            if (Input.GetButton("Jump"))
                MoveDir.y = _jumpSpeed;
        }
        else // ĳ���Ͱ� �ٴڿ� �پ� ���� ���� ���.
        {
            MoveDir.y -= Gravity * Time.deltaTime;
        }
        // ĳ���� ������.
        _characterController.Move(MoveDir * Time.deltaTime);
    }

    void ChangeAnimationState(anim_State newStateParameter)
    {
        string newState = newStateParameter.ToString();
        // ������¿� ������ �ٲ��ʿ� ����.
        if (currentState == newState) return;

        // �÷��̾� �� �ִϸ��̼� ����
        anim.Play(newState);

        // ������¸� ������Ʈ��.
        currentState = newState;
    }
}