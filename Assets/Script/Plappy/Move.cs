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

        // �����Է� ��Ȱ��ȭ
        if (_gameMgr._isIntro == true)
            return; // �Լ�����

        Vector3 pos = transform.position;
        //if (Input.GetKey(KeyCode.LeftArrow)) // ���� ȭ��ǥŰ�� ������ ���������̸�
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
        //Debug.Log("�浹�߻�" +  collision.gameObject.name);
        // ���ӿ��� UI ǥ��
        _gameMgr.OnGameOver();

        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Ʈ���� �̺�Ʈ �߻� : " + collision.gameObject.name);
        _gameMgr._score++;  // 1���� ����
    }

    void GetKey()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
}
