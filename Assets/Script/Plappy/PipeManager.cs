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
        // 1�ʸ��� MakePipSet �Լ� ȣ��
        Invoke("MakePipeSet", 1.0f);
    }

    void MakePipeSet()
    {
        // ���ӿ�����Ʈ ����
        GameObject cloneObj = Instantiate(_pipeSetTemplate);

        float yPos = Random.Range(-1.5f, 1.5f);
        cloneObj.transform.position = new Vector3(0, yPos, 0);
        cloneObj.SetActive(true);

        // 1�ʸ��� MakePipSet �Լ� ���ȣ��(�������ϰ�쿡��)
        if (_gameMgr._isGameover == false)
            Invoke("MakePipeSet", _delay);
        
        // ������ 2�ʵ� �ı�
        Destroy(cloneObj, 2.0f);
    }
}
