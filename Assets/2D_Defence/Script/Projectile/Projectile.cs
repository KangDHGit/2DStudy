using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{   // �߻�ü, ���ư��� ���� ��ü�� �����ϴ� �뵵
    Rigidbody2D _rigid;
    public float _flyForceX = 500.0f;
    //public float _flyForceY = 50.0f;
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(_flyForceX, 0);
        _rigid.AddForce(force);
        
    }

    void Update()
    {
        // ��ӵ��
        
    }
}
