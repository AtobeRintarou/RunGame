using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //�J����
    public Camera _cam;

    //�i�ޕ����̊i�[
    private Vector3 _playerPos, _movement;

    //���C���΂��I�u�W�F�N�g�̈ʒu
    public Transform _groundCheckPoint;

    //�n�ʃ��C���[
    public LayerMask _groundLayers;

    //����
    private Rigidbody _rb;

    //�v���C���[�̃R���C�_�[
    private CapsuleCollider _capsuleCol;
    
    //�^�C�}�[
    private float _time, _feverTimer;

    //�W�����v���A�X���C�f�B���O���A�t�B�[�o�[�����ۂ�
    private bool isJump = false, isSliding = false, isFever = false;

    //�v���C���[�̊e�X�e�[�^�X
    public float _playerHp = 3;
    public float _playerAttack = 10;
    public float _playerBullet = 3;

    public Vector3 _jumpForce = new Vector3(0, 6, 0);

    //���t�B�[�o�[�l�A�ő�t�B�[�o�[�l�A�������x�A�����l
    public float _feverCount = 0, _maxFeverValue = 100;
    public float _feverInterval = 1, _decrease = 0.1f;

    //�ړ����x,�����Ԋu,�����x
    public float _moveSpeed = 4f, _interval = 0.1f, _accel = 0.1f;

    //bullet�v���n�u
    public GameObject _bulletPrefab;
    public Transform _muzzle;

    //�v���C���[HP��UI�p�I�u�W�F�N�g
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
        //�i�ޕ������o���ĕϐ��Ɋi�[
        _movement = transform.forward.normalized;

        //z�����ړ�
        this.transform.position += _movement * _moveSpeed * Time.deltaTime;
        _cam.transform.position += _movement * _moveSpeed * Time.deltaTime;

        _time += Time.deltaTime;

        if (Mathf.Floor(_time) % _interval == 0)
        {
            _moveSpeed += _accel;
        }

        //x�����ړ�
        if (isJump == false && isSliding == false)
        {
            if (this.transform.position.x > -1.5 && Input.GetKeyDown(KeyCode.A))
            {
                _playerPos.x -= (float)1.5;
                _playerPos.z = this.transform.position.z;
                this.transform.position = _playerPos;
                Debug.Log("���ɂ�������");
            }
            else if (this.transform.position.x < 1.5 && Input.GetKeyDown(KeyCode.D))
            {
                _playerPos.x += (float)1.5;
                _playerPos.z = this.transform.position.z;
                this.transform.position = _playerPos;
                Debug.Log("�E�ɂӂ�����");
            }
            else if ((this.transform.position.x == -1.5 || this.transform.position.x == 1.5) && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
            {
                Debug.Log("���������A�����Ȃ��ǂ��I");
            }
        }
    }

    public void Jump()
    {
        //�n�ʂɂ��Ă��邩�A�X�y�[�X�L�[�܂���W�L�[�������ꂽ�Ƃ�
        if (IsGround() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            _rb.AddForce(_jumpForce, ForceMode.Impulse);
            Debug.Log("����ӂ�����");
        }

        if (IsGround()) isJump = false;
        else if (!IsGround()) isJump = true;
    }

    public void Sliding()
    {
        //�n�ʂɂ��Ă��邩�A���V�t�g�L�[�܂���S�L�[�������ꂽ�Ƃ�
        if (IsGround() && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.S)))
        {
            StartCoroutine("SwitchSliding");
        }
    }

    public void BulletFire()
    {
        //�c�e����̏�ԂŁA���N���b�N��������
        if (_playerBullet > 0 && Input.GetMouseButtonDown(0))
        {
            _playerBullet--;
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.transform.position = _muzzle.position;
            Debug.Log("�Ƃ�ł��`");
        }
    }

    IEnumerator SwitchSliding()
    {
        isSliding = true;
        _capsuleCol.height = 1;
        Debug.Log("������������");

        //1.5�b�҂�
        yield return new WaitForSeconds((float)1.5);

        isSliding = false;
        _capsuleCol.height = 2;
        Debug.Log("��������");
    }

    //�n�ʂɂ��Ă���� true
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
