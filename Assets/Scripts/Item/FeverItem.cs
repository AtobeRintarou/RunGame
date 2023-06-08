using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィーバーアイテム
/// </summary>

public class FeverItem : ItemBase
{
    //アイテムを取ったら溜まるフィーバーゲージ
    public float _plusFever = 1;

    PlayerController _player;

    public override void Activate()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj)
        {
            _player = playerObj.GetComponent<PlayerController>();
            if (_player)
            {
                Debug.Log("うまっ");
                _player._feverCount += _plusFever;
                Destroy(this.gameObject);
            }
        }
    }
}