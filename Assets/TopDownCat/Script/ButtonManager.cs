using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace MyCat
{
    public class ButtonManager : MonoBehaviour
    {
        public Transform _worldTrans;
        public Transform _uiTrans;
        GameObject _dishObj;
        GameObject _blackCat;

        // 쿨타임 관련 변수
        Button _eatButton;
        Image _coolTimeImg;
        public float _eatCoolTime;
        //---------------------- 

        void Start()
        {
            _blackCat = _worldTrans.transform.Find("BlackCat").gameObject;
            _dishObj = _worldTrans.transform.Find("Item").Find("Cat_Dish").gameObject;

            _eatButton = _uiTrans.Find("EatButton").GetComponent<Button>();
            _coolTimeImg = _uiTrans.Find("EatButton").Find("CoolTimeImg").GetComponent<Image>();
            _coolTimeImg.enabled = false; // 쿨타임 이미지 비활성화
            _coolTimeImg.fillAmount = 1.0f; // 쿨타임 fillAmount 초기화
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

            // 쿨타임동안 버튼 비활성화
            _eatButton.enabled = false;
            Invoke("EatCoolTimeOver", _eatCoolTime);    // 쿨타임종료시 버튼 활성화
            // 쿨타임 이미지 활성화 및 fillAmount 초기화
            _coolTimeImg.enabled = true;
            _coolTimeImg.fillAmount = 1.0f;
        }
        void EatCoolTimeOver()
        {
            _eatButton.enabled = true;
        }

        // 업데이트 함수에서 쿨타임 이미지가 활성화 되면 쿨타임에 맞게 fillAmount조절
        void PlayCoolTimeImg(Image coolTimeImg)
        {
            if (coolTimeImg == null || coolTimeImg.enabled == false)
                return;
            coolTimeImg.fillAmount -= (1.0f / _eatCoolTime) * Time.deltaTime;
            if(coolTimeImg.fillAmount <= 0)
                coolTimeImg.enabled = false;
        }

        // ShopButton 누르면 ShopUI 활성화
        public void OnClickShopButton()
        {
            _uiTrans.Find("ShopUI").gameObject.SetActive(true);
        }

        // ShopUI의 CloseBtn누르면 ShopUI 비활성화
        public void OnClickShopClose()
        {
            _uiTrans.Find("ShopUI").gameObject.SetActive(false);
        }
    }
}
