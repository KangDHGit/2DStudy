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

    // 이팩트
    public GameObject _hitEffTemplate; // 피격 이펙트 원본

    virtual protected void Start()
    {
        _hp = _maxHp;   // 체력 초기화
        RefreshHpBar(); // Hp바 갱신
    }

    virtual protected void Update()
    {
        UpdateHpBarPos(); // 체력바 위치 갱신
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
        if (_hitEffTemplate != null)// 피격 파티클 이벤트 재생
            PlayHitEffect();
    }
    void PlayHitEffect()
    {
        GameObject hitEffObj = Instantiate(_hitEffTemplate);
        hitEffObj.SetActive(true);
        hitEffObj.transform.position = transform.position;
    }

    void UpdateHpBarPos() // 체력바가 항상 유닛을 따라다니도록 위치 업데이트
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
    protected void Disappear()
    {
        Destroy(gameObject);
        if(_hpBarTrans != null)
            Destroy(_hpBarTrans.gameObject);
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
