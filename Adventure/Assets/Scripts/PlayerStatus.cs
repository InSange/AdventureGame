using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    [SerializeField]
    protected int _gold; // 소지하고 있는 돈

    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5.0f;
        _gold = 0;
    }
}
