using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyRPG
{
    public class Player : Unit
    {
        bool m_LeftClick;
        // Start is called before the first frame update
        protected override void Update()
        {
            base.Update();
            m_LeftClick = CrossPlatformInputManager.GetButtonDown("Fire1");
            Attack(m_LeftClick);
        }
    }
}
