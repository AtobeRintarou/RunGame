using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �c�e���\���p�e�L�X�g
/// </summary>

public class BulletText : MonoBehaviour
{
    public Text _bulletText;

    PlayerController _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        _bulletText.text = _player._playerBullet.ToString();
    }
}
