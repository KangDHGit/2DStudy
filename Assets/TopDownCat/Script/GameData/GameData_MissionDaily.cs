using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
// Monobehavior 상속을 안해서 유니티에서 안보일때
public class GameData_MissionDaily
{
    public int id;
    public string name;
    public int clearcount;
    public int gem_reward;
    public string desc;
}
