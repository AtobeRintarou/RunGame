using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾数増加アイテム
/// </summary>

public class BulletItem : ItemBase
{
    //アイテムを取ったら増える弾数
    public float _plusBullet = 1;

    PlayerController _player;

    public override void Activate()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj)
        {
            _player = playerObj.GetComponent<PlayerController>();
            if (_player)
            {
                Debug.Log("備えあればうれしいな");
                _player._playerBullet += _plusBullet;
                Destroy(this.gameObject);
            }
        }
    }
}