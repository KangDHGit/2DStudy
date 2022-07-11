using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyRPG
{
    public class Knight : Unit
    {
        bool m_LeftClick;
        private void Update()
        {
            m_LeftClick = CrossPlatformInputManager.GetButtonDown("Fire1");
            Attack(m_LeftClick);
        }
    }
}
