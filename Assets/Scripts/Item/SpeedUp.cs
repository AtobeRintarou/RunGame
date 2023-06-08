using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 加速アイテム
/// </summary>

public class SpeedUp : ItemBase
{
    //アイテムを取ったら増加するスピード
    public float _plusSpeed = 2;

    PlayerController _player;

    public override void Activate()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj)
        {
            _player = playerObj.GetComponent<PlayerController>();
            if (_player)
            {
                Debug.Log("だーんだーんはやくーなーる♪");
                _player._moveSpeed += _plusSpeed;
                Destroy(this.gameObject);
            }
        }
    }
}