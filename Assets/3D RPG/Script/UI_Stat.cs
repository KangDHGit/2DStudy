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
        Text _txtStrPoint;
        Text _txtDexPoint;
        Text _txtLukPoint;
        Text _txtIntPoint;
        Text _txtstatPoint;
        
        void Update()
        {
            UpdateStats();
        }

        public void InitTxt()
        {
            _txtStrPoint = transform.Find("Stat/Str/Txt_StatPoint").GetComponent<Text>();
            _txtDexPoint = transform.Find("Stat/Dex/Txt_StatPoint").GetComponent<Text>();
            _txtLukPoint = transform.Find("Stat/Luk/Txt_StatPoint").GetComponent<Text>();
            _txtIntPoint = transform.Find("Stat/Int/Txt_StatPoint").GetComponent<Text>();
            _txtstatPoint = transform.Find("Stat/StatPoint/Txt_Point").GetComponent<Text>();
        }

        void UpdateStats()
        {
            if (!gameObject.activeSelf)
                return;
            if (_txtStrPoint != null)
                _txtStrPoint.text = _player._stat._str.ToString();
            if (_txtDexPoint != null)
                _txtDexPoint.text = _player._stat._dex.ToString();
            if (_txtLukPoint != null)
                _txtLukPoint.text = _player._stat._luk.ToString();
            if (_txtIntPoint != null)
                _txtIntPoint.text = _player._stat._int.ToString();
            if (_txtstatPoint != null)
                _txtstatPoint.text = _player._stat._statPoint.ToString();
        }
        public void OnButtonStatPoints(GameObject buttonobj)
        {
            //Debug.Log("-------Stat--------");
            //Debug.Log(buttonobj.transform.parent.name);
            //Debug.Log(buttonobj.name);
            //Debug.Log("-------------------");
            if (_player._stat._statPoint == 0)
                return;
            switch (buttonobj.transform.parent.name)
            {
                case "Str":
                    if(buttonobj.name.Contains("Up"))
                    {
                        _player._stat._str++;
                    }
                    else if(buttonobj.name.Contains("Down"))
                    {
                        _player._stat._str--;
                    }
                    break;
                case "Dex":
                    if (buttonobj.name.Contains("Up"))
                    {
                        _player._stat._dex++;
                    }
                    else if (buttonobj.name.Contains("Down"))
                    {
                        _player._stat._dex--;
                    }
                    break;
                case "Luk":
                    if (buttonobj.name.Contains("Up"))
                    {
                        _player._stat._luk++;
                    }
                    else if (buttonobj.name.Contains("Down"))
                    {
                        _player._stat._luk--;
                    }
                    break;
                case "Int":
                    if (buttonobj.name.Contains("Up"))
                    {
                        _player._stat._int++;
                    }
                    else if (buttonobj.name.Contains("Down"))
                    {
                        _player._stat._int--;
                    }
                    break;
                default:
                    break;
            }
        }
        
        public void OnButtonDecision()
        {
            _player.SaveStat();
        }
    }
}
