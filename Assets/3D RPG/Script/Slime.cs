using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyRPG
{
    public class Slime : Unit
    {
        public float _attackRange;
        public GameObject _enemyObj;

        private void Update()
        {
            CheckDistance();
        }

        void CheckDistance()    // 
        {
            Vector3 pos1 = transform.position; 
            Vector3 pos2 = _enemyObj.transform.position;

            float distance = Vector3.Distance(pos1, pos2);
            if (distance < _attackRange /*&& _hp > 0*/) // ���ݹ��� �ȿ� ������
            {
                // ����
                transform.LookAt(pos2);
                _anim.SetBool("attack", true);
                // ������ ó��
                //Unit enemyUnit = _enemyObj.GetComponent<Unit>();
                //enemyUnit.DoDamege(10);
            }
            else
            {
                _anim.SetBool("attack", false); // ���ݹ����� ����ų� ü���� 0 �̸�
            }
        }
    }
}
