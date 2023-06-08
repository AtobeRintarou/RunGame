using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �t�B�[�o�[�A�C�e��
/// </summary>

public class FeverItem : ItemBase
{
    //�A�C�e����������痭�܂�t�B�[�o�[�Q�[�W
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
                Debug.Log("���܂�");
                _player._feverCount += _plusFever;
                Destroy(this.gameObject);
            }
        }
    }
}