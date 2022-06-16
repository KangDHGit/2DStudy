using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat
{
    public class GameManager : MonoBehaviour
    {
        public Text _nowTimeTxt;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            DateTime dt = DateTime.Now;
            //_nowTimeTxt.text = dt.ToString("yyyy년MM월dd일 tt hh:mm:ss");
            _nowTimeTxt.text = string.Format("{0:yyyy년M월dd일 tt hh:mm:ss}", dt);
        }
    }
}
