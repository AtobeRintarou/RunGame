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
    private Vector3 _playerPos;

    //�i�ޕ����̊i�[
    private Vector3 _movement;

    //�ړ����x,�����Ԋu,�����x
    public float _moveSpeed = 4f;
    public float _interval = 0.1f;
    public float _accel = 0.1f;

    //�W�����v��
    public Vector3 _jumpForce = new Vector3(0, 6, 0);

    //���C���΂��I�u�W�F�N�g�̈ʒu
    public Transform _groundCheckPoint;

    //�n�ʃ��C���[
    public LayerMask _groundLayers;

    //����
    private Rigidbody _rb;

    //�v���C���[�̃R���C�_�[
    private CapsuleCollider _capsuleCol;
    
    //�^�C�}�[
    public float _time;

    //�W�����v���A�X���C�f�B���O�����ۂ�
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
        //�n�ʂɂ��Ă���A���X�y�[�X�L�[�������ꂽ�Ƃ�
        if (IsGround() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(_jumpForce, ForceMode.Impulse);
            Debug.Log("����ӂ�����");
        }

        if (IsGround()) isJump = false;
        else if (!IsGround()) isJump = true;
    }

    public void Sliding()
    {
        //�n�ʂɂ��Ă���A�����V�t�g�L�[�������ꂽ�Ƃ�
        if (IsGround() && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine("SwitchSliding");
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
}
