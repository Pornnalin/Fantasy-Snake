using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Unit monsterProflie;
    private GameMananger gameMananger;
    // Start is called before the first frame update
    void Start()
    {
        gameMananger = FindAnyObjectByType<GameMananger>();
        monsterProflie.attack = gameMananger.statInfo.minMonsterAttack;
        monsterProflie.health = gameMananger.statInfo.minMonsterHeart;
        Debug.Log("SetDefault_monster" + name);

    }

    // Update is called once per frame
    void Update()
    {
        if (monsterProflie.health <= 0)
        {
            this.gameObject.SetActive(false);
        }
       
    }
}
