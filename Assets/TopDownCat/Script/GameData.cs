using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO; // 파일 다루는 C#라이브러리

public class GameData : MonoBehaviour
{
    public TextAsset _mission_daily_csv; // 일일미션 csv파일 
    public List<GameData_MissionDaily> _mission_daily_data; // 일일미션 csv파일의 데이터를 저장

    public TextAsset _shop_item_csv;
    public List<GameData_ShopItem> _shopItem_data;

    public SpriteRenderer _testSprite;
    // Start is called before the first frame update
    void Start()
    {
        Sprite[] spList = Resources.LoadAll<Sprite>("spritesheet_16x16");
        //_testSprite.sprite = Resources.Load<Sprite>("Water");
        for (int i = 0; i < spList.Length; i++)
        {
            if(spList[i].name == "gem_blue")
            {
                _testSprite.sprite = spList[i];
                break;
            }
        }
        ReadMissionDaily_csv();
        ReadShopItem_csv();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadMissionDaily_csv()
    {
        //Debug.Log(_mission_daily_csv);
        string text = _mission_daily_csv.text; // csv파일을 string으로 변환
        _mission_daily_data = new List<GameData_MissionDaily>();
        // StringReader는 System.IO가 제공
        using (StringReader reader = new StringReader(text))
        {
            string line = reader.ReadLine(); // 컬럼 이름인 첫번째줄은 읽고 쓰지않는다
            if (line != null)
            {
                while ((line = reader.ReadLine()) != null)
                {
                    //Debug.Log("데이터 : " + line);
                    string[] record = line.Split(',');

                    Debug.Assert(record.Length == 6); // 5개 인지 확인

                    GameData_MissionDaily temp = new GameData_MissionDaily();
                    temp.id = int.Parse(record[0]);
                    temp.name = record[1];
                    temp.clearcount = int.Parse(record[2]);
                    temp.gem_reward = int.Parse(record[3]);
                    temp.reward_icon = record[4];
                    temp.desc = record[5];

                    _mission_daily_data.Add(temp);
                    // 스프라이트 찾아두기
                    Sprite[] spList = Resources.LoadAll<Sprite>("spritesheet_16x16");
                    for (int i = 0; i < spList.Length; i++)
                    {
                        if (spList[i].name == temp.reward_icon)
                        {
                            temp.reward_sprite = spList[i];
                            break;
                        }
                    }
                }
            }
        }
    }

    void ReadShopItem_csv()
    {
        string text = _shop_item_csv.text;
        _shopItem_data = new List<GameData_ShopItem>();
        using(StringReader reader = new StringReader(text))
        {
            string line = reader.ReadLine();
            if(line != null)
            {
                while((line = reader.ReadLine()) != null)
                {
                    //Debug.Log("데이터 : " + line);
                    string[] record = line.Split(',');

                    Debug.Assert(record.Length == 4);

                    GameData_ShopItem temp = new GameData_ShopItem();
                    temp._id = int.Parse(record[0]);
                    temp._name = record[1];
                    temp._price = int.Parse(record[2]);
                    temp._sprite = record[3];

                    _shopItem_data.Add(temp);
                }
            }
        }
    }
}
