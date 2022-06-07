using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit
{
    public GameObject _arrowTemplate;
    public Transform _firePos;
    protected override void Attack()
    {
        
        // Unit���ִ� Attack �Լ��� ����
        base.Attack();

        // ȭ��(arrow) ��ü�� ����
        if (_arrowTemplate != null)
        {
            GameObject arrowObj = Instantiate(_arrowTemplate);
            arrowObj.SetActive(true);
            arrowObj.transform.position = _firePos.position;
        }
    }
}
