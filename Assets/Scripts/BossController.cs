using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float _bossHp = 1000;

    [HideInInspector]
    public float _bossMaxHp = 1000;

    private float _time;


    void Start()
    {
        _bossMaxHp = _bossHp;
    }

    void Update()
    {
        
    }
}
