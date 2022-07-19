using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : Status
{
    private void Start()
    {
        _hp = 100;
        _maxHp = 100;
        _atk = 10;
        _def = 5;
        _moveSpeed = 5.0f;
        _atkSpeed = 1.0f;
        _atkCoolDown = 0f;
        _atkRange = 2.0f;
    }

    private void Update()
    {
        _atkCoolDown -= Time.deltaTime; // ���� ������(��ٿ�) ����
    }

    public void Attack(Status targetStatus) // ����
    {
        if(_atkCoolDown <= 0)
        {
            targetStatus.TakeDamage(_atk); // Ÿ�ٿ� �������� ����!
            _atkCoolDown = 2.0f / _atkSpeed; // ���� ������ ����
        }
    }
}
