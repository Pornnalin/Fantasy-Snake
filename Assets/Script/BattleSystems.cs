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
  

    public bool isPlayerContinue = false;
    private Spawn spawn;
    private bool canShake = false;
    // Start is called before the first frame update
    void Start()
    {
        state = battleStage.NONE;
        //  playerManager = FindAnyObjectByType<PlayerManager>();
        displayUI = FindAnyObjectByType<DisplayUI>();
        spawn = FindAnyObjectByType<Spawn>();
    }

    // Update is called once per frame

    private void Update()
    {

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
                StartCoroutine(SpawnNewMonster());

                break;
            case battleStage.LOST:
                if (PlayerManager.instance.heroList.Count == 1)
                {
                    PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.GAMEOVER;
                }
                break;
        }

    }
    private void RandomNumber()
    {
        if (!isRand)
        {
            isRand = true;
            int _rand = Random.Range(1, 3);
            rand = _rand;
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
        displayUI.result.SetActive(false);
        displayUI.uiOddEven.SetActive(true);

        isRand = false;

        Debug.Log("oddOrEvenHead_Close");

        state = playerControl.isMyTurn ? battleStage.PLAYERTURN : battleStage.MONSTERTURN;
        displayUI.playerBattleUI.SetActive(true);
        displayUI.monsBattleUI.SetActive(true);
        rand = -1;
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

        displayUI.monsterHealthText.text = monsterControl.monsterProflie.health.ToString();
        displayUI.monsterAttackText.text = monsterControl.monsterProflie.attack.ToString();
    }
    public IEnumerator PlayerTurnAttack()
    {
        yield return new WaitForSeconds(1f);
        if (monsterControl.monsterProflie.health > 0)
        {
            monsterControl.monsterProflie.health -= playerControl.playerProfile.attack;
            Debug.Log("Player_Hit_Monster!!");
            monsterControl.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.5f);
            monsterControl.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            //  StartCoroutine(Shake(monsterControl.transform.position));
            yield return new WaitForSeconds(.5f);

            if (monsterControl.monsterProflie.health > 0)
            {
                state = battleStage.MONSTERTURN;
            }
            else
            {
               monsterControl.gameObject.SetActive(false);
                MonsterManager.instance.monsPosition.RemoveAll(x => x.Equals(monsterControl.transform.position));

                MonsterManager.instance.monsList.RemoveAll(x => x.name == monsterControl.transform.root.name);

                //remove monster
                Destroy(monsterControl.transform.root.gameObject);
                //Debug.Log("remove " + monsterControl.transform.root.name);
                state = battleStage.WON;
                PlayerManager.instance.amountKilled += 1;
              
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
        yield return new WaitForSeconds(2f);
        if (playerControl.playerProfile.health > 0)
        {
            playerControl.playerProfile.health -= monsterControl.monsterProflie.attack;
            Debug.Log("Monster_Hit_ME!!");
            playerControl.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.5f);
            playerControl.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.5f);

            if (playerControl.playerProfile.health > 0)
            {
                state = battleStage.PLAYERTURN;
            }
            else
            {
                state = battleStage.LOST;             

                if (PlayerManager.instance.heroList.Count > 1)
                {
                    //remove first and move the line 
                    StartCoroutine(WaitContinue());

                }
                else
                {
                    isPlayerContinue = false;
                }

            }

            isMonsAttack = false;
        }
        else
        {

            state = battleStage.LOST;
            isMonsAttack = false;


        }
    }
    IEnumerator WaitContinue()
    {
        isPlayerContinue = true;

        //PlayerManager.instance.heroNotInTeam.RemoveAll(x => x.name == playerControl.name);

        if (isPlayerContinue)
        {
            PlayerManager.instance.RemovePlayerDie();
            state = battleStage.PLAYERTURN;
        }
        yield return new WaitForSeconds(2f);
        isPlayerContinue = false;

    }
    IEnumerator SpawnNewMonster()
    {
        displayUI.playerBattleUI.SetActive(false);
        displayUI.monsBattleUI.SetActive(false);

        if (!spawn.isSpwan)
        {
            spawn.isSpwan = true;
            //spawn monster or trap
            spawn.SpwanNewCharacterOrObject(GameMananger.instance.ChanceSpawnMonster(), spawn.monsterPrefab, false);
            playerControl.isSetNumber = false;

        }
        yield return new WaitForSeconds(0.2f);
        spawn.isSpwan = false;
        state = battleStage.NONE;
        PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.MOVE;
    }
}

   