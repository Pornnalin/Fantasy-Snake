using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rigi;
    private Vector3 currentPos;
    [SerializeField] private bool isLockU, isLockD, isLockR, isLockL;
    [Space]
    [SerializeField] private SpriteRenderer[] collidersHit;
    [SerializeField] private SpriteRenderer[] arrow;
    [SerializeField] private Color colorEnable, colorDisable;
    [SerializeField] private PlayerManager playerManager;
    private GameMananger gameMananger;

    // Start is called before the first frame update
    void Start()
    {
        //   playerInput = GetComponent<PlayerInput>();
        rigi = GetComponent<Rigidbody2D>();
        playerManager = transform.root.GetComponent<PlayerManager>();
        gameMananger = FindAnyObjectByType<GameMananger>();
        currentPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        // Debug.Log(Gamepad.current);        
        switch (playerManager.currentPlayerStage)
        {
            case PlayerManager.playerStage.MOVE:
                if (gameMananger.isGamePadDetect)
                {
                    InputGamePadAndKeyborad();
                    //Check();
                }
                else
                {
                    InputOnlyKeyboard();
                }
                    CheckPlayerMove();
                break;
            case PlayerManager.playerStage.BATTLE:
                break;
            case PlayerManager.playerStage.DIE:
                break;
        }
    }
    private void LateUpdate()
    {
        // playerManager.UpdateNewPositionTeam();
    }

    private void InputGamePadAndKeyborad()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Gamepad.current.dpad.up.wasPressedThisFrame) && !isLockU)
        {
            currentPos += Vector3.up;
            isLockD = true;
            isLockL = false;
            isLockR = false;
            isLockU = false;
            UpdateColorArrow(1);
            transform.position = currentPos;
            playerManager.UpdatePostionTeamWhenMove();

        }
        else if ((Input.GetKeyDown(KeyCode.S) || Gamepad.current.dpad.down.wasPressedThisFrame) && !isLockD)
        {
            currentPos += Vector3.down;
            isLockU = true;
            isLockL = false;
            isLockR = false;
            isLockD = false;
            UpdateColorArrow(0);
            transform.position = currentPos;
            playerManager.UpdatePostionTeamWhenMove();
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Gamepad.current.dpad.left.wasPressedThisFrame) && !isLockL)
        {
            currentPos += Vector3.left;
            //playerSprite.flipX = true;
            playerManager.FlipXAllHero(true);
            isLockR = true;
            isLockD = false;
            isLockU = false;
            isLockL = false;
            UpdateColorArrow(2);
            transform.position = currentPos;
            playerManager.UpdatePostionTeamWhenMove();

        }
        else if ((Input.GetKeyDown(KeyCode.D) || Gamepad.current.dpad.right.wasPressedThisFrame) && !isLockR)
        {
            currentPos += Vector3.right;
            playerManager.FlipXAllHero(false);
            //playerSprite.flipX = false;
            isLockL = true;
            isLockR = false;
            isLockU = false;
            isLockD = false;
            UpdateColorArrow(3);
            transform.position = currentPos;
            playerManager.UpdatePostionTeamWhenMove();

        }
    }

    private void InputOnlyKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isLockU)
        {
            currentPos += Vector3.up;
           // transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            isLockD = true;
            isLockL = false;
            isLockR = false;
            isLockU = false;
            UpdateColorArrow(1);
            transform.position = currentPos;
            playerManager.UpdatePostionTeamWhenMove();

        }
        else if (Input.GetKeyDown(KeyCode.S) && !isLockD)
        {
            currentPos += Vector3.down;
          //  transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180f));
            isLockU = true;
            isLockL = false;
            isLockR = false;
            isLockD = false;
            UpdateColorArrow(0);
            transform.position = currentPos;
            playerManager.UpdatePostionTeamWhenMove();
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isLockL)
        {
            currentPos += Vector3.left;
           // transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
            //playerSprite.flipX = true;
            playerManager.FlipXAllHero(true);

            isLockR = true;
            isLockD = false;
            isLockU = false;
            isLockL = false;
            UpdateColorArrow(2);
            transform.position = currentPos;
            playerManager.UpdatePostionTeamWhenMove();

        }
        else if (Input.GetKeyDown(KeyCode.D) && !isLockR)
        {
            currentPos += Vector3.right;
            //playerSprite.flipX = false;
            playerManager.FlipXAllHero(false);
            isLockL = true;
            isLockR = false;
            isLockU = false;
            isLockD = false;
            UpdateColorArrow(3);
            transform.position = currentPos;
            playerManager.UpdatePostionTeamWhenMove();
        }

    }

    private void UpdateColorArrow(int indexDisable)
    {
        for (int i = 0; i < arrow.Length; i++)
        {
            arrow[i].color = i == indexDisable ? colorDisable : colorEnable;
        }
    }
    public void CheckPlayerMove()
    {
        for (int i = 0; i < playerManager.heroList.Count; i++)
        {
            if (i != 0)
            {
                if (playerManager.heroList[0].transform.position == playerManager.heroList[i].transform.position)
                {
                    playerManager.currentPlayerStage = PlayerManager.playerStage.DIE;
                }
            }
        }
    }
}
