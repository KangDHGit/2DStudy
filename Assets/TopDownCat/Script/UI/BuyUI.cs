using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class BuyUI : MonoBehaviour
    {
        public Transform _uiTrans;
        GameObject _resource;
        public Text _itemPrice_Txt;
        // Start is called before the first frame update
        void Start()
        {
            _resource = _uiTrans.Find("Resource").gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

    }
}
