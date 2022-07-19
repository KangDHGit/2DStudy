using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyRPG
{
    public class UI_Stat : MonoBehaviour
    {
        public Player _player;
        Text _TxtStrPoint;
        Text _TxtDexPoint;
        Text _TxtLukPoint;
        Text _TxtIntPoint;
        
        // Start is called before the first frame update
        void Start()
        {
            _TxtStrPoint = transform.Find("Stat/Str/Txt_StatPoint").GetComponent<Text>();
            if (_TxtStrPoint != null)
                _TxtStrPoint.text = _player._stat._str.ToString();
            _TxtDexPoint = transform.Find("Stat/Dex/Txt_StatPoint").GetComponent<Text>();
            if(_TxtDexPoint != null)
                _TxtDexPoint.text = _player._stat._dex.ToString();
            _TxtLukPoint = transform.Find("Stat/Luk/Txt_StatPoint").GetComponent<Text>();
            if(_TxtLukPoint != null)
                _TxtLukPoint.text = _player._stat._luk.ToString();
            _TxtIntPoint = transform.Find("Stat/Int/Txt_StatPoint").GetComponent<Text>();
            if(_TxtIntPoint != null)
                _TxtIntPoint.text = _player._stat._Int.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateStats();
        }

        void UpdateStats()
        {
            if (!gameObject.activeSelf)
                return;
            _TxtStrPoint.text = _player._stat._str.ToString();
            _TxtDexPoint.text = _player._stat._dex.ToString();
            _TxtLukPoint.text = _player._stat._luk.ToString();
            _TxtIntPoint.text = _player._stat._Int.ToString();
        }
        public void OnButtonStatPoints(GameObject buttonobj)
        {
            //Debug.Log("-------Stat--------");
            //Debug.Log(buttonobj.transform.parent.name);
            //Debug.Log(buttonobj.name);
            //Debug.Log("-------------------");
        }
    }
}
