using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Unit monsterProflie;
    void Start()
    {
        monsterProflie.attack = GameMananger.instance.statInfo.minMonsterAttack;
        monsterProflie.health = GameMananger.instance.statInfo.minMonsterHeart;

        Debug.Log("<color=white>SetDefault_monster" + name + "</color>");

    }

    // Update is called once per frame
    void Update()
    {
     
       
    }
}
