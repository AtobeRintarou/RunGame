using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// アイテムの基底クラス
/// </summary>

public abstract class ItemBase : MonoBehaviour
{
    public abstract void Activate();

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("これはっっ！！");
            Activate();
        }
    }

    protected void Destroy() => Destroy(this.gameObject);
}
