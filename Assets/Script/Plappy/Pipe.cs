using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    GameManager _gameMgr;
    // Start is called before the first frame update
    void Start()
    {
        _gameMgr = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ������Ʈ�ο� ���ӿ����� �ƴҶ�
        if(_gameMgr._isIntro == false && _gameMgr._isGameover == false)
        {
            transform.Translate(-0.01f, 0, 0);   // x�����θ� �����̵�
        }
    }
}
