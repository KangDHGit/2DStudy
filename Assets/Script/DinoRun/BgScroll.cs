using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    public float _speed;

    public float _leftPos;
    public float _rightPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime, 0, 0);

        // ���� x��ǥ�� ���� �������� �����
        if (transform.position.x < _leftPos)
        {
            // ������ ���������� ���� �̵���Ų��.
            Vector3 pos = transform.position;
            pos.x = _rightPos;
            transform.position = pos;
        }

    }
}
