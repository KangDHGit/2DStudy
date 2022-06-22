using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace MyCat
{
    public class ButtonManager : MonoBehaviour
    {
        public List<GameObject> _skillObjList;
        public List<SkillData> _skillData;

        public Transform _worldTrans;

        public GameObject _eatButtonUI;
        public Button _eatButton;
        public Image _coolTimeImg;
        public float _eatCoolTime;

        GameObject _dishObj;
        GameObject _blackCat;
        // Start is called before the first frame update
        void Start()
        {
            _blackCat = _worldTrans.transform.Find("BlackCat").gameObject;
            _dishObj = _worldTrans.transform.Find("Cat_Dish").gameObject;
            //_skillObjList = GameObject.FindGameObjectsWithTag("Skill_Button").ToList<GameObject>();

            _eatButton = _eatButtonUI.GetComponent<Button>();
            _coolTimeImg = _eatButtonUI.transform.Find("CoolTimeImg").GetComponent<Image>();
            _coolTimeImg.enabled = false;
            _coolTimeImg.fillAmount = 1.0f;
        }

        // Update is called once per frame
        void Update()
        {
            PlayCoolTimeImg(_coolTimeImg);
        }

        public void OnClickEat()
        {
            Cat cat = _blackCat.GetComponent<Cat>();
            _dishObj.SetActive(true);
            cat.SetTarget(_dishObj);

            // 쿨타임 동안 버튼 비활성화
            _eatButton.enabled = false;
            Invoke("EatCoolTimeOver", _eatCoolTime);    // 쿨타임지난후에 버튼 활성화

            _coolTimeImg.enabled = true;
            _coolTimeImg.fillAmount = 1.0f;
        }

        void EatCoolTimeOver()
        {
            _eatButton.enabled = true;
        }

        // 쿨타임에 따라 CoolTimeImg의 filAmount를 조절하는 함수
        void PlayCoolTimeImg(Image coolTimeImg)
        {
            if (coolTimeImg == null || coolTimeImg.enabled == false)
                return;
            coolTimeImg.fillAmount -= (1.0f / _eatCoolTime) * Time.deltaTime;
            if(coolTimeImg.fillAmount <= 0)
                coolTimeImg.enabled = false;
        }

        void GetSkillData(List<GameObject> skillObjList)
        {
            foreach (var skillObj in skillObjList)
            {
                _skillData.Add(skillObj.GetComponent<SkillData>());
            }
        }
    }
}
