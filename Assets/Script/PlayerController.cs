﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Unit playerProfile;
    private GameMananger gameMananger;
    private PlayerManager playerManager;
    private BattleSystems battleSystems;
    private int setNumber = 0;
    private bool isSetNumber = false;
    private UIController uIController;
    // Start is called before the first frame update
    void Start()
    {
        gameMananger = FindAnyObjectByType<GameMananger>();
        playerManager = FindAnyObjectByType<PlayerManager>();
        battleSystems = FindAnyObjectByType<BattleSystems>();
        uIController = FindAnyObjectByType<UIController>();

    }

    // Update is called once per frame
    void Update()
    {
        SwitchFirstToSecond();
        SwitchLastToFirst();

        switch (battleSystems.state)
        {
            case BattleSystems.battleStage.START:
                break;
            case BattleSystems.battleStage.RANDOM:
                if (!isSetNumber && uIController.oddOrEvenHead.activeSelf)
                {
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        setNumber = 2;                        
                        isSetNumber = true;                     
                        StartCoroutine(wait());

                    }
                    else if (Input.GetKeyDown(KeyCode.K))
                    {
                        setNumber = 1;                      
                        isSetNumber = true;
                        StartCoroutine(wait());


                    }
                    else
                    {
                        Debug.Log("not right input!!!");
                    }
                }
                break;
            case BattleSystems.battleStage.PLAYERTURN:
                break;
            case BattleSystems.battleStage.MONSTERTURN:
                break;
            case BattleSystems.battleStage.WON:
                break;
            case BattleSystems.battleStage.LOST:
                break;
        }
    }
    #region SwitchFirstToSecond
    public void SwitchFirstToSecond()
    {
        //       Pressing Q on the keyboard or the left shoulder button on the gamepad will
        //rotate the second character in line to be the front character and the front
        //character to be the last.
        if (gameMananger.isGamePadDetect && gameMananger.canPress)
        {
            if ((Input.GetKeyDown(KeyCode.Q) || Gamepad.current.leftShoulder.isPressed) && !gameMananger.isSwtichF_To_S)
            {
                SetBoolForSwitch(false, true, true);
                StartCoroutine(ChangePositionInTeam_F_To_L());

                Debug.Log("SwapF_S");
            }
        }
        else if (gameMananger.canPress)
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
        List<Vector2> originPos = playerManager.heroPosition;
        int lastIndex = originPos.Count - 1;

        for (int i = 0; i < originPos.Count; i++)
        {
            if (i == 0)
            {
                playerManager.heroList[i].position = originPos[lastIndex];
                playerManager.heroList[i].SetSiblingIndex(lastIndex);
            }
            else
            {
                int erase = i - 1;
                playerManager.heroList[i].position = originPos[erase];
                playerManager.heroList[i].SetSiblingIndex(erase);

            }
        }

        playerManager.ResetListAndAddNew();
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

        if (gameMananger.isGamePadDetect && gameMananger.canPress)
        {
            if ((Input.GetKeyDown(KeyCode.E) || Gamepad.current.rightShoulder.isPressed) && !gameMananger.isSwtichL_To_F)
            {
                SetBoolForSwitch(false, true, true);
                StartCoroutine(ChangePositionInTeam_L_To_F());
                Debug.Log("SwtichL_S");
            }
        }
        else if (gameMananger.canPress)
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
        List<Vector2> originPos = playerManager.heroPosition;
        int lastIndex = originPos.Count - 1;

        for (int i = 0; i < originPos.Count; i++)
        {
            if (i < lastIndex)
            {
                int add = i + 1;
                playerManager.heroList[i].position = originPos[add];
                playerManager.heroList[i].SetSiblingIndex(add);
                // Debug.Log(i);
            }
            else
            {
                playerManager.heroList[lastIndex].position = originPos[0];
                playerManager.heroList[lastIndex].SetSiblingIndex(0);
            }
        }

        playerManager.ResetListAndAddNew();
        yield return new WaitForSeconds(3f);
        SetBoolForSwitch(true, false, false);
    }
    #endregion

    private void SetBoolForSwitch(bool canPress, bool isSwtichL_To_F, bool isSwtichF_To_S)
    {
        gameMananger.canPress = canPress;
        gameMananger.isSwtichF_To_S = isSwtichL_To_F;
        gameMananger.isSwtichL_To_F = isSwtichF_To_S;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("hit");

            battleSystems.state = BattleSystems.battleStage.RANDOM;
            uIController.oddOrEvenHead.SetActive(true);           
        }
    }

    IEnumerator wait()
    {
        battleSystems.RandomNumber();
        uIController.numberText.text = battleSystems.rand.ToString();
        uIController.result.SetActive(true);
        uIController.uiOddEven.SetActive(false);      

        //uIController.oddOrEvenHead.SetActive(false);
        //Debug.Log("CloseUI");
        if (setNumber.Equals(2) && battleSystems.isEven)
        {
            uIController.tellPlayer.text = "Just lucky...";

            // battleSystems.state = BattleSystems.battleStage.PLAYERTURN;
        }
        else if (setNumber.Equals(2) && !battleSystems.isEven)
        {
            uIController.tellPlayer.text = "Too badd!!";
            //battleSystems.state = BattleSystems.battleStage.MONSTERTURN;
        }
        else if (setNumber.Equals(1) && battleSystems.isEven)
        {
            uIController.tellPlayer.text = "Too badd!!";
            // battleSystems.state = BattleSystems.battleStage.MONSTERTURN;
        }
        else
        {
            uIController.tellPlayer.text = "Just lucky...";
            //battleSystems.state = BattleSystems.battleStage.PLAYERTURN;
        }
        yield return new WaitForSeconds(3f);
        // Debug.Log("ChangeState");
    }



}
