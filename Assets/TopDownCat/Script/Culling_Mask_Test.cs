using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCat
{
    public class Culling_Mask_Test : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log($"Player : {LayerMask.NameToLayer("Player")}");
            //Debug.Log($"BackGround : {LayerMask.NameToLayer("BackGround")}");
            //Debug.Log($"Item : {LayerMask.NameToLayer("Item")}");

            //Debug.Log(Camera.main);

            // 플레이어만 나오도록
            //Camera.main.cullingMask = (1 << LayerMask.NameToLayer("Player"));

            // 플레이어만 뺀상태
            //Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Player"));

            // 플레이어와 아이템만 카메라에 나오도록
            //Camera.main.cullingMask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("item"));
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
