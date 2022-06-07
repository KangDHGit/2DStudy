using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody2D _rigid; // 멤버변수는 앞에 _를 붙이는것이 관례
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
        Vector3 vel = _rigid.velocity;  // _rigid의 속도를 가져와서
        float limit = Mathf.Min(_jumplimit, vel.y);   // 5, 가져온 y속도 중에 작은 값을 반환(5를 넘기지 않음)
        _rigid.velocity = new Vector2(vel.x, limit);
    }
}
