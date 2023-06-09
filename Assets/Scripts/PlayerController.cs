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
    private Vector3 _playerPos, _movement;

    //レイを飛ばすオブジェクトの位置
    public Transform _groundCheckPoint;

    //地面レイヤー
    public LayerMask _groundLayers;

    //剛体
    private Rigidbody _rb;

    //プレイヤーのコライダー
    private CapsuleCollider _capsuleCol;
    
    //タイマー
    private float _time, _feverTimer;

    //ジャンプ中、スライディング中、フィーバー中か否か
    private bool isJump = false, isSliding = false, isFever = false;

    //プレイヤーの各ステータス
    public float _playerHp = 3;
    public float _playerAttack = 10;
    public float _playerBullet = 3;

    public Vector3 _jumpForce = new Vector3(0, 6, 0);

    //現フィーバー値、最大フィーバー値、減少速度、減少値
    public float _feverCount = 0, _maxFeverValue = 100;
    public float _feverInterval = 1, _decrease = 0.1f;

    //移動速度,加速間隔,加速度
    public float _moveSpeed = 4f, _interval = 0.1f, _accel = 0.1f;

    //bulletプレハブ
    public GameObject _bulletPrefab;
    public Transform _muzzle;

    //プレイヤーHPのUI用オブジェクト
    public GameObject[] _playerHpUI;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _capsuleCol = GetComponent<CapsuleCollider>();
        _playerPos = this.transform.position;

    }

    void Update()
    {
        Fever();
        PlayerMove();
        Jump();
        Sliding();
        BulletFire();
    }

    public void Fever()
    {
        _feverTimer += Time.deltaTime;

        if (Mathf.Floor(_feverTimer) % _feverInterval == 0 && isFever == true)
        {
            _feverCount -= _decrease;
        }

        if (_feverCount >= _maxFeverValue)
        {
            isFever = true;
            _moveSpeed += 5;
        }
        else if (_feverCount <= -0.1)
        {
            _feverCount = 0;
            isFever = false;
            _moveSpeed -= 5;
        }
    }

    public void PlayerDamage()
    {
        _playerHp--;
        _moveSpeed /= 2;

        if (_playerHpUI[(int)_playerHp] != null)
        {
            //_playerHpUI[(int)_playerHp]
            _playerHpUI[(int)_playerHp].SetActive(false);
        }
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
        //地面についているかつ、スペースキーまたはWキーが押されたとき
        if (IsGround() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            _rb.AddForce(_jumpForce, ForceMode.Impulse);
            Debug.Log("やっふぅぅぅ");
        }

        if (IsGround()) isJump = false;
        else if (!IsGround()) isJump = true;
    }

    public void Sliding()
    {
        //地面についているかつ、左シフトキーまたはSキーが押されたとき
        if (IsGround() && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.S)))
        {
            StartCoroutine("SwitchSliding");
        }
    }

    public void BulletFire()
    {
        //残弾ありの状態で、左クリックをしたら
        if (_playerBullet > 0 && Input.GetMouseButtonDown(0))
        {
            _playerBullet--;
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.transform.position = _muzzle.position;
            Debug.Log("とんでけ～");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isFever == false)
        {
            PlayerDamage();
        }
    }
}
