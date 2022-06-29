using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCat
{
    public class ItemPlacement : MonoBehaviour
    {

        bool _dragActive;
        
        // Start is called before the first frame update
        void Start()
        {
            _dragActive = true;
        }

        // Update is called once per frame
        void Update()
        {
            DragItem(_dragActive);
        }

        void DragItem(bool value)
        {
            if (value == false)
                return;
            Vector2 mousePos = Input.mousePosition;
            Vector2 dragPos = new Vector2(mousePos.x / 320, mousePos.y / 180);
            transform.position = dragPos;
        }
    }
}
