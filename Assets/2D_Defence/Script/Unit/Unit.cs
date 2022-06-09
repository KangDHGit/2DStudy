using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MapObject
{
    // 적 찾기
    public GameObject[] _enemyList;

    Rigidbody2D _rigid;
    SpriteRenderer _renderer;
    protected Animator _Anima;
    BoxCollider2D _attackCol;
    public GameObject _enemyObj;
    
    
    public float _speed = 1.0f;
    public float _attackRange = 1.75f; // 공격범위


    override protected void Start()
    {
        base.Start();
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
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // rigidbody를 건드려서 앞으로 이동

        // 1. rigidbody AddForce 함수로 힘(가속도)를 주어서 이동(가속도운동)
        //_rigid.AddForce(new Vector2(10, 0));

        // 2. rigidbody velocity 변수(x축)를 직접 건드리는 방법(등속도운동)
        Move();
        _enemyObj = FindEnemy();
        if (_enemyObj != null)
        {
            CheckDistance();
        }
        else // 적 죽으면 이동하도록
        {
            _Anima.SetBool("attack", false);
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
    public override void DoDamage(int damage)
    {
        base.DoDamage(damage);
        if (_hp > 0.0f)
        {   
            _Anima?.SetTrigger("hit");
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
    
    protected virtual void Attack()
    {
        _Anima.SetBool("attack", true);
    }
    public void OnAttack() // 애니메이션 이벤트 연결용
    {
        Attack();
    }
    GameObject FindEnemy()
    {
        GameObject enemyObj = null;
        // 적을 찾는 로직 구현
        // 적 리스트(배열)에서 가장 첫번째 것을 찾기
        if(_enemyList != null && _enemyList.Length > 0)
        {
            enemyObj = _enemyList[0];
        }

        return enemyObj;
    }
}
