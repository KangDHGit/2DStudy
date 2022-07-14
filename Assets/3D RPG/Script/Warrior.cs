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
        }
    }
}
