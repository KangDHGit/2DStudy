using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCat
{
    public class Cat : MonoBehaviour
    {
        public float _speed = 100.0f;
        Rigidbody2D _rigid;
        // Start is called before the first frame update
        void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
        }
        private void FixedUpdate()
        {
            Move();
        }
        void Move()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            //Debug.Log("h: " + h);
            //Debug.Log(string.Format("v: {0}", v));
            float fixedDeltaTime = Time.fixedDeltaTime;

            _rigid.velocity = new Vector2(h, v) * _speed * fixedDeltaTime;
        }
    }
}
