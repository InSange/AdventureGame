using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f; // �÷��̾� �̵��ӵ�
    //[SerializeField] private float _jumpSpeed = 8.0f; // �÷��̾� �����ӵ�
    [SerializeField] private float Gravity = 20.0f; // �߷�
    //[SerializeField] private float rotateSpeed = 3.0f; // �÷��̾� ȸ�� �ӵ�
    [SerializeField] private Camera cam; // �÷��̾� ī�޶�
    [SerializeField] private float _flipSpeed = 8.0f; // ������ �ӵ�

    private Animator anim; //ĳ���� �ִϸ�����
    private CharacterController _characterController; // ĳ���� ��Ʈ�ѷ�
    private Vector3 MoveDir = Vector3.zero; // ĳ������ �����̴� ����.

    public Quaternion finalRotation; // ����ȸ�� ��
    private Ray ray; // ���� ����
    private RaycastHit hit; // ray�� �΋H�� ��������
    //private float RayLength = 100; // ���� ����

    private bool _isfleep = false; // ���������ΰ�?

    enum anim_State { Idle, Run, Flip, Float, Jump_end, Jump_start, Walk, Jump_loop }; // �ִϸ��̼� ���µ� ������
    string currentState; // �÷��̾� �ִϸ��̼� ������¸� ������ ����.

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>(); // ĳ���� ��Ʈ�ѷ� ������Ʈ ����
        anim = gameObject.GetComponent<Animator>(); // ĳ���� �ִϸ��̼� ������Ʈ ����
        //cam = GetComponentInChildren<Camera>(); // �ڽĿ�����Ʈ�� ī�޶� ������Ʈ�� ������
    }

    void FixedUpdate()
    {
        Move();
        LookMouse();
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
            //MoveDir = _characterController.transform.TransformDirection(MoveDir);

            // �÷��̾ �����̴� �������� �ٶ󺸴� ���� ȸ��.
            //transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed);
            
            if(_isfleep == false)
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) // �÷��̾ �����̰� �ִٸ� �ִϸ��̼� Walk���� ü����.
                {
                    ChangeAnimationState(anim_State.Walk);
                    // �ٶ󺸴� �������� �ӵ� ����.
                    MoveDir *= _moveSpeed;
                }
                else // �ȿ����̰� ������ Idle�� ü����.
                {
                    ChangeAnimationState(anim_State.Idle);
                }

                // ĳ���� ����. -> ������� ����.
                if (Input.GetButtonDown("Jump"))
                {
                    //MoveDir.y = _jumpSpeed;
                    _isfleep = true;
                }
            }
            else
            {
                ChangeAnimationState(anim_State.Flip); // ������ �ִϸ��̼����� ü����.
                MoveDir *= _flipSpeed;
                if(anim.GetCurrentAnimatorStateInfo(0).IsName("Flip") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    _isfleep = false;
                }
            }
        }
        else // ĳ���Ͱ� �ٴڿ� �پ� ���� ���� ���.
        {
            MoveDir.y -= Gravity * Time.deltaTime;
        }
        // ĳ���� ������.
        _characterController.Move(MoveDir * Time.deltaTime);
    }

    void LookMouse()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ���콺 �������� ���̹߻�.

        if(Physics.Raycast(ray, out hit))
        {
            Vector3 mouseDir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
            anim.transform.forward = mouseDir;
        }
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