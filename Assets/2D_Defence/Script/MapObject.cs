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

    virtual protected void Start()
    {
        _hp = _maxHp;   // ü�� �ʱ�ȭ
        RefreshHpBar(); // Hp�� ����
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
