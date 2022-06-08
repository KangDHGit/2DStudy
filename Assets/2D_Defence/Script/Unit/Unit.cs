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
    // hp�� ����
    public RectTransform _hpBarTrans;
    public Vector3 _hpBarOffset;
    
    
    int _maxHp = 100;
    public int _hp = 0;    
    public float _speed = 1.0f;
    public float _attackRange = 1.75f; // ���ݹ���


    void Start()
    {
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
        _hp = _maxHp;   // ü�� �ʱ�ȭ
        RefreshHpBar();
    }

    // Update is called once per frame
    void Update()
    {
        // rigidbody�� �ǵ���� ������ �̵�

        // 1. rigidbody AddForce �Լ��� ��(���ӵ�)�� �־ �̵�(���ӵ��)
        //_rigid.AddForce(new Vector2(10, 0));

        // 2. rigidbody velocity ����(x��)�� ���� �ǵ帮�� ���(��ӵ��)

        Move();
        if (_enemyObj != null)
        {
            CheckDistance();
        }
        else // �� ������ �̵��ϵ���
        {
            _Anima.SetBool("attack", false);
        }
        UpdateHpBarBos();
    }
    void RefreshHpBar() // ü�¹� ����
    {
        if (_hpBarTrans != null)
        {
            // fill �̹��� ������Ʈ ã��
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
    public void DoDamage(int damage)
    {
        _hp -= damage;
        if (_hp > 0.0f)
        {   // Math�� System�� �������ִ� �Լ�
            _hp = Math.Max(_hp, 0); // _hp�� 0 �߿� ū���� ����
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
    void UpdateHpBarBos() // ü�¹ٰ� �׻� ������ ����ٴϵ��� ��ġ ������Ʈ
    {
        // ������ ��ġ�� �����ͼ�(������ǥ)
        Vector3 unitPos = transform.position;
        // ���� ��ǥ�� ��ũ����ǥ(UI ��ǥ�� ��ȯ)
        Vector3 screenPos = Camera.main.WorldToScreenPoint(unitPos);
        // ��ȯ�� ��ũ����ǥ�� ü�¹��� rectTransform�� ����
        if (_hpBarTrans != null)
        {
            _hpBarTrans.position = screenPos + _hpBarOffset;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("���� �����ΰ� " + name);
        //Debug.Log("�浹�� ��ü�� �����ΰ� " + collision.name);
        //if (collision.name == "AttackCol" || collision.name == "Arrow_Blue") // ������ ó��
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
    public void OnAttack() // �ִϸ��̼� �̺�Ʈ �����
    {
        Attack();
    }
}
