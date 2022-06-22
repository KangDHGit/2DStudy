using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillData : MonoBehaviour
{
    public Button _skillBtn;
    public Image _coolDownImg;
    public float _coolDown;
    public string _skillName;

    private void Start()
    {
        _skillBtn = GetComponent<Button>();
        _coolDownImg = GetComponent<Image>();
    }


}
