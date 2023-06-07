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
    private Vector3 _playerPos;

    //進む方向の格納
    private Vector3 _movement;

    //移動速度,加速間隔,加速度
    public float _moveSpeed = 4f;
    public float _interval = 0.1f;
    public float _accel = 0.1f;

    //ジャンプ力
    public Vector3 _jumpForce = new Vector3(0, 6, 0);

    //レイを飛ばすオブジェクトの位置
    public Transform _groundCheckPoint;

    //地面レイヤー
    public LayerMask _groundLayers;

    //剛体
    private Rigidbody _rb;

    //プレイヤーのコライダー
    private CapsuleCollider _capsuleCol;
    
    //タイマー
    public float _time;

    //ジャンプ中、スライディング中か否か
    private bool isJump = false, isSliding = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _capsuleCol = GetComponent<CapsuleCollider>();
        _playerPos = this.transform.position;
    }

    void Update()
    {
        PlayerMove();
        Jump();
        Sliding();
    }

    public void PlayerMove()
    {
        //進む方向を出して変数に格納
        _movement = transform.forward.normalized;

        //z方向移動
        this.transform.position += _movement * _moveSpeed * Time.deltaTime;
        _cam.transform.position += _movement * _moveSpeed * Time.deltaTime;

        _time += Time.deltaTime;

        if (Mathf.Floor(_time) % _interval == 0)
        {
            _moveSpeed += _accel;
        }

        //x方向移動
        if (isJump == false && isSliding == false)
        {
            if (this.transform.position.x > -1.5 && Input.GetKeyDown(KeyCode.A))
            {
                _playerPos.x -= (float)1.5;
                _playerPos.z = this.transform.position.z;
                this.transform.position = _playerPos;
                Debug.Log("左にうぃぃぃ");
            }
            else if (this.transform.position.x < 1.5 && Input.GetKeyDown(KeyCode.D))
            {
                _playerPos.x += (float)1.5;
                _playerPos.z = this.transform.position.z;
                this.transform.position = _playerPos;
                Debug.Log("右にふぅぅぅ");
            }
            else if ((this.transform.position.x == -1.5 || this.transform.position.x == 1.5) && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
            {
                Debug.Log("うぐっっ、見えない壁が！");
            }
        }
    }

    public void Jump()
    {
        //地面についている、かつスペースキーが押されたとき
        if (IsGround() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(_jumpForce, ForceMode.Impulse);
            Debug.Log("やっふぅぅぅ");
        }

        if (IsGround()) isJump = false;
        else if (!IsGround()) isJump = true;
    }

    public void Sliding()
    {
        //地面についている、かつ左シフトキーが押されたとき
        if (IsGround() && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine("SwitchSliding");
        }
    }

    IEnumerator SwitchSliding()
    {
        isSliding = true;
        _capsuleCol.height = 1;
        Debug.Log("ずざぁぁぁぁ");

        //1.5秒待つ
        yield return new WaitForSeconds((float)1.5);

        isSliding = false;
        _capsuleCol.height = 2;
        Debug.Log("すたっっ");
    }

    //地面についていれば true
    public bool IsGround()
    {
        return Physics.Raycast(_groundCheckPoint.position, Vector3.down, 0.25f, _groundLayers);
    }
}
