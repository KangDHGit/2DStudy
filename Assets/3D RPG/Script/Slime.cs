using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyRPG
{
    public class Slime : Unit
    {
        public float _attackRange;
        public GameObject _enemyObj;
        public GameObject _objHpBarTemp;
        public GameObject _objHpBar;
        public Vector3 _hpBarOffset;
        public Camera _camera;

        protected override void Update()
        {
            base.Update();
            CheckDistance();
            //UpdateHpBarPos();
        }

        protected override void Init()
        {
            base.Init();
            //InitHpBar();
            _attackCol = transform.Find("Body").GetComponent<BoxCollider>();
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

        private void InitHpBar()
        {
            _objHpBar = Instantiate(_objHpBarTemp);
            _objHpBar.SetActive(false);
            _ImgHpBar = _objHpBar.GetComponent<Image>();
        }

        private void UpdateHpBarPos()
        {
            Vector3 unitPos = transform.position;
            if (_camera != null)
            {
                Vector3 screenPos = _camera.WorldToScreenPoint(unitPos);
                if (_objHpBar != null)
                {
                    RectTransform barTrans = _objHpBar.GetComponent<RectTransform>();
                    //barTrans.position = screenPos;
                    barTrans.anchoredPosition = screenPos;
                }
            }
        }
    }
}
