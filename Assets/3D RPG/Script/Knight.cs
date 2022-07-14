using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace MyRPG
{
    public class Knight : Player
    {
        protected override void Init()
        {
            base.Init();
            _attackCol = transform.Find("arm_R_weapon/Knight_handsword").GetComponent<BoxCollider>();
            if (_attackCol != null)
                _attackCol.enabled = false;
        }
    }
}
