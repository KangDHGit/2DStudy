using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody2D _rigid; // ��������� �տ� _�� ���̴°��� ����
    public AudioSource _birdFx;
    public float _jumpForce;
    public float _jumplimit;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 force = new Vector2(0, _jumpForce);
        if(Input.GetKey(KeyCode.Space))
        {
            _rigid.AddForce(force);
            _birdFx.Play();
        }
        Vector3 vel = _rigid.velocity;  // _rigid�� �ӵ��� �����ͼ�
        float limit = Mathf.Min(_jumplimit, vel.y);   // 5, ������ y�ӵ� �߿� ���� ���� ��ȯ(5�� �ѱ��� ����)
        _rigid.velocity = new Vector2(vel.x, limit);
    }
}
