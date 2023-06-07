using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //プレイヤー情報格納用
    [SerializeField] GameObject _player;

    //相対距離取得用
    private Vector3 _offset;

    void Start()
    {

    }

    void Update()
    {
        transform.forward = _player.transform.forward;
    }
}
