using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCat
{
    public class ItemPlacement : MonoBehaviour
    {
        public Transform _instantiatePath;
        SpriteRenderer _sprite;

        Vector2 _mousePos;

        
        // Start is called before the first frame update
        void Start()
        {
            transform.SetParent(_instantiatePath);
            _sprite = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void MousePos()
        {
            
        }
    }
}
