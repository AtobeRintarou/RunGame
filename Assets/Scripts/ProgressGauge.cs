using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージ進行度ゲージ
/// </summary>

public class ProgressGauge : MonoBehaviour
{
    public GameObject _goalObj, _startObj, _playerObj;

    public Slider _slider;

    void Start()
    {
        _slider.minValue = (float)_startObj.transform.position.z;
        _slider.maxValue = (float)_goalObj.transform.position.z;
    }

    void Update()
    {

        _slider.value = (float)_playerObj.transform.position.z;
    }
}
