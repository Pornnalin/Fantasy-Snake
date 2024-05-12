using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] float targetTime;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        targetTime = GameMananger.instance.statInfo.growing;
        //convert to seconds
        targetTime = targetTime * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.currentPlayerStage == PlayerManager.playerStage.MOVE)
        {
            if (time < targetTime)
            {
                time += Time.deltaTime;

                Debug.Log(Mathf.Round(time));
            }
            else
            {
                time = 0;
                UpdateState();
            }
        }
    }

    public void UpdateState()
    {
        int maxHealthP = GameMananger.instance.statInfo.maxPlayerHeart;
        int maxHealthM = GameMananger.instance.statInfo.maxMonsterHeart;
        int maxAttackP = GameMananger.instance.statInfo.maxPlayerAttack;
        int maxAttackM = GameMananger.instance.statInfo.maxMonsterrAttack;

        for (int i = 0; i < PlayerManager.instance.heroList.Count; i++)
        {
            PlayerController player = PlayerManager.instance.heroList[i].GetComponent<PlayerController>();
            if (player.playerProfile.health < maxHealthP)
            {
                player.playerProfile.health += 1;
            }
            if (player.playerProfile.attack < maxAttackP)
            {
                player.playerProfile.attack += 1;
            }
        }

        for (int i = 0; i < MonsterManager.instance.monsList.Count; i++)
        {
            if (MonsterManager.instance.monsList[i].gameObject.CompareTag("Monster"))
            {
                MonsterController monsterController = MonsterManager.instance.monsList[i].GetComponentInChildren<MonsterController>();
                if (monsterController.monsterProflie.health < maxHealthM)
                {
                    monsterController.monsterProflie.health += 1;
                }
                if (monsterController.monsterProflie.attack < maxAttackM)
                {
                    monsterController.monsterProflie.attack += 1;
                }

            }
        }
    }
}

