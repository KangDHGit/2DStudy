using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // 파일 다루는 C#라이브러리

public class GameData : MonoBehaviour
{
    public TextAsset _mission_daily_csv; // csv파일 
    public List<GameData_MissionDaily> _mission_daily_data;
    // csv파일의 데이터를 저장

    // Start is called before the first frame update
    void Start()
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

                    Debug.Assert(record.Length == 5); // 5개 인지 확인

                    GameData_MissionDaily temp = new GameData_MissionDaily();
                    temp.id = int.Parse(record[0]);
                    temp.name = record[1];
                    temp.clearcount = int.Parse(record[2]);
                    temp.gem_reward = int.Parse(record[3]);
                    temp.desc = record[4];

                    _mission_daily_data.Add(temp);
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
