using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �A�C�e���̊��N���X
/// </summary>

public abstract class ItemBase : MonoBehaviour
{
    public abstract void Activate();

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("����͂����I�I");
            Activate();
        }
    }

    protected void Destroy() => Destroy(this.gameObject);
}
