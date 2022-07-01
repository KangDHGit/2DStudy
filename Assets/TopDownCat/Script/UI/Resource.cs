using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class Resource : MonoBehaviour
    {
        const string KEY_RES_COIN = "Res_CoinCount";
        const string KEY_RES_HEART = "Res_HeartCount";

        int _coin = 0;
        int _heart = 0;

        Text _coin_Txt;
        Text _heart_Txt;

        // Start is called before the first frame update
        void Start()
        {
            // 저장된 값 로드
            if (PlayerPrefs.HasKey(KEY_RES_COIN))
                _coin = PlayerPrefs.GetInt(KEY_RES_COIN);


            if (PlayerPrefs.HasKey(KEY_RES_HEART))
                _heart = PlayerPrefs.GetInt(KEY_RES_HEART);

            _coin_Txt = transform.Find("Coin").Find("Coin_Txt").GetComponent<Text>();
            _coin_Txt.text = string.Format($"{_coin:D4}");

            _heart_Txt = transform.Find("Heart").Find("Heart_Txt").GetComponent<Text>();
            _heart_Txt.text = string.Format($"{_heart:D4}");

        }

        public void CalculateHeart(int count)
        {
            if (count < 0 && _heart < Mathf.Abs(count))
                return;

            _heart += count;

            PlayerPrefs.SetInt(KEY_RES_HEART, _heart);
            _heart_Txt.text = _heart.ToString("D4");
        }
        
        public bool CalculateCoin(int count)
        {
            // count가 음수이고 절대값이 소지한 coin보다 클경우 리턴
            if (count < 0 && _coin < Mathf.Abs(count))
                return false;
            else
            {
                _coin += count;

                // 저장할 값 세이브
                PlayerPrefs.SetInt(KEY_RES_COIN, _coin);
                _coin_Txt.text = string.Format($"{_coin:D4}");
                return true;
            }
        }
    }
}
