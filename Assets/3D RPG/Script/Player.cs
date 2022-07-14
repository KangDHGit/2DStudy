using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace MyRPG
{
    public class Player : Unit
    {
        bool m_LeftClick;
        
        bool _KeySpace;
        public float _jumpForce;
        // Start is called before the first frame update
        protected override void Update()
        {
            base.Update();
            KeyInput();
            Attack(m_LeftClick);
            Jump();
        }
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
            }
        }
        void KeyInput()
        {
            m_LeftClick = CrossPlatformInputManager.GetButtonDown("Fire1");
            _KeySpace = Input.GetKeyDown(KeyCode.Space);
        }
    }
}
