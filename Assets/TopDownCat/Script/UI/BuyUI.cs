using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace MyCat
{
    public class BuyUI : MonoBehaviour
    {
        public Transform _InstantiatePath;
        Resource _resource;
        public Transform _uiTrans;
        ShopItem _buyItem; // 내가 살 아이템

        // Start is called before the first frame update
        void Start()
        {
            _resource = _uiTrans.Find("Resource").GetComponent<Resource>();
        }

        public void SetBuyItem(ShopItem buyitem)
        {
            _buyItem = buyitem;
        }
        public void OnClickBuyYes()
        {
            if (_buyItem == null)
                return;
            // 구매 아이템 가격(string)의 "," 제거 한 string을 int로 변환
            int.TryParse(_buyItem.PriceDeleteComma(), out int price);
            _resource.CalculateCoin(-price);

            _buyItem = null;

            this.gameObject.SetActive(false);
            _uiTrans.Find("ShopUI").gameObject.SetActive(false);

            GameObject item = new GameObject();
            item.transform.SetParent(_InstantiatePath);
            _InstantiatePath.gameObject.GetComponent<TilemapRenderer>();
        }
        public void OnClickBuyNo()
        {
            this.gameObject.SetActive(false);
        }
    }
}
