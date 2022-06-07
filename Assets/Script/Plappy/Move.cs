using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float horizontal;
    float vertical;
    public GameManager _gameMgr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetKey();
        //transform.position += new Vector3(Horizontal, Vertical) * Time.deltaTime;

        // 유저입력 비활성화
        if (_gameMgr._isIntro == true)
            return; // 함수종료

        Vector3 pos = transform.position;
        //if (Input.GetKey(KeyCode.LeftArrow)) // 왼쪽 화살표키를 누르고 있으면중이면
        //{
        //    pos += (new Vector3(-2, 0) * Time.deltaTime);
        //    transform.position = pos;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    pos += (new Vector3(2, 0) * Time.deltaTime);
        //    transform.position = pos;
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    pos += (new Vector3(0, 2) * Time.deltaTime);
        //    transform.position = pos;       
        //}
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos += (new Vector3(0, -2) * Time.deltaTime);
            transform.position = pos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("충돌발생" +  collision.gameObject.name);
        // 게임오버 UI 표시
        _gameMgr.OnGameOver();

        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("트리거 이벤트 발생 : " + collision.gameObject.name);
        _gameMgr._score++;  // 1점씩 증가
    }

    void GetKey()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
}
