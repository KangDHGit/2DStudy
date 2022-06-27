using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class BuyUI : MonoBehaviour
    {
        public Transform _uiTrans;
        Text _resourceCoin;
        Text _resourceHeart;
        public Text _itemPrice_Txt;
        // Start is called before the first frame update
        void Start()
        {
            _resourceCoin = _uiTrans.Find("Resource").Find("Coin").Find("Coin_Txt").GetComponent<Text>();
            _resourceHeart = _uiTrans.Find("Resource").Find("Heart").Find("Heart_Txt").GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void CalculateResource()
        {
            if (_itemPrice_Txt == null)
                return;
            if (_itemPrice_Txt.text.Length > 3)
            {
                _itemPrice_Txt.text.Replace(",", "");
            }
            int.TryParse(_itemPrice_Txt.text, out int itemPrice);
            int.TryParse(_resourceCoin.text, out int coin);
            if (coin < itemPrice)
                return;
            _resourceCoin.text = (coin - itemPrice).ToString();
            this.gameObject.SetActive(false);
        }
    }
}
