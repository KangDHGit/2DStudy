using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D _rigid;
    SpriteRenderer _renderer;
    protected Animator _Anima;
    BoxCollider2D _attackCol;
    public GameObject _enemyObj;
    // hp바 관련
    public RectTransform _hpBarTrans;
    public Vector3 _hpBarOffset;
    
    
    int _maxHp = 100;
    public int _hp = 0;    
    public float _speed = 1.0f;
    public float _attackRange = 1.75f; // 공격범위


    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _Anima = GetComponent<Animator>();
        if (transform.Find("AttackCol") != null)
        {
            _attackCol = transform.Find("AttackCol").GetComponent<BoxCollider2D>();
            // 공격 충돌체는 시작할때 꺼둔다
            _attackCol.enabled = false;
        }

        TeamCheck();    // 팀체크
        _hp = _maxHp;   // 체력 초기화
        RefreshHpBar();
    }

    // Update is called once per frame
    void Update()
    {
        // rigidbody를 건드려서 앞으로 이동

        // 1. rigidbody AddForce 함수로 힘(가속도)를 주어서 이동(가속도운동)
        //_rigid.AddForce(new Vector2(10, 0));

        // 2. rigidbody velocity 변수(x축)를 직접 건드리는 방법(등속도운동)

        Move();
        if (_enemyObj != null)
        {
            CheckDistance();
        }
        else // 적 죽으면 이동하도록
        {
            _Anima.SetBool("attack", false);
        }
        UpdateHpBarBos();
    }
    void RefreshHpBar() // 체력바 갱신
    {
        if (_hpBarTrans != null)
        {
            // fill 이미지 컴포넌트 찾기
            Image fill_Img = _hpBarTrans.Find("Fil").GetComponent<Image>();
            fill_Img.fillAmount = (float)_hp / (float)_maxHp;
        }
    }
    void TeamCheck()
    {
        if (_renderer.flipX == true)
            _speed = -_speed;
    }
    void Move()
    {
        Vector2 vel = _rigid.velocity;
        // 공격 애니메이션 인지 확인
        bool isAttacking = _Anima.GetBool("attack");

        if (isAttacking)
            vel.x = 0.0f;
        else
            vel.x = _speed;

        _rigid.velocity = vel;
    }

    void CheckDistance()    // 거리를 체크하는 함수
    {
        // 나와 적 캐릭터 간의 거리를 계산해서 설정된 공격범위 안에 들어오면 공격
        float Pos1 = transform.position.x; // 내 x좌표
        float Pos2 = _enemyObj.transform.position.x; // 적 x좌표
        // 두 캐릭터간의 거리(두 x좌표 사이의 거리)
        float distance = Mathf.Abs(Pos1 - Pos2);

        if (distance < _attackRange /*&& _hp > 0*/) // 공격범위 안에 들어오면
        {
            // 공격
            _Anima.SetBool("attack", true);
            // 데미지 처리
            //Unit enemyUnit = _enemyObj.GetComponent<Unit>();
            //enemyUnit.DoDamege(10);
        }
        else
        {
            _Anima.SetBool("attack", false); // 공격범위를 벗어나거나 체력이 0 이면
        }
    }
    public void DoDamage(int damage)
    {
        _hp -= damage;
        if (_hp > 0.0f)
        {   // Math는 System이 제공해주는 함수
            _hp = Math.Max(_hp, 0); // _hp와 0 중에 큰값을 리턴
            _Anima?.SetTrigger("hit");
            RefreshHpBar();
        }
        else
        {
            _Anima?.SetBool("die", true);
            Invoke("Disappear", 1.5f);
        }
    }
    public void SetAttackCol(int on) // 1 = on, 0 = off
    {
        if (transform.Find("AttackCol") == null)
            return;
        if (on == 1)
            _attackCol.enabled = true;
        if (on == 0)
            _attackCol.enabled = false;
        
    }
    void UpdateHpBarBos() // 체력바가 항상 유닛을 따라다니도록 위치 업데이트
    {
        // 유닛의 위치를 가져와서(월드좌표)
        Vector3 unitPos = transform.position;
        // 월드 좌표를 스크린좌표(UI 좌표로 변환)
        Vector3 screenPos = Camera.main.WorldToScreenPoint(unitPos);
        // 변환된 스크린좌표를 체력바의 rectTransform에 적용
        if (_hpBarTrans != null)
        {
            _hpBarTrans.position = screenPos + _hpBarOffset;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("나는 누구인가 " + name);
        //Debug.Log("충돌한 물체는 누구인가 " + collision.name);
        //if (collision.name == "AttackCol" || collision.name == "Arrow_Blue") // 데미지 처리
        if(collision.tag == "AttackCol")
        {
            DoDamage(10);
        }
    }
    void Disappear()
    {
        Destroy(gameObject);
        Destroy(_hpBarTrans.gameObject);
    }

    protected virtual void Attack()
    {
        _Anima.SetBool("attack", true);
    }
    public void OnAttack() // 애니메이션 이벤트 연결용
    {
        Attack();
    }
}
