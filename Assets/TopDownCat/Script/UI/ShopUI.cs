using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCat
{
    public class ShopUI : MonoBehaviour
    {
        public GameData _gameData;
        public List<ShopItem> _itemList; // 상점 아이템들의 컴포넌트 리스트
        void Start()
        {
            //ShopItem[] array = GetComponentsInChildren<ShopItem>();
            //_itemList.AddRange(array);
            _itemList.AddRange(GetComponentsInChildren<ShopItem>());

            List<GameData_ShopItem> shopItemList = _gameData._shopItem_data; // csv파일을 옮긴데이터
            for (int i = 0; i < _itemList.Count; i++)
            {
                _itemList[i].SetData(shopItemList[i]);
            }
        }
    }
}
