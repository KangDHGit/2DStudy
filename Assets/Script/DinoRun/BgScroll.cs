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

        // 현재 x좌표가 왼쪽 기준점을 벗어나면
        if (transform.position.x < _leftPos)
        {
            // 오른쪽 기준점으로 강제 이동시킨다.
            Vector3 pos = transform.position;
            pos.x = _rightPos;
            transform.position = pos;
        }

    }
}
