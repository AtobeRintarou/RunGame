using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //�v���C���[���i�[�p
    [SerializeField] GameObject _player;

    //���΋����擾�p
    private Vector3 _offset;

    void Start()
    {

    }

    void Update()
    {
        transform.forward = _player.transform.forward;
    }
}
