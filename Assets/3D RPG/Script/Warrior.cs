using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyRPG
{
    public class Warrior : Player
    {
        protected override void Init()
        {
            base.Init();
            _attackCol = transform.Find("arm_R_weapon/warrior_handsword").GetComponent<BoxCollider>();
            if (_attackCol != null)
                _attackCol.enabled = false;
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
    }
}
