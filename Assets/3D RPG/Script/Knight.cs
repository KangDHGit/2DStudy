using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace MyRPG
{
    public class Knight : Unit
    {
        bool m_LeftClick;

        protected override void Update()
        {
            base.Update();
            m_LeftClick = CrossPlatformInputManager.GetButtonDown("Fire1");
            Attack(m_LeftClick);
        }

        protected override void Init()
        {
            base.Init();
            _attackCol = transform.Find("arm_R_weapon/Knight_handsword").GetComponent<BoxCollider>();
            if (_attackCol != null)
                _attackCol.enabled = false;
            _hpSlider = _uiTrans.Find("UI_Main/Slider_HP").GetComponent<Slider>();
            if (_hpSlider != null)
                _hpSlider.value = 1;
            _hpDarkSlider = _uiTrans.Find("UI_Main/Slider_HP/Slider_HP_Dark").GetComponent<Slider>();
            if (_hpDarkSlider != null)
                _hpDarkSlider.value = 1;
            _mpSlider = _uiTrans.Find("UI_Main/Slider_MP").GetComponent<Slider>();
            if (_mpSlider != null)
                _mpSlider.value = 1;
        }
    }
}
