using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyRPG
{
    public class Slime : Unit
    {
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "AttackCol")
            {
                Debug.Log("슬라임 피격");
            }
        }
    }
}
