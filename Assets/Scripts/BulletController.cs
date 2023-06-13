using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private RaycastHit _hit;

    public LayerMask _enemyLayer;
    public LayerMask _bossLayers;
    public LayerMask _breakLayers;

    public float _bulltSpeed = 3f;
    public float _BFbulltSpeed = 3f;
    public float _lifeTime = 5f;

    PlayerController _player;
    BossController _boss;
    void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj) _player = playerObj.GetComponent<PlayerController>();

        var bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj) _boss = bossObj.GetComponent<BossController>();
    }

    void Update()
    {
        if (!GameObject.Find("Boss"))
        {
            _bulltSpeed = _player._moveSpeed;
            this.transform.position += transform.forward * (_bulltSpeed *= 4) * Time.deltaTime;
        }
        else if (GameObject.Find("Boss"))
        {
            this.transform.position += transform.forward * _BFbulltSpeed * Time.deltaTime;
        }

        Destroy(this.gameObject, _lifeTime);

        Ray ray = new Ray(this.gameObject.transform.position, new Vector3(0, 0, 1));

        if (Physics.Raycast(ray, out _hit, 0.25f, _enemyLayer))
        {
            Debug.Log("hitEnemy");
            Destroy(_hit.collider.gameObject);
            Destroy(this.gameObject);
        }
        else if (Physics.Raycast(ray, out _hit, 0.25f, _bossLayers))
        {
            Debug.Log("hiBosst");
            _boss._bossHp -= _player._playerAttack;
            Destroy(this.gameObject);
        }
        else if (Physics.Raycast(ray, out _hit, 0.25f, _breakLayers))
        {
            Debug.Log("hitBreak");
            _boss._count++;
            Destroy(_hit.collider.gameObject);
            Destroy(this.gameObject);
        }
    }
}
