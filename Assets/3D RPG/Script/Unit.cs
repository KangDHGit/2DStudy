using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyRPG
{
    public class Unit : MonoBehaviour
    {
        BoxCollider _attackCol;
        // Start is called before the first frame update
        void Start()
        {
            if(this is Knight)
            {
                _attackCol = transform.Find("arm_R_weapon/Knight_handsword").GetComponent<BoxCollider>();

                if(_attackCol != null)
                    _attackCol.enabled = false;
            }
        }

        public void SetAttackCol(int num)
        {
            if (num == 1)
                _attackCol.enabled = true;
            else if(num == 0)
                _attackCol.enabled = false;
        }
    }
}
