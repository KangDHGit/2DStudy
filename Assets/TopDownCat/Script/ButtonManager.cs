using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace MyCat
{
    public class ButtonManager : MonoBehaviour
    {
        //public List<GameObject> _skillObjList;
        //public List<SkillData> _skillData;

        public Transform _worldTrans;
        public Transform _uiTrans;
        GameObject _dishObj;
        GameObject _blackCat;

        // 쿨타임 관련 변수
        Button _eatButton;
        Image _coolTimeImg;
        public float _eatCoolTime;
        //---------------------- 

        // 상점
        Button _shopButton;

        void Start()
        {
            _blackCat = _worldTrans.transform.Find("BlackCat").gameObject;
            _dishObj = _worldTrans.transform.Find("Item").Find("Cat_Dish").gameObject;

            _eatButton = _uiTrans.Find("EatButton").GetComponent<Button>();
            _coolTimeImg = _uiTrans.Find("EatButton").Find("CoolTimeImg").GetComponent<Image>();
            _coolTimeImg.enabled = false; // 쿨타임 이미지 비활성화
            _coolTimeImg.fillAmount = 1.0f; // 쿨타임 fillAmount 초기화

            _shopButton = _uiTrans.Find("ShopButton").GetComponent<Button>();
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

        // ShopUI에서 상품을 클릭히면 BuyUI 활성화
        public void OnClickShopItem()
        {
            _uiTrans.Find("BuyUI").gameObject.SetActive(true);
        }

        public void OnClickBuyNo()
        {
            _uiTrans.Find("BuyUI").gameObject.SetActive(false);
        }

        public void OnClickBuyYes()
        {
            // 1. 코인 텍스트 불러와서 int로 변환
            Text CoinTxt = _uiTrans.Find("Resource").Find("Coin").Find("Coin_Txt").GetComponent<Text>();
            int.TryParse(CoinTxt.text, out int Coin);

            // 2. 구매할 아이템 가격 불러오기

            // 3. 아이템 가격만큼 코인 감소(가격보다 적으면 구매 불가 4번실행 x)

            // 4. 구매 완료 후 창닫기
            _uiTrans.Find("BuyUI").gameObject.SetActive(false);
        }
    }
}
