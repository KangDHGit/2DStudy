using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class ShopItem : MonoBehaviour
    {
        public Transform _uiTrans;
        BuyUI _buyUI;

        Image _item_Icon;
        public Text _item_Name;
        public Text _price_Txt;
        // Start is called before the first frame update
        void Start()
        {
            _buyUI = _uiTrans.Find("BuyUI").GetComponent<BuyUI>();
            _item_Icon = transform.Find("Item_Icon").GetComponent<Image>();
            _item_Name = transform.Find("Item_Name").GetComponent<Text>();
            _price_Txt = transform.Find("Price_Txt").GetComponent<Text>();
        }

        public void SetData(GameData_ShopItem data)
        {
            _item_Name.text = data._name;
            _price_Txt.text = data._price.ToString("#,0");
            if (data._sprite == "")
                return;
            _item_Icon.sprite = Resources.Load<Sprite>(data._sprite);
        }

        public void SetPriceTxt_BuyUI()
        {
            _buyUI._itemPrice_Txt = _price_Txt;
        }
    }
}
