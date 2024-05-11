﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Unit playerProfile;
 //   private GameMananger gameMananger;
  //  private PlayerManager playerManager;
    private BattleSystems battleSystems;
    public int setNumber = 0;
    public bool isSetNumber = false;
    public bool isMyTurn;
    public bool isEndAttack;
    private DisplayUI displayUI;
    private PlayerUI playerUI;
    // Start is called before the first frame update
    void Start()
    {
        //gameMananger = FindAnyObjectByType<GameMananger>();
     //   playerManager = FindAnyObjectByType<PlayerManager>();
        battleSystems = FindAnyObjectByType<BattleSystems>();
        displayUI = FindAnyObjectByType<DisplayUI>();
        playerUI = FindAnyObjectByType<PlayerUI>();

        playerProfile.attack = GameMananger.instance.statInfo.minPlayerAttack;
        playerProfile.health = GameMananger.instance.statInfo.minPlayerHeart;

        
        //Debug.Log("SetDefault_Player" + name);
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerManager.instance.currentPlayerStage)
        {
            case PlayerManager.playerStage.MOVE:
                SwitchFirstToSecond();
                SwitchLastToFirst();
                break;
        }
      

        switch (battleSystems.state)
        {
            case BattleSystems.battleStage.NONE:
                break;
            case BattleSystems.battleStage.RANDOM:

                if (!isSetNumber && displayUI.oddOrEvenHead.activeSelf)
                {
                    InputRandom();
                }
                break;
            case BattleSystems.battleStage.PLAYERTURN:
                PlayerManager.instance.ManageStatUI();
                InputAttack();
                break;
            case BattleSystems.battleStage.MONSTERTURN:
                PlayerManager.instance.ManageStatUI();

                break;
            case BattleSystems.battleStage.WON:
                break;
            case BattleSystems.battleStage.LOST:
                break;
        }
    }
    #region InputRandom
    private void InputRandom()
    {
        if (GameMananger.instance.isGamePadDetect)
        {
            if (Input.GetKeyDown(KeyCode.J) || Gamepad.current.leftTrigger.wasPressedThisFrame)
            {
                setNumber = 2;
                isSetNumber = true;
                StartCoroutine(battleSystems.FuncDisplayUIAndChangeState());

            }
            else if (Input.GetKeyDown(KeyCode.K) || Gamepad.current.rightTrigger.wasPressedThisFrame)
            {
                setNumber = 1;
                isSetNumber = true;
                StartCoroutine(battleSystems.FuncDisplayUIAndChangeState());


            }
            else if (!Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.J) && Gamepad.current.rightTrigger.wasPressedThisFrame && Gamepad.current.leftTrigger.wasPressedThisFrame && Input.anyKey)
            {
                Debug.Log("not right input!!!");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                setNumber = 2;
                isSetNumber = true;
                StartCoroutine(battleSystems.FuncDisplayUIAndChangeState());

            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                setNumber = 1;
                isSetNumber = true;
                StartCoroutine(battleSystems.FuncDisplayUIAndChangeState());
            }
            else if (!Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.J) && Input.anyKey)
            {
                Debug.Log("not right input!!!");
            }
        }
      //  Debug.Log(setNumber);
    }
    #endregion

    #region SwitchFirstToSecond
    public void SwitchFirstToSecond()
    {
        //       Pressing Q on the keyboard or the left shoulder button on the gamepad will
        //rotate the second character in line to be the front character and the front
        //character to be the last.
        if (GameMananger.instance.isGamePadDetect && GameMananger.instance.canPress)
        {
            if ((Input.GetKeyDown(KeyCode.Q) || Gamepad.current.leftShoulder.isPressed) && !GameMananger.instance.isSwtichF_To_S)
            {
                SetBoolForSwitch(false, true, true);
                StartCoroutine(ChangePositionInTeam_F_To_L());

                Debug.Log("SwapF_S");
            }
        }
        else if (GameMananger.instance.canPress)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetBoolForSwitch(false, true, true);
                StartCoroutine(ChangePositionInTeam_F_To_L());

                Debug.Log("SwapF_S");

            }
        }
    }
    IEnumerator ChangePositionInTeam_F_To_L()
    {
        List<Vector2> originPos = PlayerManager.instance.heroPosition;
        int lastIndex = originPos.Count - 1;

        for (int i = 0; i < originPos.Count; i++)
        {
            if (i == 0)
            {
                PlayerManager.instance.heroList[i].position = originPos[lastIndex];
                PlayerManager.instance.heroList[i].SetSiblingIndex(lastIndex);
            }
            else
            {
                int erase = i - 1;
                PlayerManager.instance.heroList[i].position = originPos[erase];
                PlayerManager.instance.heroList[i].SetSiblingIndex(erase);

            }
        }

        PlayerManager.instance.ResetListAndAddNew();
        yield return new WaitForSeconds(3f);
        SetBoolForSwitch(true, false, false);
    }
    #endregion

    #region SwitchLastToFirst

    public void SwitchLastToFirst()
    {
        //■ Pressing E on the keyboard or the right shoulder button on the gamepad will
        // switch the last character in line to be the front character and the front
        // character to be the second

        if (GameMananger.instance.isGamePadDetect && GameMananger.instance.canPress)
        {
            if ((Input.GetKeyDown(KeyCode.E) || Gamepad.current.rightShoulder.isPressed) && !GameMananger.instance.isSwtichL_To_F)
            {
                SetBoolForSwitch(false, true, true);
                StartCoroutine(ChangePositionInTeam_L_To_F());
                Debug.Log("SwtichL_S");
            }
        }
        else if (GameMananger.instance.canPress)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SetBoolForSwitch(false, true, true);
                StartCoroutine(ChangePositionInTeam_L_To_F());
                Debug.Log("SwtichL_S");

            }
        }

    }

    IEnumerator ChangePositionInTeam_L_To_F()
    {
        List<Vector2> originPos = PlayerManager.instance.heroPosition;
        int lastIndex = originPos.Count - 1;

        for (int i = 0; i < originPos.Count; i++)
        {
            if (i < lastIndex)
            {
                int add = i + 1;
                PlayerManager.instance.heroList[i].position = originPos[add];
                PlayerManager.instance.heroList[i].SetSiblingIndex(add);
                // Debug.Log(i);
            }
            else
            {
                PlayerManager.instance.heroList[lastIndex].position = originPos[0];
                PlayerManager.instance.heroList[lastIndex].SetSiblingIndex(0);
            }
        }

        PlayerManager.instance.ResetListAndAddNew();
        yield return new WaitForSeconds(3f);
        SetBoolForSwitch(true, false, false);
    }
    #endregion

    #region Attack
    public void InputAttack()
    {
        if (GameMananger.instance.isGamePadDetect)
        {
            if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Gamepad.current.rightTrigger.wasPressedThisFrame) && !isEndAttack) 
            {
                isEndAttack = true;
                StartCoroutine(battleSystems.PlayerTurnAttack());
                Debug.Log("Player_" + name + "Attack_" + battleSystems.monsterControl);
            }
            else
            {
                Debug.Log("not right input!!!");
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.Return) && !isEndAttack))
            {
                isEndAttack = true;
                StartCoroutine(battleSystems.PlayerTurnAttack());
                Debug.Log("Player_" + name + "Attack_" + battleSystems.monsterControl);

            }
            else if (!Input.GetKeyDown(KeyCode.Space) && Input.anyKey) 
            {
                Debug.Log("not right input!!!");
            }

        }
    }
                
    #endregion
    private void SetBoolForSwitch(bool canPress, bool isSwtichL_To_F, bool isSwtichF_To_S)
    {
        GameMananger.instance.canPress = canPress;
        GameMananger.instance.isSwtichF_To_S = isSwtichL_To_F;
        GameMananger.instance.isSwtichL_To_F = isSwtichF_To_S;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Found Monster");

            if (!battleSystems.isPlayerContinue)
            {
                battleSystems.state = BattleSystems.battleStage.RANDOM;
                displayUI.oddOrEvenHead.SetActive(true);
                PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.BATTLE;

                GetMonster(collision);
                GetPlayer();
            }
            else
            {
                GetPlayer();
            }

        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.SetActive(false);
            PlayerManager.instance.RemovePlayerDie();
        }
    }

    private void GetPlayer()
    {
        //get first player
        PlayerController _player = PlayerManager.instance.heroList[0].gameObject.GetComponent<PlayerController>();
        battleSystems.playerControl = _player;
    }

    private void GetMonster(Collider2D collision)
    {
        //get monster detech
        MonsterController _monster = collision.transform.parent.parent.GetComponent<MonsterController>();
        battleSystems.monsterControl = _monster;
    }
}

