using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Team
{
    None,
    BLUE,
    RED,
}

public class MapObject : MonoBehaviour
{
    protected GameDirector _gameDir;

    public Team _team;
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
        // ���ʱ�ȭ
        if (transform.parent != null)
        {
            if (transform.parent.gameObject.name == "Red")
            {
                _team = Team.RED;
            }
            else if (transform.parent.gameObject.name == "Blue")
            {
                _team = Team.BLUE;
            }
        }
        _gameDir = transform.parent.parent.GetComponent<GameDirector>();
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
        if(_team == Team.RED)
        {
            // _gameDir.red_list �迭���� ����
            List<GameObject> redList = new List<GameObject>(_gameDir._red_list);
            redList.Remove(gameObject); // ����� �ڽ��� ���ְ�

            _gameDir._red_list = redList.ToArray();
        }
        else if(_team == Team.BLUE)
        {
            // _gameDir.red_list �迭���� ����
            List<GameObject> blueList = new List<GameObject>(_gameDir._blue_list);
            blueList.Remove(gameObject); // ����� �ڽ��� ���ְ�

            _gameDir._blue_list = blueList.ToArray();
        }
        Destroy(gameObject);
        if(_hpBarTrans != null)
            Destroy(_hpBarTrans.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.name == "AttackCol" || collision.name == "Arrow_Blue") // ������ ó��
        if (collision.tag == "AttackCol")
        {
            //// ȭ�� ��ų ����
            //if (collision.name == "Arrow" && collision.gameObject.GetComponent<Arrow>()._team == this._team)
            //    return;

            // ȭ���浹�� �� ����
                Arrow arrow = collision.gameObject.GetComponent<Arrow>();
                if (arrow != null)  // ȭ���ΰ��
                {
                    if (arrow._team == this._team)
                        return;
                    //Destroy(collision.gameObject);
                    Destroy(arrow.gameObject);
                }
                else
                {
                    MapObject attacker = collision.transform.parent.GetComponent<MapObject>();
                    if(attacker != null && attacker._team == this._team)
                    {
                        return;
                    }
                }

            DoDamage(10);
        }
    }
}
