using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit
{
    public GameObject _arrowTemplate;
    public Transform _firePos;
    protected override void Attack()
    {
        
        // Unit에있는 Attack 함수를 실행
        base.Attack();

        // 화살(arrow) 객체를 생성
        if (_arrowTemplate != null)
        {
            GameObject arrowObj = Instantiate(_arrowTemplate);
            arrowObj.SetActive(true);
            arrowObj.transform.position = _firePos.position;
        }
    }
}
