using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フィーバーゲージ
/// </summary>

public class FeverGauge : MonoBehaviour
{
    public Slider _slider;

    PlayerController _player;

    void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj)_player = playerObj.GetComponent<PlayerController>();

        _slider.maxValue = (float)_player._maxFeverValue;
    }

    void Update()
    {
        if (_player)
        {
            _slider.value = (float)_player._feverCount;
        }
    }
}

