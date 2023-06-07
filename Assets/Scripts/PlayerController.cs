using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //カメラ
    public Camera _cam;

    //進む方向の格納
    private Vector3 _movement;

    //移動速度
    public float _moveSpeed = 4f;

    //ジャンプ力
    public Vector3 _jumpForce = new Vector3(0, 6, 0);

    //レイを飛ばすオブジェクトの位置
    public Transform _groundCheckPoint;

    //地面レイヤー
    public LayerMask _groundLayers;

    //剛体
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerMove();
        Jump();
    }

    public void PlayerMove()
    {
        //進む方向を出して変数に格納
        _movement = transform.forward.normalized;

        //現在位置に反映
        this.transform.position += _movement * _moveSpeed * Time.deltaTime;
        _cam.transform.position += _movement * _moveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        //地面についている、かつスペースキーが押されたとき
        if (IsGround() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(_jumpForce, ForceMode.Impulse);
        }
    }

    //地面についていれば true
    public bool IsGround()
    {
        return Physics.Raycast(_groundCheckPoint.position, Vector3.down, 0.25f, _groundLayers);
    }
}
