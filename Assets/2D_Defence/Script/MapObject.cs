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
        // 팀초기화
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
        if(_team == Team.RED)
        {
            // _gameDir.red_list 배열에서 삭제
            List<GameObject> redList = new List<GameObject>(_gameDir._red_list);
            redList.Remove(gameObject); // 사망한 자신을 빼주고

            _gameDir._red_list = redList.ToArray();
        }
        else if(_team == Team.BLUE)
        {
            // _gameDir.red_list 배열에서 삭제
            List<GameObject> blueList = new List<GameObject>(_gameDir._blue_list);
            blueList.Remove(gameObject); // 사망한 자신을 빼주고

            _gameDir._blue_list = blueList.ToArray();
        }
        Destroy(gameObject);
        if(_hpBarTrans != null)
            Destroy(_hpBarTrans.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.name == "AttackCol" || collision.name == "Arrow_Blue") // 데미지 처리
        if (collision.tag == "AttackCol")
        {
            //// 화살 팀킬 방지
            //if (collision.name == "Arrow" && collision.gameObject.GetComponent<Arrow>()._team == this._team)
            //    return;

            // 화살충돌시 씬 삭제
                Arrow arrow = collision.gameObject.GetComponent<Arrow>();
                if (arrow != null)  // 화살인경우
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
