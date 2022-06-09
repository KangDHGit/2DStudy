using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MapObject
{
    public GameObject _fireEffTemplate;
    public override void DoDamage(int damage)
    {
        base.DoDamage(damage);
        // 건물만의 특수처리
        if (_hp <= 0.0f)
        {
            // 파이어 이펙트
            GameObject _fireEffObj = Instantiate(_fireEffTemplate);
            _fireEffObj.SetActive(true);
            _fireEffObj.transform.position = transform.position;
            Invoke("Disappear", 1.5f);
        }
    }
}
