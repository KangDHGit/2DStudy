using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyRPG
{
    public class Unit : MonoBehaviour
    {
        public int _maxHp = 100;
        public int _hp = 0;
        public float _dieDelay;
        public float _reBirthDelay;

        protected BoxCollider _attackCol;
        protected Rigidbody _rigidbody;
        public Animator _anim;

        public Transform _uiTrans;
        protected Image _ImgHpBar;
        protected Image _ImgDarkHpBar;
        protected Image _ImgMpBar;

        Vector3 _rebirthPos;
        // 공격 효과음
        public AudioSource _sound_Attack;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            _rebirthPos = transform.position;
            Init();
        }

        protected virtual void Update()
        {
            HpDarkBarDown();
        }

        protected virtual void Init()
        {
            _anim = GetComponent<Animator>();
            _sound_Attack = GetComponent<AudioSource>();
            _rigidbody = GetComponent<Rigidbody>();

            // 체력 초기화
            _hp = _maxHp;
        }
        protected virtual void OnTriggerEnter(Collider other)
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
                if (_ImgHpBar != null)
                {
                    if (!_ImgHpBar.gameObject.activeSelf)
                        _ImgHpBar.gameObject.SetActive(true);
                    _ImgHpBar.fillAmount = _hp / (float)_maxHp;
                }

                if (_anim != null)
                    _anim.SetTrigger("hit");

                if (_hp <= 0)
                {
                    _anim.SetTrigger("die");
                    Invoke("ActiveFalse", _dieDelay);
                    Invoke("ReBirth", _reBirthDelay);
                }
            }
        }

        private void ThisDestroy()
        {
            Destroy(gameObject);
        }

        private void ReBirth()
        {
            transform.position = _rebirthPos;
            Init();
            gameObject.SetActive(true);
        }

        private void ActiveFalse()
        {
            gameObject.SetActive(false);
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

        public void SetisAttack(int num)
        {
            if (num == 1)
                _anim.SetBool("isattack", true);
            else if (num == 0)
                _anim.SetBool("isattack", false);
        }

        public void SetisJump(int num)
        {
            if (num == 1)
                _anim.SetBool("isjump", true);
            else if (num == 0)
                _anim.SetBool("isjump", false);
        }
        #endregion

        public void HpDarkBarDown()
        {
            if(_ImgDarkHpBar != null)
            {
                if(_ImgDarkHpBar.fillAmount >= _ImgHpBar.fillAmount)
                {
                    _ImgDarkHpBar.fillAmount -= (0.1f * Time.deltaTime);
                }
            }
        }
    }
}
