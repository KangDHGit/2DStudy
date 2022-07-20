using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace MyRPG
{
    [Serializable] // 클래스는 Serializable 어트리뷰트가 있어야 인스펙터 창에서 보인다.
    public class PlayerStat
    {
        public int _str;
        public int _dex;
        public int _luk;
        public int _int;
        public int _statPoint;
    }
    public class Player : Unit
    {
        [Header("===== Stat =====")]
        public PlayerStat _stat;
        //_stat을 new를 안해도 되는 이유는 [Serializable]로 인스펙터에 보이고 수정하기 위해서 new가 되있는 상태
        public int _maxLevel = 100;
        public int _level = 1;

        public AnimationCurve _expCurve;
        public long _maxExp = 1000000000;
        public long _exp = 0;

        public long _requiredExp;

        bool m_LeftClick;
        
        bool _KeySpace;
        [SerializeField] float _jumpForce; // private 이지만 인스펙터 창에서는 보인다

        protected override void Init()
        {
            base.Init();
            _ImgHpBar = _uiTrans.Find("UI_Main/HPBar/Img_Fil").GetComponent<Image>();
            if (_ImgHpBar != null)
                _ImgHpBar.fillAmount = 1;
            _ImgDarkHpBar = _uiTrans.Find("UI_Main/HPBar/Img_FilDark").GetComponent<Image>();
            if (_ImgDarkHpBar != null)
                _ImgDarkHpBar.fillAmount = 1;
            _ImgMpBar = _uiTrans.Find("UI_Main/MPBar/Img_Fill").GetComponent<Image>();
            if (_ImgMpBar != null)
                _ImgMpBar.fillAmount = 1;
            GetPPrefsStat();
        }

        protected override void Update()
        {
            base.Update();

            // 경험치
            int nextLevel = _level + 1;
            float expRatio = _expCurve.Evaluate((float)nextLevel / _maxLevel);
            _requiredExp = (long)(_maxExp * expRatio);

            KeyInput();
            Attack(m_LeftClick);
            Jump();
        }

        void GetPPrefsStat()
        {
            if (PlayerPrefs.HasKey("STAT_STR")) // 이미 스탯 랜덤 결정을 한 적이 있음
            {
                _stat._str = PlayerPrefs.GetInt("STAT_STR");
                _stat._dex = PlayerPrefs.GetInt("STAT_DEX");
                _stat._luk = PlayerPrefs.GetInt("STAT_LUK");
                _stat._int = PlayerPrefs.GetInt("STAT_INT");
                _stat._statPoint = PlayerPrefs.GetInt("STAT_POINT");
            }
            else // 게임을 처음 실행하는 상태(스탯 주사위 굴림 필요)
            {
                _stat._str = UnityEngine.Random.Range(5, 10);
                _stat._dex = UnityEngine.Random.Range(5, 10);
                _stat._luk = UnityEngine.Random.Range(5, 10);
                _stat._int = UnityEngine.Random.Range(5, 10);
                _stat._statPoint = 5;

                PlayerPrefs.SetInt("STAT_STR", _stat._str);
                PlayerPrefs.SetInt("STAT_DEX", _stat._dex);
                PlayerPrefs.SetInt("STAT_LUK", _stat._luk);
                PlayerPrefs.SetInt("STAT_INT", _stat._int);
                PlayerPrefs.SetInt("STAT_POINT", _stat._statPoint);
            }
        }

        protected override void ProcessHit(int damage)
        {
            base.ProcessHit(damage);
        }

        void Jump()
        {
            if (_anim.GetBool("isattack") || _anim.GetBool("isjump"))
                return;
            Vector3 force = new Vector3(0, _jumpForce);
            if(_KeySpace == true)
            {
                _rigidbody.AddForce(force);
                _anim.SetTrigger("jump");
                _anim.ResetTrigger("hit");
            }
        }
        void KeyInput()
        {
            m_LeftClick = CrossPlatformInputManager.GetButtonDown("Fire1");
            _KeySpace = Input.GetKeyDown(KeyCode.Space);
        }

        public void SaveStat()
        {
            PlayerPrefs.SetInt("STAT_STR", _stat._str);
            PlayerPrefs.SetInt("STAT_DEX", _stat._dex);
            PlayerPrefs.SetInt("STAT_LUK", _stat._luk);
            PlayerPrefs.SetInt("STAT_INT", _stat._int);
            PlayerPrefs.SetInt("STAT_POINT", _stat._statPoint);
        }
    }
}
