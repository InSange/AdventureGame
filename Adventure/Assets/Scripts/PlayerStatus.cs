using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    [SerializeField]
    protected int _gold; // �����ϰ� �ִ� ��

    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _hp = 100;
        _maxHp = 100;
        _atk = 10;
        _def = 5;
        _moveSpeed = 5.0f;
        _gold = 0;
        _atkSpeed = 1.0f;
        _atkCoolDown = 0f;
        _atkRange = 3.0f;
    }

    public void Attack(Status MonsterStatus)
    {
        if (_atkCoolDown <= 0)
        {
            MonsterStatus.TakeDamage(_atk); // Ÿ�ٿ� �������� ����!
            _atkCoolDown = 2.0f / _atkSpeed; // ���� ������ ����
        }
    }
}
