using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEditor.Experimental.GraphView.GraphView;
using Unity.VisualScripting;

public class BossSkills : MonoBehaviour
{
    public float _skillSpeed = 5;

    public float _waitTime = 2;

    public Color  _red  = Color.red;
    public Color  _gray  = Color.gray;

    public int Title;
    public int Title2;

    public bool[] _skillPop = new bool[9];
    public bool[] _skillRange = new bool[9];

    public GameObject[] _skillZone = new GameObject[9];

    BossController _boss;
    void Start()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);

        _boss = GameObject.FindGameObjectWithTag("Boss").
            gameObject.GetComponent<BossController>();

        for (int i = 0; i < _skillZone.Length; i++)
        {
            int n = i;
            string sells = $"Zone_" + (n + 1);
            _skillZone[i] = GameObject.Find(sells);
        }

        if (_boss._bossHp <= (_boss._bossMaxHp /= 2))
        {
            _skillSpeed *= 1.5f;
        }

        ToWarning();
    }

    void Update()
    {
        StartCoroutine("ToWait");
    }

    public void ToWarning()
    {
        for (int i = 0; i < 9; i++)
        {
            if (_skillRange[i] == true)
            {
                _skillZone[i].GetComponent<Renderer>().material.color = _red;
            }
            else
            {
                _skillZone[i].GetComponent<Renderer>().material.color = _gray;
            }
        }
    }

    public void ToReset()
    {
        for (int i = 0; i < 9; i++)
        {
            if (_skillRange[i] == true)
            {
                _skillZone[i].GetComponent<Renderer>().material.color = _gray;
            }
            else
            {
                _skillZone[i].GetComponent<Renderer>().material.color = _gray;
            }
        }
    }

    IEnumerator ToWait()
    {
        yield return new WaitForSeconds((float)_waitTime);

        this.gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 255);
        this.transform.position += (transform.forward * -1) * _skillSpeed * Time.deltaTime;

        ToReset();
    }
}
