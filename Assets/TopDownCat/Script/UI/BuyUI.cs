using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace MyCat
{
    public class BuyUI : MonoBehaviour
    {
        Resource _resource;
        public Transform _uiTrans;
        public Transform _GameMgr;

        public Text _itemNameTxt;
        public Text _itemPriceTxt;

        ShopItem _buyItem; // 내가 살 아이템

        // 가구배치 관련
        Vector2 _mousePos;

        // Start is called before the first frame update
        private void Start()
        {
            Init();
        }
        void Init()
        {
            _resource = _uiTrans.Find("Resource").GetComponent<Resource>();
            _itemNameTxt = transform.Find("BuyScreen").Find("ItemInfo").Find("ItemName_Txt").GetComponent<Text>();
            _itemPriceTxt = transform.Find("BuyScreen").Find("ItemInfo").Find("Price_Txt").GetComponent<Text>();
        }
        private void Update()
        {
            _mousePos = Input.mousePosition;
        }

        public void SetBuyItem(ShopItem buyitem)
        {
            Init();

            _buyItem = buyitem;
            _itemNameTxt.text = _buyItem._Item_Name.text;
            _itemPriceTxt.text = _buyItem._Price_Txt.text;
        }
        public void OnClickBuyYes()
        {
            if (_buyItem == null)
                return;
            // 구매 아이템 가격(string)의 "," 제거 한 string을 int로 변환
            int.TryParse(_buyItem.PriceDeleteComma(), out int price);

            if (!_resource.CalculateCoin(-price))
                return;

            this.gameObject.SetActive(false);
            _uiTrans.Find("ShopUI").gameObject.SetActive(false);

            _GameMgr.GetComponent<GameManager>().ItemPlacement(_buyItem);
            _buyItem = null;
        }
        public void OnClickBuyNo()
        {
            this.gameObject.SetActive(false);
        }
    }
}
