using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MapObject
{
    // �� ã��
    public GameObject[] _enemyList;

    Rigidbody2D _rigid;
    SpriteRenderer _renderer;
    protected Animator _Anima;
    BoxCollider2D _attackCol;
    public GameObject _enemyObj;
    
    
    public float _speed = 1.0f;
    public float _attackRange = 1.75f; // ���ݹ���


    override protected void Start()
    {
        base.Start();
        _rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _Anima = GetComponent<Animator>();
        if (transform.Find("AttackCol") != null)
        {
            _attackCol = transform.Find("AttackCol").GetComponent<BoxCollider2D>();
            // ���� �浹ü�� �����Ҷ� ���д�
            _attackCol.enabled = false;
        }

        TeamCheck();    // ��üũ
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // rigidbody�� �ǵ���� ������ �̵�

        // 1. rigidbody AddForce �Լ��� ��(���ӵ�)�� �־ �̵�(���ӵ��)
        //_rigid.AddForce(new Vector2(10, 0));

        // 2. rigidbody velocity ����(x��)�� ���� �ǵ帮�� ���(��ӵ��)
        Move();
        _enemyObj = FindEnemy();
        if (_enemyObj != null)
        {
            CheckDistance();
        }
        else // �� ������ �̵��ϵ���
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
        // ���� �ִϸ��̼� ���� Ȯ��
        bool isAttacking = _Anima.GetBool("attack");

        if (isAttacking)
            vel.x = 0.0f;
        else
            vel.x = _speed;

        _rigid.velocity = vel;
    }

    void CheckDistance()    // �Ÿ��� üũ�ϴ� �Լ�
    {
        // ���� �� ĳ���� ���� �Ÿ��� ����ؼ� ������ ���ݹ��� �ȿ� ������ ����
        float Pos1 = transform.position.x; // �� x��ǥ
        float Pos2 = _enemyObj.transform.position.x; // �� x��ǥ
        // �� ĳ���Ͱ��� �Ÿ�(�� x��ǥ ������ �Ÿ�)
        float distance = Mathf.Abs(Pos1 - Pos2);

        if (distance < _attackRange /*&& _hp > 0*/) // ���ݹ��� �ȿ� ������
        {
            // ����
            _Anima.SetBool("attack", true);
            // ������ ó��
            //Unit enemyUnit = _enemyObj.GetComponent<Unit>();
            //enemyUnit.DoDamege(10);
        }
        else
        {
            _Anima.SetBool("attack", false); // ���ݹ����� ����ų� ü���� 0 �̸�
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
    public void OnAttack() // �ִϸ��̼� �̺�Ʈ �����
    {
        Attack();
    }
    GameObject FindEnemy()
    {
        GameObject enemyObj = null;
        // ���� ã�� ���� ����
        // �� ����Ʈ(�迭)���� ���� ù��° ���� ã��
        if(_enemyList != null && _enemyList.Length > 0)
        {
            enemyObj = _enemyList[0];
        }

        return enemyObj;
    }
}
