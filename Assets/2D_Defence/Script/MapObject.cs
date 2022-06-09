using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapObject : MonoBehaviour
{
    // hp�� ����
    public RectTransform _hpBarTrans;
    public Vector3 _hpBarOffset;
    int _maxHp = 100;
    public int _hp = 0;

    // ����Ʈ
    public GameObject _hitEffTemplate; // �ǰ� ����Ʈ ����

    virtual protected void Start()
    {
        _hp = _maxHp;   // ü�� �ʱ�ȭ
        RefreshHpBar(); // Hp�� ����
    }

    virtual protected void Update()
    {
        UpdateHpBarPos(); // ü�¹� ��ġ ����
    }
    protected void RefreshHpBar() // ü�¹� ����
    {
        if (_hpBarTrans != null)
        {
            // fill �̹��� ������Ʈ ã��
            Image fill_Img = _hpBarTrans.Find("Fil").GetComponent<Image>();
            fill_Img.fillAmount = (float)_hp / (float)_maxHp;
        }
    }
    public virtual void DoDamage(int damage)
    {
        _hp -= damage;
        // Math�� System�� �������ִ� �Լ�
        _hp = Math.Max(_hp, 0); // _hp�� 0 �߿� ū���� ����
        RefreshHpBar();
        if (_hitEffTemplate != null)// �ǰ� ��ƼŬ �̺�Ʈ ���
            PlayHitEffect();
    }
    void PlayHitEffect()
    {
        GameObject hitEffObj = Instantiate(_hitEffTemplate);
        hitEffObj.SetActive(true);
        hitEffObj.transform.position = transform.position;
    }

    void UpdateHpBarPos() // ü�¹ٰ� �׻� ������ ����ٴϵ��� ��ġ ������Ʈ
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
    protected void Disappear()
    {
        Destroy(gameObject);
        if(_hpBarTrans != null)
            Destroy(_hpBarTrans.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.name == "AttackCol" || collision.name == "Arrow_Blue") // ������ ó��
        if (collision.tag == "AttackCol")
        {
            DoDamage(10);
            // ȭ���浹�� �� ����
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            if (arrow != null)
            {
                //Destroy(collision.gameObject);
                Destroy(arrow.gameObject);
            }
        }
    }
}
