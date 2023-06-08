using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃力増加アイテム
/// </summary>

public class AttackUp : ItemBase
{
    //アイテムを取ったら増加する攻撃力
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
                Debug.Log("右腕が疼く・・・");
                _player._playerAttack += _plusAttack;
                Destroy(this.gameObject);
            }
        }
    }
}