using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCat
{
    public class DailyMissionUI : MonoBehaviour
    {
        public GameData _gameData;
        public List<DailyMissionItem> _itemList;    // DailyMissionUI의 자식오브젝트들의 DailyMissionitem 컴포넌트 리스트
        // Start is called before the first frame update
        void Start()
        {
            DailyMissionItem[] array = GetComponentsInChildren<DailyMissionItem>(); // 자식오브젝트들의 DailyMissionitem 컴포넌트 한번에 가져오기
            // List에 여러개(배열)을 집어넣을 때는 AddRange 함수 쓴다
            _itemList.AddRange(array);

            List<GameData_MissionDaily> missionDataList = _gameData._mission_daily_data;
            for (int i = 0; i < _itemList.Count; i++)
            {
                //Debug.Log("아이템 : " + i);
                GameData_MissionDaily data = missionDataList[i];
                DailyMissionItem item = _itemList[i];
                item.SetData(data); // 일일미션데이터를 각항목에 넣어준다

                //_itemList[i].SetData(missionDataList[i]);
            }


        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
