using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f; // 플레이어 이동속도
    //[SerializeField] private float _jumpSpeed = 8.0f; // 플레이어 점프속도
    [SerializeField] private float Gravity = 20.0f; // 중력
    //[SerializeField] private float rotateSpeed = 3.0f; // 플레이어 회전 속도
    [SerializeField] private Camera cam; // 플레이어 카메라
    [SerializeField] private float _flipSpeed = 8.0f; // 구르기 속도

    private Animator anim; //캐릭터 애니메이터
    private CharacterController _characterController; // 캐릭터 컨트롤러
    private Vector3 MoveDir = Vector3.zero; // 캐릭터의 움직이는 방향.

    public Quaternion finalRotation; // 최종회전 값
    private Ray ray; // 레이 변수
    private RaycastHit hit; // ray가 부딫힌 정보변수
    //private float RayLength = 100; // 레이 길이

    private bool _isfleep = false; // 구르는중인가?

    enum anim_State { Idle, Run, Flip, Float, Jump_end, Jump_start, Walk, Jump_loop }; // 애니메이션 상태들 열거형
    string currentState; // 플레이어 애니메이션 현재상태를 지정할 변수.

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>(); // 캐릭터 컨트롤러 컴포넌트 연결
        anim = gameObject.GetComponent<Animator>(); // 캐릭터 애니메이션 컴포넌트 연결
        //cam = GetComponentInChildren<Camera>(); // 자식오브젝트인 카메라 컴포넌트를 가져옴
    }

    void FixedUpdate()
    {
        Move();
        LookMouse();
    }

    private void Move()
    {
        if(_characterController == null) return;

        // 캐릭터가 바닥에 붙어 있는 경우 true를 반환.
        if(_characterController.isGrounded)
        {
            // 키보드에 따른 X, Z 축 이동방향을 새로 결정한다.
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // 오브젝트가 바라보는 앞방향으로 이동방향을 돌려서 조정한다.
            //MoveDir = _characterController.transform.TransformDirection(MoveDir);

            // 플레이어가 움직이는 방향으로 바라보는 방향 회전.
            //transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed);
            
            if(_isfleep == false)
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) // 플레이어가 움직이고 있다면 애니메이션 Walk으로 체인지.
                {
                    ChangeAnimationState(anim_State.Walk);
                    // 바라보는 방향으로 속도 적용.
                    MoveDir *= _moveSpeed;
                }
                else // 안움직이고 있으면 Idle로 체인지.
                {
                    ChangeAnimationState(anim_State.Idle);
                }

                // 캐릭터 점프. -> 구르기로 변경.
                if (Input.GetButtonDown("Jump"))
                {
                    //MoveDir.y = _jumpSpeed;
                    _isfleep = true;
                }
            }
            else
            {
                ChangeAnimationState(anim_State.Flip); // 구르기 애니메이션으로 체인지.
                MoveDir *= _flipSpeed;
                if(anim.GetCurrentAnimatorStateInfo(0).IsName("Flip") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    _isfleep = false;
                }
            }
        }
        else // 캐릭터가 바닥에 붙어 있지 않은 경우.
        {
            MoveDir.y -= Gravity * Time.deltaTime;
        }
        // 캐릭터 움직임.
        _characterController.Move(MoveDir * Time.deltaTime);
    }

    void LookMouse()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 방향으로 레이발사.

        if(Physics.Raycast(ray, out hit))
        {
            Vector3 mouseDir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
            anim.transform.forward = mouseDir;
        }
    }

    void ChangeAnimationState(anim_State newStateParameter)
    {
        string newState = newStateParameter.ToString();
        // 현재상태와 같으면 바꿀필요 없음.
        if (currentState == newState) return;

        // 플레이어 새 애니메이션 실행
        anim.Play(newState);

        // 현재상태를 업데이트함.
        currentState = newState;
    }
}