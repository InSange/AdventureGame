using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    // 유닛들의 기본 스탯이 되어주는 클래스이다.

    [SerializeField]
    protected int _hp;  // 현재체력
    [SerializeField]
    protected int _maxHp; // 최대체력
    [SerializeField]
    protected int _atk; // 공격력
    [SerializeField]
    protected int _def; // 방어력
    [SerializeField]
    protected float _moveSpeed; // 이동속도
    [SerializeField]
    protected float _atkSpeed; // 공격속도
    [SerializeField]
    protected float _atkCoolDown; // 공격딜레이
    [SerializeField]
    protected float _atkRange; // 공격사거리

    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Atk { get { return _atk; } set { _atk = value; } }
    public int Def { get { return _def; } set { _def = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float AtkSpeed { get { return _atkSpeed; } set { _atkSpeed = value; } }
    public float AtkCoolDown { get { return _atkCoolDown; } set { _atkCoolDown = value; } }
    public float AtkRange { get { return _atkRange; } set { _atkRange = value; } }


    public void TakeDamage(int damage) // 데미지 받는 함수
    {
        _hp -= damage;
    }
}
