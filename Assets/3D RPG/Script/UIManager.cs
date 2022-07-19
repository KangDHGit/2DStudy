using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MyRPG
{
    public class UIManager : MonoBehaviour
    {
        bool _cKey;
        public GameObject _uI_Stat;

        bool _escKey;
        public GameObject _uI_Pased;
        
        // Start is called before the first frame update
        void Start()
        {
            _uI_Stat = transform.Find("UI_Stat").gameObject;
            _uI_Pased = transform.Find("UI_Pased").gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            UI_StatSetActive();
            UI_PasedSetActive();
        }

        void UI_StatSetActive()
        {
            _cKey = Input.GetKeyDown(KeyCode.C);
            if(_cKey == true)
            {
                if (_uI_Stat.activeSelf == true)
                    _uI_Stat.SetActive(false);
                else if (_uI_Stat.activeSelf == false)
                    _uI_Stat.SetActive(true);
            }
        }
        
        void UI_PasedSetActive()
        {
            _escKey = Input.GetKeyDown(KeyCode.Escape);
            if (_escKey == true)
            {
                if (_uI_Pased.activeSelf == true)
                    _uI_Pased.SetActive(false);
                else if (_uI_Pased.activeSelf == false)
                    _uI_Pased.SetActive(true);
            }
        }

        public void OnButtonStatPoints(GameObject buttonobj)
        {
            Debug.Log("-------Stat--------");
            Debug.Log(buttonobj.transform.parent.name);
            Debug.Log(buttonobj.name);
            Debug.Log("-------------------");
        }
    }
}
