using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �U���͑����A�C�e��
/// </summary>

public class AttackUp : ItemBase
{
    //�A�C�e����������瑝������U����
    public float _plusAttack = 5;

    PlayerController _player;

    public override void Activate()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj)
        {
            _player = playerObj.GetComponent<PlayerController>();
            if (_player)
            {
                Debug.Log("�E�r���u���E�E�E");
                _player._playerAttack += _plusAttack;
                Destroy(this.gameObject);
            }
        }
    }
}