using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCat
{
    public class DropItem : MonoBehaviour
    {
        public Rigidbody2D _rigid;
        public CircleCollider2D _collider;
        // Start is called before the first frame update
        void Start()
        {
            _collider = GetComponent<CircleCollider2D>();
            _rigid = gameObject.GetComponent<Rigidbody2D>();
            _rigid.bodyType = RigidbodyType2D.Dynamic;
            _rigid.AddForce(new Vector2(0, 100.0f));
            _collider.enabled = false;
            //_rigid.simulated = true;

            Invoke("StopPhysics", 0.5f);
        }

        void StopPhysics()
        {
            _collider.enabled = true;
            //_rigid.simulated = false;
            _rigid.bodyType = RigidbodyType2D.Static;
        }

        void Update()
        {
            
        }
    }
}
