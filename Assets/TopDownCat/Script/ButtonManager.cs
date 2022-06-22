using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class ButtonManager : MonoBehaviour
    {
        public GameObject _eatButtonUI;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void EatButtonOn()
        {
            Button eatButton = _eatButtonUI.GetComponent<Button>();
            eatButton.enabled = false;
            Image coolDownBg = _eatButtonUI.transform.Find("CoolDownBg").GetComponent<Image>();
            coolDownBg.enabled = true;
        }
    }
}
