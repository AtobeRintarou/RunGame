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

    private bool IsChange = false;

    public GameObject[] _skillZone = new GameObject[9];

    public bool IsSkillCheck = false;

    public float _lifeTime = 3f;

    public GameObject _breakPoint;

    BossController _boss;
    void Start()
    {
        //this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);

        _boss = GameObject.FindGameObjectWithTag("Boss").
            gameObject.GetComponent<BossController>();

        for (int i = 0; i < _skillZone.Length; i++)
        {
            int n = i;
            string sells = $"Zone_" + (n + 1);
            _skillZone[i] = GameObject.Find(sells);
        }
        if (IsSkillCheck == false)
        {
            if (_boss._time <= 120 && _boss._time > 60)
            {
                _skillSpeed *= 2;
                _waitTime /= 2;
            }
            else if (_boss._time <= 60)
            {
                _skillSpeed *= 2;
                _waitTime /= 3;
            }
        }

        ChangePos();

        ToWarning();

        if (IsSkillCheck == true) { SkillAll(); }
    }

    void Update()
    {
        if (_boss.IsSkill == false && IsSkillCheck == false)
        {
            ToReset();
            Destroy(this.gameObject);
        }
        if (_boss._count == 3 && IsSkillCheck == true)
        {
            Debug.Log("Creae");
            ToReset();

            _boss.IsSkill = true;
            _boss._textA.text = " ";

            _boss.StopCoroutine("ToWaiting");
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject, _lifeTime);
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

    public void ChangePos()
    {
        for (int i = 0; i < 9; i++)
        {
            if (_skillPop[i] == true)
            {
                Vector3 _pos = _skillZone[i].transform.position;
                this.gameObject.transform.position = _pos;
            }
        }
    }

    public void SkillAll()
    {
        GameObject breakPointA = Instantiate(_breakPoint);
        breakPointA.transform.position = _skillZone[0].transform.position;
        GameObject breakPointB = Instantiate(_breakPoint);
        breakPointB.transform.position = _skillZone[6].transform.position;
        GameObject breakPointC = Instantiate(_breakPoint);
        breakPointC.transform.position = _skillZone[8].transform.position;


        Destroy(breakPointA,7);
        Destroy(breakPointB,7);
        Destroy(breakPointC,7);

    }

    IEnumerator ToWait()
    {
        yield return new WaitForSeconds((float)_waitTime);

        this.gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 255);

        this.transform.position += (transform.forward * -1) * _skillSpeed * Time.deltaTime;

        if (!IsChange)
        {
            IsChange = true;
            ToReset();
        }
    }
}
