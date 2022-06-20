using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class DailyMissionItem : MonoBehaviour
    {
        public Text _nameTxt;
        public Text _cleatCountTxt;
        public Text _rewardTxt;
        public Image _rewardIcon; // 아직은 안씀
        int currentCount = 0;

        public void Start()
        {
            _nameTxt = transform.Find("QuestName").GetComponent<Text>();
            _cleatCountTxt = transform.Find("ClearCount").GetComponent<Text>();
            _rewardTxt = transform.Find("RewardValue").GetComponent<Text>();
            _rewardIcon = transform.Find("RewardIcon").GetComponent<Image>();
        }
        public void SetData(GameData_MissionDaily data)
        {
            // 데이터를 받아서 실제 그 데이터로 각종 표시를 한다
            // 미션이름
            _nameTxt.text = data.name;
            // 진행횟수와 완료를 위한 총 횟수
            int totalCount = data.clearcount;
            _cleatCountTxt.text = string.Format($"{currentCount} / {totalCount}");
            // 보상
            _rewardTxt.text = data.gem_reward.ToString();
        }
    }
}