using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapObject : MonoBehaviour
{
    // hp바 관련
    public RectTransform _hpBarTrans;
    public Vector3 _hpBarOffset;
    int _maxHp = 100;
    public int _hp = 0;

    virtual protected void Start()
    {
        _hp = _maxHp;   // 체력 초기화
        RefreshHpBar(); // Hp바 갱신
    }
    protected void RefreshHpBar() // 체력바 갱신
    {
        if (_hpBarTrans != null)
        {
            // fill 이미지 컴포넌트 찾기
            Image fill_Img = _hpBarTrans.Find("Fil").GetComponent<Image>();
            fill_Img.fillAmount = (float)_hp / (float)_maxHp;
        }
    }
    public virtual void DoDamage(int damage)
    {
        _hp -= damage;
        // Math는 System이 제공해주는 함수
        _hp = Math.Max(_hp, 0); // _hp와 0 중에 큰값을 리턴
        RefreshHpBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.name == "AttackCol" || collision.name == "Arrow_Blue") // 데미지 처리
        if (collision.tag == "AttackCol")
        {
            DoDamage(10);
            // 화살충돌시 씬 삭제
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            if (arrow != null)
            {
                //Destroy(collision.gameObject);
                Destroy(arrow.gameObject);
            }
        }
    }
}
