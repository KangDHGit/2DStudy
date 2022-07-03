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
        BoxCollider2D _collider;
        Rigidbody2D _rigid;

        GameObject _placementUI; // 배치UI(배치, 취소)
        Button _placementBtn;
        Button _cancelBtn;

        bool _crashCheck;

        // Start is called before the first frame update
        void Start()
        {
            _dragActive = true;
            _sprite = GetComponent<SpriteRenderer>();
            _placementUI = _uiTrans.Find("PlacementUI").gameObject;
            _placementBtn = _placementUI.transform.Find("PlacementBtn").GetComponent<Button>();
            _cancelBtn = _placementUI.transform.Find("CancelBtn").GetComponent<Button>();

            if (_collider == null)
                _collider = gameObject.AddComponent<BoxCollider2D>();
            else
                _collider = gameObject.GetComponent<BoxCollider2D>();

            _rigid = gameObject.GetComponent<Rigidbody2D>();

            _collider.isTrigger = true;
            _rigid.bodyType = RigidbodyType2D.Dynamic;
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
                if (_crashCheck == false)
                    return;
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
                if (_crashCheck == false)
                    return;
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
            if (_crashCheck == false)
                return;

            _dragActive = false;
            _placementUI.SetActive(false);
            _sprite.color = new Color(1f, 1f, 1f, 1f);
            _collider.isTrigger = false;
            this.enabled = false;
            _rigid.bodyType = RigidbodyType2D.Static;
        }
        // 취소 눌렀을때
        public void PlacementNo()
        {
            Destroy(gameObject);
            _placementUI.SetActive(false);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isActiveAndEnabled)
                return;

            _crashCheck = false;
            Debug.Log("Crash Check " + _crashCheck +  " " + this.name);
            // 반투명 빨강색으로 만들기
            _sprite.color = new Color(1f, 0f, 0f, 0.5f);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!isActiveAndEnabled)
                return;

            _crashCheck = true;
            Debug.Log("Crash Check " + _crashCheck + " " + this.name);
            // 다시 반투명 색으로
            _sprite.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
}
