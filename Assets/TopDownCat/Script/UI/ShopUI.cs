using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameData _gameData;
    public List<ShopItem> _itemList;
    void Start()
    {
        ShopItem[] array = GetComponentsInChildren<ShopItem>();
        _itemList.AddRange(array);
        //_itemList.AddRange(GetComponentsInChildren<ShopItem>()); // ������ �ڽĿ�����Ʈ�� ������Ʈ

        List<GameData_ShopItem> shopItemList = _gameData._shopItem_data; // ������� ������
        for (int i = 0; i < _itemList.Count; i++)
        {
            _itemList[i].SetData(shopItemList[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
