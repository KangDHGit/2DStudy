using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class GameManager : MonoBehaviour
    {
        public ButtonManager _buttonMgr;

        public Text _nowTimeTxt;
        public Transform _worldTrans;
        GameObject _dishObj;
        GameObject _blackCat;

        public GameObject _heartTemplate;
        public Canvas _canvasObj;
        int _coinCount = 0;
        Text _coinTxt;
        int _heartCount = 0;
        Text _heartTxt;
        // Start is called before the first frame update
        void Start()
        {
            _blackCat = _worldTrans.transform.Find("BlackCat").gameObject;
            _dishObj = _worldTrans.transform.Find("Cat_Dish").gameObject;
            _dishObj.SetActive(false);
            _heartTemplate.SetActive(false);

            _coinTxt = _canvasObj.transform.Find("Resource").Find("Coin").Find("Coin_Txt").GetComponent<Text>();
            _coinTxt.text = 0.ToString("D4");
            _heartTxt = _canvasObj.transform.Find("Resource").Find("Heart").Find("Heart_Txt").GetComponent<Text>();
            _heartTxt.text = 0.ToString("D4");
        }

        // Update is called once per frame
        void Update()
        {
            DateTime dt = DateTime.Now;
            //_nowTimeTxt.text = dt.ToString("yyyy년MM월dd일 tt hh:mm:ss");
            _nowTimeTxt.text = string.Format("{0:yyyy년M월dd일 tt hh:mm:ss}", dt);
        }

        public void OnClick_Food()
        {
            Cat cat = _blackCat.GetComponent<Cat>();
            _dishObj.SetActive(true);
            cat.SetTarget(_dishObj);
            _buttonMgr.EatButtonOn();
        }
        public void OnFinish_Eat()
        {
            _dishObj.SetActive(false);
        }

        public void AddCoin(int count)
        {
            _coinCount += count;
            //_coinTxt.text = _coinCount.ToString("D4");
            _coinTxt.text = string.Format($"{_coinCount:D4}");
        }

        public void AddHeart(int count)
        {
            _heartCount += count;
            _heartTxt.text = _heartCount.ToString("D4");
        }

        public void DropItem(GameObject item)
        {
            GameObject itemtemp = Instantiate(item);
            itemtemp.transform.position = _dishObj.transform.position;
            itemtemp.SetActive(true);
        }
    }
}
