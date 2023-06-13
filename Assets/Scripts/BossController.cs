using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("--- Bossステータス ---")]
    public float _bossHp = 1000;
    public float _bossSpeed = 2f;

    //[HideInInspector]
    public float _bossHarfHp;
    public float _bossHp3of2;
    public float _bossHp5of1;

    public float _time = 180;

    [Space(15),Header("--- スキルオブジェクト ---")]
    public GameObject[] _skillObj;
    public GameObject[] _bigTrickObj;

    [Space(15), Header("--- スキルクールタイム（秒） ---")]
    public float _waitTime = 1.5f;
    public float _stageTime = 0;
    private float _3of2Wait;
    private float _3of1Wait;

    private float _skillTime = 0f;

    //private GameObject _LookObj;
    //private Vector3 _lookPos;

    //private Vector3 startPosition, targetPosition;
    //private Vector3 bossStartPosition, bossTargetPosition;

    [Space(15), Header("--- Camera 移動 ---")]
    public float _cameraSpeed = 1.5f;

    //public GameObject _targetObj;
    //public GameObject _bossTargetObj;

    //private bool IsCameraMove = false;
    //private bool IsCameraRet = false;

    //[HideInInspector]
    public int _count = 0;
    public int _LastCount = 0;

    public bool IsSkill = true;
    public bool IsSkillA = true;
    public bool IsSkillB = true;
    public bool IsSkillC = true;

    public bool IsNock = false;
    public bool IsChance = false;
    public bool IsCheck = false;
    public bool IsLast = false;

    //private bool IsBossMove = false;

    public Text _textA;
    public Text _textB;
    public Text _textC;

    public GameObject _skillBObj;

    private Animator _anim;
    private Animator _camAnim;

    PlayerController _player;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _camAnim = Camera.main.GetComponent<Animator>();

        _bossHarfHp = _bossHp / 2;
        _3of1Wait = _waitTime / 4;
        _3of2Wait = ((_waitTime / 3) * 2);
        _bossHp3of2 = ((_bossHp / 3) * 2);
        _bossHp5of1 = _bossHp / 5;

        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _player._moveSpeed = 0;
        _player._interval = 0;

        /*_LookObj = GameObject.Find("LookPoint");
        _lookPos = _LookObj.transform.position;

        //スタート位置をキャッシュ
        startPosition = Camera.main.transform.position;
        bossStartPosition = this.transform.position;

        targetPosition = _targetObj.transform.position;
        bossTargetPosition = _bossTargetObj.transform.position;*/
    }

    void Update()
    {
        //技の発生
        _skillTime += Time.deltaTime;
        _time -= Time.deltaTime;

        if (_skillTime >= _waitTime && IsSkill == true)
        {
            _skillTime = 0;
            GameObject num = _skillObj[Random.Range(0, _skillObj.Length)];
            Instantiate(num);
        }

        if (_time <= 120 && _time > 60)
        {
            _waitTime = _3of2Wait;
        }
        else if (_time <= 60)
        {
            _waitTime = _3of1Wait;
        }

        //_stageTime += Time.deltaTime;
        //if (_bossHp <= _bossHarfHp) { IsSkillB = true; }
        //if (_stageTime >= 60) { SkillB(); }

        /*if (this.gameObject.transform.position ==  bossStartPosition)
        {
            StopCoroutine(ToWait());
        }
        else if (this.gameObject.transform.position == bossTargetPosition)
        {
            IsBossMove = false;
        }

        if (IsBossMove == true)
        {
            this.transform.position = Vector3.Lerp(bossStartPosition, bossTargetPosition, Time.time / _cameraSpeed);
        }*/


        _anim.SetBool("isBossRet", false);
        _camAnim.SetBool("isCameraRet", false);

        if (_bossHp <= _bossHp3of2 && IsSkillA == true)//BossHPが３分の２
        {
            IsSkillA = false;
            IsSkill = false;
            _textA.text = "狙って撃て！";

            Instantiate(_bigTrickObj[0]);
            StartCoroutine("ToWaiting");
        }
        else if (_bossHp <= _bossHarfHp && IsSkillB == true)//BossHPが２分の１
        {
            //Debug.Log("Harf");
            //IsCameraMove = true;
            //IsBossMove = true;5

            IsSkillB = false;
            IsSkill = false;

            _anim.SetBool("isBossMove", true);
            _camAnim.SetBool("isCameraMove", true);

            SkillB();

            StartCoroutine("ToWait");
        }
        else if (_bossHp <= _bossHp5of1 && IsSkillC == true)//BossHPが5分の1
        {
            //IsBossMove = true;

            IsSkillC = false;
            IsSkill = false;

            _camAnim.SetBool("isCameraMove", true);

            SkillC();
        }

        /*if (IsCameraMove == true)
        {
            CameraMove();
        }
        if (IsCameraRet == true)
        {
            ReturnCamera();
        }*/

        if (IsCheck == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                IsCheck = false;
                Debug.Log("Enter");

                _textB.text = "チャンスだ！";

                StopCoroutine("ToWait");
                StartCoroutine("ToNock");
            }
        }

        if (IsLast == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _LastCount++;
            }
        }
    }

    public void SkillB()
    {
        _skillBObj.SetActive(true);
        _textB.text = "タイミングよく「右クリ」";
        IsChance = true;
    }
    
    public void SkillC()
    {
        _textC.text = "連打ぁぁ！！「Enter」";
        IsLast = true;
    }

    /*public void ReturnBoss()
    {
        this.transform.position = Vector3.Lerp(bossTargetPosition, bossStartPosition, Time.time / _cameraSpeed);
    }

    public void ReturnCamera()
    {
        Vector3 vector3 = _lookPos - Camera.main.transform.position;

        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vector3);

        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, quaternion, 1);

        //移動させる
        Camera.main.transform.position = Vector3.Lerp(targetPosition, startPosition, Time.time / _cameraSpeed);

        if (this.gameObject.transform.position == startPosition) { IsCameraRet = false; }
    }

    public void CameraMove()
    {
        Vector3 vector3 = _lookPos - Camera.main.transform.position;

        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vector3);

        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, quaternion, 1);

        //移動させる
        Camera.main.transform.position = Vector3.Lerp(startPosition, targetPosition, Time.time / _cameraSpeed);
        if (this.gameObject.transform.position == targetPosition) { IsCameraMove = false; }
    }*/

    IEnumerator ToWaiting()
    {
        yield return new WaitForSeconds((float)10);

        _textA.text = " ";

        IsSkill = true;
    }

    IEnumerator ToWait()
    {
        yield return new WaitForSeconds((float)10);

        if (IsNock == false) { _anim.SetBool("isBossAttack", true); }

        StartCoroutine("A");

        /*else if (IsNock == true)
        {
            _textB.text = "チャンスだ！";
            _textC.text = "チャンスだ！";

            StartCoroutine("ToNock");
        }*/

        //if (Camera.main.transform.position == targetPosition) { IsCameraRet = true; }
        //if (this.gameObject.transform.position == bossTargetPosition) { ReturnBoss(); }
    }

    IEnumerator A()
    {
        yield return new WaitForSeconds((float)1);

        if (IsNock == false)
        {
            IsSkill = true;

            _anim.SetBool("isBossAttack", false);

            _anim.SetBool("isBossMove", false);
            _camAnim.SetBool("isCameraMove", false);

            _anim.SetBool("isBossRet", true);
            _camAnim.SetBool("isCameraRet", true);

            _textA.text = " ";
            _textB.text = " ";
            _textC.text = " ";
        }

        StartCoroutine("B");
    }

    IEnumerator B()
    {
        yield return new WaitForSeconds((float)1);

        _anim.SetBool("isBossRet", false);
        _camAnim.SetBool("isCameraRet", false);
        _skillBObj.SetActive(false);

    }

    IEnumerator ToNock()
    {
        yield return new WaitForSeconds((float)5);
        IsSkill = true;

        _anim.SetBool("isBossMove", false);
        _camAnim.SetBool("isCameraMove", false);

        _anim.SetBool("isBossRet", true);
        _camAnim.SetBool("isCameraRet", true);

        _textA.text = " ";
        _textB.text = " ";
        _textC.text = " ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && IsChance == true)
        {
            IsCheck = true;
            _textB.text = "今だ！！";
        }
    }
}
