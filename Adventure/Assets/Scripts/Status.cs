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
    protected int _atk; // ���ݷ�
    [SerializeField]
    protected int _def; // ����
    [SerializeField]
    protected float _moveSpeed; // �̵��ӵ�
    [SerializeField]
    protected float _atkSpeed; // ���ݼӵ�
    [SerializeField]
    protected float _atkCoolDown; // ���ݵ�����
    [SerializeField]
    protected float _atkRange; // ���ݻ�Ÿ�

    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Atk { get { return _atk; } set { _atk = value; } }
    public int Def { get { return _def; } set { _def = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float AtkSpeed { get { return _atkSpeed; } set { _atkSpeed = value; } }
    public float AtkCoolDown { get { return _atkCoolDown; } set { _atkCoolDown = value; } }
    public float AtkRange { get { return _atkRange; } set { _atkRange = value; } }


    public void TakeDamage(int damage) // ������ �޴� �Լ�
    {
        _hp -= damage;
    }
}
