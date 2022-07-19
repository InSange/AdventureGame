using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterController : MonoBehaviour
{
    public Transform target; // 목표 타겟
    private NavMeshAgent nav; // // 네비게이션
    PlayerStatus targetStatus; // 타겟의 스테이터스 (체력감소 및 적용)
    MonsterStatus monsterStatus; // 몬스터의 스테이터스

    private void OnDrawGizmos() // 범위 반경을 보여줄 기즈모
    {
        Gizmos.color = Color.red;   // 기즈모의 색깔은 레드
        Gizmos.DrawWireSphere(transform.position, monsterStatus.AtkRange) ;   // 오브젝트 공격범위 거리
    }

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>(); // Nav메쉬를 불러온다.
        targetStatus = target.GetComponent<PlayerStatus>(); // 타겟(플레이어) 스테이터스를 불러온다.
        monsterStatus = this.gameObject.GetComponent<MonsterStatus>(); // 현재 오브젝트(몬스터)의 스테이터스를 불러온다.
    }

    private void Update()
    {
        MonsterAI(); // 매 프레임마다 실행. (플레이어를 감지해야함)
    }

    void MonsterAI()
    {
        float distance = Vector3.Distance(target.position, transform.position); // 플레이어와 몬스터의 거리
        
        if(distance > monsterStatus.AtkRange) // 거리가 몬스터 공격범위 밖이면
        {
            nav.SetDestination(target.position); // 타겟(플레이어)를 따라간다.
            nav.speed = monsterStatus.MoveSpeed;
        }
        else if(distance <= monsterStatus.AtkRange) // 거리가 몬스터 공격범위 안이면
        {
            if(targetStatus != null) // 플레이어가 있으면
            {
                monsterStatus.Attack(targetStatus); // 공격 실행.
            }
        }
        else // 플레이어(타겟)이 없으면
        {
            nav.SetDestination(transform.position); // 제자리
        }

    }
}
