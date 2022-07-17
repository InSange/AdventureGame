using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    // ���ֵ��� �⺻ ������ �Ǿ��ִ� Ŭ�����̴�.

    [SerializeField]
    protected int _hp;  // ����ü��
    [SerializeField]
    protected int _maxHp; // �ִ�ü��
    [SerializeField]
    protected int _attack; // ���ݷ�
    [SerializeField]
    protected int _defense; // ����
    [SerializeField]
    protected float _moveSpeed; // �̵��ӵ�

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
