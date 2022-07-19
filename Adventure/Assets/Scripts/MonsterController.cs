using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterController : MonoBehaviour
{
    public Transform target; // ��ǥ Ÿ��
    private NavMeshAgent nav; // // �׺���̼�
    PlayerStatus targetStatus; // Ÿ���� �������ͽ� (ü�°��� �� ����)
    MonsterStatus monsterStatus; // ������ �������ͽ�

    private void OnDrawGizmos() // ���� �ݰ��� ������ �����
    {
        Gizmos.color = Color.red;   // ������� ������ ����
        Gizmos.DrawWireSphere(transform.position, monsterStatus.AtkRange) ;   // ������Ʈ ���ݹ��� �Ÿ�
    }

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>(); // Nav�޽��� �ҷ��´�.
        targetStatus = target.GetComponent<PlayerStatus>(); // Ÿ��(�÷��̾�) �������ͽ��� �ҷ��´�.
        monsterStatus = this.gameObject.GetComponent<MonsterStatus>(); // ���� ������Ʈ(����)�� �������ͽ��� �ҷ��´�.
    }

    private void Update()
    {
        MonsterAI(); // �� �����Ӹ��� ����. (�÷��̾ �����ؾ���)
    }

    void MonsterAI()
    {
        float distance = Vector3.Distance(target.position, transform.position); // �÷��̾�� ������ �Ÿ�
        
        if(distance > monsterStatus.AtkRange) // �Ÿ��� ���� ���ݹ��� ���̸�
        {
            nav.SetDestination(target.position); // Ÿ��(�÷��̾�)�� ���󰣴�.
            nav.speed = monsterStatus.MoveSpeed;
        }
        else if(distance <= monsterStatus.AtkRange) // �Ÿ��� ���� ���ݹ��� ���̸�
        {
            if(targetStatus != null) // �÷��̾ ������
            {
                monsterStatus.Attack(targetStatus); // ���� ����.
            }
        }
        else // �÷��̾�(Ÿ��)�� ������
        {
            nav.SetDestination(transform.position); // ���ڸ�
        }

    }
}
