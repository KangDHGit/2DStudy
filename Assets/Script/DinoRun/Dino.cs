using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    Rigidbody2D _rigid;
    public int _jumpForce;
    Animator _animator;
    public AudioSource _jumpSound;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _rigid.AddForce(new Vector3(0, _jumpForce, 0));
            _animator.SetTrigger("Jump");
            _jumpSound?.Play();
        }
    }

}
