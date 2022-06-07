using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject _pipeSetTemplate;
    float _delay = 1.0f;
    GameManager _gameMgr;
    private void Start()
    {
        _pipeSetTemplate.SetActive(false);
        _gameMgr = GameObject.FindObjectOfType<GameManager>();
    }
    public void Start_MakePipeSet()
    {
        // 1초마다 MakePipSet 함수 호출
        Invoke("MakePipeSet", 1.0f);
    }

    void MakePipeSet()
    {
        // 게임오브젝트 복제
        GameObject cloneObj = Instantiate(_pipeSetTemplate);

        float yPos = Random.Range(-1.5f, 1.5f);
        cloneObj.transform.position = new Vector3(0, yPos, 0);
        cloneObj.SetActive(true);

        // 1초마다 MakePipSet 함수 재귀호출(게임중일경우에만)
        if (_gameMgr._isGameover == false)
            Invoke("MakePipeSet", _delay);
        
        // 생성후 2초뒤 파괴
        Destroy(cloneObj, 2.0f);
    }
}
