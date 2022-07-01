using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class ItemPlacement : MonoBehaviour
    {   // 배치 아이템에 들어가는 스크립트
        bool _dragActive;
        public RectTransform _uiTrans;
        SpriteRenderer _sprite;

        GameObject _placementUI; // 배치UI(배치, 취소)
        Button _placementBtn;
        Button _cancelBtn;

        // Start is called before the first frame update
        void Start()
        {
            _dragActive = true;
            _sprite = GetComponent<SpriteRenderer>();
            _placementUI = _uiTrans.Find("PlacementUI").gameObject;
            _placementBtn = _placementUI.transform.Find("PlacementBtn").GetComponent<Button>();
            _cancelBtn = _placementUI.transform.Find("CancelBtn").GetComponent<Button>();

            //if(gameObject.GetComponent<BoxCollider2D>() != null)
            //    gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            DragItem(_dragActive);
        }

        void DragItem(bool value)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _dragActive = true;
                _placementUI.SetActive(false);
                _sprite.color = new Color(1f, 1f, 1f, 0.5f);
            }

            if (value == false)
                return;

            Vector2 mousePos = Input.mousePosition;
            Vector2 dragPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = dragPos;
            _placementUI.transform.position = mousePos;

            if (Input.GetMouseButtonDown(0))
            {
                _dragActive = false;
                _placementUI.SetActive(true);
                _placementBtn.onClick.AddListener(PlacementYes);
                _cancelBtn.onClick.AddListener(PlacementNo);
                _sprite.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        // 배치 눌렀을때
        public void PlacementYes()
        {
            _dragActive = false;
            _placementUI.SetActive(false);
            _sprite.color = new Color(1f, 1f, 1f, 1f);
            if (gameObject.GetComponent<BoxCollider2D>() == null)
                gameObject.AddComponent<BoxCollider2D>();
            else
                gameObject.GetComponent<BoxCollider2D>().enabled = true;


            this.enabled = false;
        }
        // 취소 눌렀을때
        public void PlacementNo()
        {
            Destroy(gameObject);
            _placementUI.SetActive(false);
        }
    }
}
