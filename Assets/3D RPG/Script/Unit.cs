using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyRPG
{
    public class Unit : MonoBehaviour
    {
        public int _maxHp = 100;
        public int _hp = 0;

        BoxCollider _attackCol;
        protected Animator _anim;

        // 공격 효과음
        AudioSource _sound_Attack;

        // Start is called before the first frame update
        void Start()
        {
            if(this is Knight)
            {
                _attackCol = transform.Find("arm_R_weapon/Knight_handsword").GetComponent<BoxCollider>();
                if (_attackCol != null)
                    _attackCol.enabled = false;
            }
            _anim = GetComponent<Animator>();
            _sound_Attack = GetComponent<AudioSource>();

            // 체력 초기화
            _hp = _maxHp;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Post-process Volume")
                return; // 포스트 프로세스 스킵

            if (other.gameObject.name.Contains("Terrain"))
                return; // 터레인 스킵

            string log = string.Format($"this {this.gameObject.name}, other {other.gameObject.name}");
            Debug.Log(log);

            if (other.gameObject.tag == "AttackCol")
            {
                Debug.Log("===== 데미지 발생! =====");

                Debug.Log($"attacker : {other.gameObject.name}");

                Debug.Log("===== 데미지 종료! =====");

                _hp -= 10;
                if (_anim != null)
                    _anim.SetTrigger("hit");


                if (_hp <= 0)
                {
                    _anim.SetTrigger("die");
                    Invoke("ThisDestroy", 1.1f);   
                }
            }
        }
        private void ThisDestroy()
        {
            Destroy(gameObject);
        }

        protected void Attack(bool leftClick)
        {
            if (leftClick)
                if (_anim != null)
                {
                    _anim.SetTrigger("attack");
                }
        }

        #region ANIMATION_EVENT
        // 애니매에션에서 호출되는 함수
        public void AnimSound(string name)
        {
            if(name == "attack")
            {
                _sound_Attack.Play();
            }
        }
        public void SetAttackCol(int num)
        {
            if (num == 1)
                _attackCol.enabled = true;
            else if (num == 0)
                _attackCol.enabled = false;
        }
        #endregion
    }
}
