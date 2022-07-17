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
    protected int _attack; // 공격력
    [SerializeField]
    protected int _defense; // 방어력
    [SerializeField]
    protected float _moveSpeed; // 이동속도

    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    /*
    private void Start()
    {
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5.0f;
    }
    */
}
