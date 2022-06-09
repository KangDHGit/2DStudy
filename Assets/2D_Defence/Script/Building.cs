using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MapObject
{
    public GameObject _fireEffTemplate;
    public override void DoDamage(int damage)
    {
        base.DoDamage(damage);
        // �ǹ����� Ư��ó��
        if (_hp <= 0.0f)
        {
            // ���̾� ����Ʈ
            GameObject _fireEffObj = Instantiate(_fireEffTemplate);
            _fireEffObj.SetActive(true);
            _fireEffObj.transform.position = transform.position;
            Invoke("Disappear", 1.5f);
        }
    }
}
