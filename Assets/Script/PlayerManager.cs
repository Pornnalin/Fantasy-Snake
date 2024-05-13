using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public enum playerStage { NONE, MOVE, BATTLE, GAMEOVER }
    public playerStage currentPlayerStage;
    [Space]
    public List<Vector2> playerPosition = new List<Vector2>();
    public List<float> playerRotation = new List<float>();
    public List<Transform> playerTransList = new List<Transform>();
    public List<Transform> PlayerNotInTeam = new List<Transform>();  
    public List<SpriteRenderer> heroSprite = new List<SpriteRenderer>();
    [Space]
    public int amountKilled;
    private SpriteRenderer playerFirst;
    private DisplayUI displayUI;
    private Transform playerDieTran;
    [Space]
    public bool isLockU, isLockD, isLockR, isLockL;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        currentPlayerStage = playerStage.NONE;
    }
    void Start()
    {
        GetData();
        displayUI = FindAnyObjectByType<DisplayUI>();
    }
    private void Update()
    {
        displayUI.UpdateColor(GameMananger.instance.isGamePadDetect ? Color.white : Color.gray);
        displayUI.keyboardUi.color = Keyboard.current != null ? Color.white : Color.gray;

        if (playerTransList.Count<=0)
        {
            currentPlayerStage = playerStage.GAMEOVER;
        }
        switch (currentPlayerStage)
        {
            case playerStage.MOVE:
                break;
            case playerStage.BATTLE:
                break;
            case playerStage.GAMEOVER:

                displayUI.resetPanel.SetActive(true);
                displayUI.monsBattleUI.SetActive(false);
                displayUI.playerBattleUI.SetActive(false);
                displayUI.oddOrEvenHead.SetActive(false);
                displayUI.amountKill.text = amountKilled.ToString();
                InputResetGame();
                break;
        }

    }
    #region InputResetGame
    private void InputResetGame()
    {
        if (GameMananger.instance.isGamePadDetect)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Gamepad.current.rightTrigger.wasPressedThisFrame)
            {
                GameMananger.instance.ResetGame(0);

            }
            else if ((!Input.GetKeyDown(KeyCode.Return) || !Gamepad.current.rightTrigger.wasPressedThisFrame) && Input.anyKey)
            {
                Debug.Log("not right input!!!");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameMananger.instance.ResetGame(0);

            }
            else if (!Input.GetKeyDown(KeyCode.Return) && Input.anyKey)
            {
                Debug.Log("not right input!!!");
            }

        }
    }
    #endregion
    public void FlipXAllHero(bool isFlipX)
    {
        for (int i = 0; i < playerPosition.Count; i++)
        {
            heroSprite[i].flipX = isFlipX;
        }
    }

    public void GetData()
    {
        int childLength = this.transform.childCount;
        for (int i = 0; i < childLength; i++)
        {
            playerPosition.Add(transform.GetChild(i).transform.localPosition);
            playerRotation.Add(transform.GetChild(i).transform.localRotation.eulerAngles.z);
            playerTransList.Add(transform.GetChild(i));
            heroSprite.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
            ScriptTurnONOFF(i);

        }
        playerFirst = playerTransList[0].GetComponent<SpriteRenderer>();
        Debug.Log("GetPlayerToList");
    }

    public void WhenMoveUpdatePostionTeam()
    {
        int childLength = this.transform.childCount - 1;        
        for (int i = 0; i < childLength; i++)
        {
            int SkipZero = i + 1;
            playerTransList[SkipZero].transform.position = playerPosition[i];

            playerTransList[SkipZero].transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerTransList[0].transform.localRotation.eulerAngles.z));
        }
        
        playerPosition[0] = playerTransList[0].transform.position;
        playerRotation[0] = playerTransList[0].transform.localRotation.eulerAngles.z;

        //update new position
        UpdateNewPositionTeam();
    }
    
    
    public void UpdateNewPositionTeam()
    {
        for (int i = 0; i < playerPosition.Count; i++)
        {
            playerPosition[i] = playerTransList[i].position;
        }
    }
    public void UpadteEnabledScript()
    {
        for (int i = 0; i < playerPosition.Count; i++)
        {
            ScriptTurnONOFF(i);
        }
    }

    private void ScriptTurnONOFF(int i)
    {
        if (i == 0)
        {
            playerTransList[i].gameObject.GetComponent<PlayerController>().enabled = true;
            playerTransList[i].gameObject.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            playerTransList[i].gameObject.GetComponent<PlayerController>().enabled = false;
            playerTransList[i].gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void ResetListAndAddNew()
    {
        playerTransList = new List<Transform>();
        playerPosition = new List<Vector2>();
        heroSprite = new List<SpriteRenderer>();
        playerRotation = new List<float>();
        GetData();

    }
   
    public void RemovePlayerDie()
    {       

        List<Vector2> originPos = new List<Vector2>(playerPosition);
        List<Vector2> copy = new List<Vector2>();
        int lastIndex = originPos.Count - 1;
        for (int i = 0; i < originPos.Count; i++)
        {
            if (i < lastIndex)
            {
                copy.Add(originPos[i]);

            }

        }
        playerPosition = new List<Vector2>();
        foreach (var item in copy)
        {
            playerPosition.Add(item);
        }

        playerTransList[0].gameObject.SetActive(false);
        GameObject move = playerTransList[0].gameObject;
        move.transform.SetParent(playerDieTran);       
        Debug.Log("<color=red>" + move.name + "_Die</color>");

        playerTransList.RemoveAt(0);
        heroSprite.RemoveAt(0);      

        for (int i = 0; i < playerTransList.Count; i++)
        {
            playerTransList[i].position = new Vector3();
            playerTransList[i].position = new Vector3(copy[i].x, copy[i].y, 0);
            ScriptTurnONOFF(i);
        }

    }
}