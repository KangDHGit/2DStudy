using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class Resource : MonoBehaviour
    {
        int _coin = 9000;
        int _heart = 0;

        Text _coin_Txt;
        Text _heart_Txt;

        // Start is called before the first frame update
        void Start()
        {
            _coin_Txt = transform.Find("Coin").Find("Coin_Txt").GetComponent<Text>();
            _coin_Txt.text = string.Format($"{_coin:D4}");
            _heart_Txt = transform.Find("Heart").Find("Heart_Txt").GetComponent<Text>();
            _heart_Txt.text = string.Format($"{_heart:D4}");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
