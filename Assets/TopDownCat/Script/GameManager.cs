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
        
        // Start is called before the first frame update
        void Start()
        {
            _blackCat = _worldTrans.transform.Find("BlackCat").gameObject;
            _dishObj = _worldTrans.transform.Find("Item").Find("Cat_Dish").gameObject;
            _dishObj.SetActive(false);
            //_heartTemplate.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            DateTime dt = DateTime.Now;
            //_nowTimeTxt.text = dt.ToString("yyyy년MM월dd일 tt hh:mm:ss");
            _nowTimeTxt.text = string.Format("{0:yyyy년M월dd일 tt hh:mm:ss}", dt);
        }

        public void OnFinish_Eat()
        {
            _dishObj.SetActive(false);
        }

        public void DropItem(GameObject item)
        {
            GameObject itemobj = Instantiate(item);

            // 단위 원범위(1)
            float maxRadius = 1.5f;
            float minRadius = 1.2f;
            Vector2 circleRange = UnityEngine.Random.insideUnitCircle * maxRadius;
            Vector2 randomPos;

            if (circleRange.magnitude < minRadius)
                randomPos = circleRange.normalized * minRadius;
            else
                randomPos = circleRange;


            itemobj.transform.position = _blackCat.transform.position
                                           + new Vector3(randomPos.x, randomPos.y);
            itemobj.SetActive(true);
        }
    }
}
