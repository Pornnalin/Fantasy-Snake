using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystems : MonoBehaviour
{
    public enum battleStage { NONE, RANDOM, PLAYERTURN, MONSTERTURN, WON, LOST }
    public battleStage state;
    public bool isEven;
    public int rand;
    public bool isRand;
    public string descriptionLucky;
    public string descriptionBad;
    private bool isMonsAttack = false;
    private DisplayUI displayUI;
    public MonsterController monsterControl;
    public PlayerController playerControl;
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        state = battleStage.NONE;
        playerManager = FindAnyObjectByType<PlayerManager>();
        displayUI = FindAnyObjectByType<DisplayUI>();

    }

    // Update is called once per frame
    public void RandomNumber()
    {
        if (!isRand)
        {
            isRand = true;
            int _rand = Random.Range(1, 3);
            rand = _rand;
        }
    }
    private void Update()
    {
        //if (isRand)
        //{
        //    if (rand % 2 == 0)
        //    {
        //        Debug.Log(rand + "true");
        //        isEven = true;
        //    }
        //    else
        //    {
        //        Debug.Log(rand + "fasle");
        //        isEven = false;
        //    }
        //}
        switch (state)
        {
            case battleStage.NONE:
                break;
            case battleStage.RANDOM:
                break;
            case battleStage.PLAYERTURN:
                UpdatePlayerText();
                displayUI.playerArrowGroup.SetActive(true);
                displayUI.monsArrowGroup.SetActive(false);
                break;
            case battleStage.MONSTERTURN:
                UpdatePlayerText();
                displayUI.playerArrowGroup.SetActive(false);
                displayUI.monsArrowGroup.SetActive(true);
                if (!isMonsAttack)
                {
                    isMonsAttack = true;
                    StartCoroutine(MonsterTurnAttack());
                }
                break;
            case battleStage.WON:
                break;
            case battleStage.LOST:
                break;
        }
    }
    public IEnumerator FuncDisplayUIAndChangeState()
    {
        //  displayUI.oddOrEvenHead.SetActive(true);
        RandomNumber();
        displayUI.numberText.text = rand.ToString();
        displayUI.result.SetActive(true);
        displayUI.uiOddEven.SetActive(false);


        if (playerControl.setNumber.Equals(rand))
        {
            ModiflyTextAndSetBool(descriptionLucky, true);
         //   Debug.Log("Lucky" + playerControl.setNumber + isEven);

        }
        else
        {
           // Debug.Log("bad" + playerControl.setNumber + isEven);
            ModiflyTextAndSetBool(descriptionBad, false);

        }
      
      
        yield return new WaitForSeconds(2.5f);
        displayUI.oddOrEvenHead.SetActive(false);
        isRand = false;
        Debug.Log("oddOrEvenHead_Close");
        state = playerControl.isMyTurn ? battleStage.PLAYERTURN : battleStage.MONSTERTURN;
        displayUI.playerBattleUI.SetActive(true);
        displayUI.monsBattleUI.SetActive(true);

        Debug.Log("CurrentState" + state);

    }

    private void ModiflyTextAndSetBool(string str, bool _isMyTurn)
    {
        displayUI.tellPlayer.text = str;
        playerControl.isMyTurn = _isMyTurn;
    }

    public void UpdatePlayerText()
    {
        displayUI.playerHealthText.text = playerControl.playerProfile.health.ToString();
        displayUI.playerAttackText.text = playerControl.playerProfile.attack.ToString();

        displayUI.monsterAttackText.text = monsterControl.monsterProflie.health.ToString();
        displayUI.monsterHealthText.text = monsterControl.monsterProflie.attack.ToString();
    }
    public IEnumerator PlayerTurnAttack()
    {
        yield return new WaitForSeconds(1f);
        if (monsterControl.monsterProflie.health > 0)
        {
            monsterControl.monsterProflie.health -= playerControl.playerProfile.attack;

            yield return new WaitForSeconds(1f);

            if (monsterControl.monsterProflie.health > 0)
            {
                state = battleStage.MONSTERTURN;
            }
            else
            {
                state = battleStage.WON;
            }
            playerControl.isEndAttack = false;
        }
        else
        {
            state = battleStage.WON;
            playerControl.isEndAttack = false;

        }
    }

    IEnumerator MonsterTurnAttack()
    {
        yield return new WaitForSeconds(5f);
        if (playerControl.playerProfile.health > 0)
        {
            playerControl.playerProfile.health -= monsterControl.monsterProflie.attack;
            Debug.Log("Monster_Hit_ME!!");

            yield return new WaitForSeconds(1f);

            if (playerControl.playerProfile.health > 0)
            {
                state = battleStage.PLAYERTURN;
            }
            else
            {
                state = battleStage.LOST;

            }
            isMonsAttack = false;
        }
        else
        {
            state = battleStage.LOST;
            isMonsAttack = false;

        }
    }
}