using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e�������A�C�e��
/// </summary>

public class BulletItem : ItemBase
{
    //�A�C�e����������瑝����e��
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
                Debug.Log("��������΂��ꂵ����");
                _player._playerBullet += _plusBullet;
                Destroy(this.gameObject);
            }
        }
    }
}