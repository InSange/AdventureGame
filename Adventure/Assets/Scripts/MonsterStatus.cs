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
        _atkCoolDown -= Time.deltaTime; // 공격 딜레이(쿨다운) 감소
    }

    public void Attack(Status targetStatus) // 공격
    {
        if(_atkCoolDown <= 0)
        {
            targetStatus.TakeDamage(_atk); // 타겟에 데미지를 입힘!
            _atkCoolDown = 2.0f / _atkSpeed; // 공격 딜레이 갱신
        }
    }
}
