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
    private Vector3 _movement;

    //�ړ����x
    public float _moveSpeed = 4f;

    //�W�����v��
    public Vector3 _jumpForce = new Vector3(0, 6, 0);

    //���C���΂��I�u�W�F�N�g�̈ʒu
    public Transform _groundCheckPoint;

    //�n�ʃ��C���[
    public LayerMask _groundLayers;

    //����
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
        //�i�ޕ������o���ĕϐ��Ɋi�[
        _movement = transform.forward.normalized;

        //���݈ʒu�ɔ��f
        this.transform.position += _movement * _moveSpeed * Time.deltaTime;
        _cam.transform.position += _movement * _moveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        //�n�ʂɂ��Ă���A���X�y�[�X�L�[�������ꂽ�Ƃ�
        if (IsGround() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(_jumpForce, ForceMode.Impulse);
        }
    }

    //�n�ʂɂ��Ă���� true
    public bool IsGround()
    {
        return Physics.Raycast(_groundCheckPoint.position, Vector3.down, 0.25f, _groundLayers);
    }
}
