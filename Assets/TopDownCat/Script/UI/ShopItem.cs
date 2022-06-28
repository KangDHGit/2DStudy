using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class ShopItem : MonoBehaviour
    {
        public Transform _uiTrans;
        public BuyUI _buyUI;

        Image _item_Icon;
        Text _item_Name;
        Text _price_Txt;

        public Image _Item_Icon { get { return _item_Icon; } }
        public Text _Item_Name { get { return _item_Name; } }


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
        // ShopUI에서 상품을 클릭히면 BuyUI 활성화
        public void OnClickShopItem()
        {
            _uiTrans.Find("BuyUI").gameObject.SetActive(true);
            _buyUI.SetBuyItem(this);
        }
        // 아이템 가격(string) "," 제거 후 string 반환
        public string PriceDeleteComma()
        {
            return _price_Txt.text.Replace(",", "");
        }
    }
}
