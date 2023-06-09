using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private RaycastHit _hit;

    private Vector3 _movement;

    public LayerMask _EnemyLayers;

    public float _bulltSpeed = 3f;
    public float _lifeTime = 5f;

    PlayerController _player;

    void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj) _player = playerObj.GetComponent<PlayerController>();
    }

    void Update()
    {
        _bulltSpeed = _player._moveSpeed;
        _movement = transform.forward.normalized;
        this.transform.position += transform.forward * (_bulltSpeed *= 4) * Time.deltaTime;
        Destroy(this.gameObject, _lifeTime);

        Ray ray = new Ray(this.gameObject.transform.position, new Vector3(0, 0, 1));

        if (Physics.Raycast(ray, out _hit, 0.1f, _EnemyLayers))
        {
            Debug.Log("hit");
            Destroy(_hit.collider.gameObject);
            Destroy(this.gameObject);
        }
    }
}
