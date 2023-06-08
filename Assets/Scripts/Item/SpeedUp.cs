using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����A�C�e��
/// </summary>

public class SpeedUp : ItemBase
{
    //�A�C�e����������瑝������X�s�[�h
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
                Debug.Log("���[�񂾁[��͂₭�[�ȁ[���");
                _player._moveSpeed += _plusSpeed;
                Destroy(this.gameObject);
            }
        }
    }
}