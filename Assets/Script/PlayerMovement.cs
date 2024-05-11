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
 //   private float currentRotZ;
    //[SerializeField] private bool isLockU, isLockD, isLockR, isLockL;
    [Space]
    //  [SerializeField] private SpriteRenderer[] collidersHit;
    [SerializeField] private SpriteRenderer[] arrow;
    [SerializeField] private Color colorEnable, colorDisable;
    [SerializeField] private Transform mark;
    // [SerializeField] private PlayerManager playerManager;
    // private GameMananger gameMananger;

    // Start is called before the first frame update
    void Start()
    {
        //   playerInput = GetComponent<PlayerInput>();
        rigi = GetComponent<Rigidbody2D>();
        //playerManager = transform.root.GetComponent<PlayerManager>();
        //  gameMananger = FindAnyObjectByType<GameMananger>();
        currentPos = transform.position;
      //  currentRotZ = transform.localRotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        // moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        // Debug.Log(Gamepad.current);        
        switch (PlayerManager.instance.currentPlayerStage)
        {
            case PlayerManager.playerStage.MOVE:
                if (GameMananger.instance.isGamePadDetect)
                {
                    //InputGamePadAndKeyborad();
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

        }
    }
    private void LateUpdate()
    {
        // playerManager.UpdateNewPositionTeam();
    }

    //private void InputGamePadAndKeyborad()
    //{
    //    if ((Input.GetKeyDown(KeyCode.W) || Gamepad.current.dpad.up.wasPressedThisFrame) && PlayerManager.instance.isLockU == false) 
    //    {
    //        currentPos += Vector3.up;
    //        PlayerManager.instance.isLockD = true;
    //        PlayerManager.instance.isLockL = false;
    //        PlayerManager.instance.isLockR = false;
    //        PlayerManager.instance.isLockU = false;
    //        UpdateColorArrow(1);
    //        transform.position = currentPos;
    //        PlayerManager.instance.WhenMoveUpdatePostionTeam();

    //    }
    //    else if ((Input.GetKeyDown(KeyCode.S) || Gamepad.current.dpad.down.wasPressedThisFrame) && !isLockD)
    //    {
    //        currentPos += Vector3.down;
    //        isLockU = true;
    //        isLockL = false;
    //        isLockR = false;
    //        isLockD = false;
    //        UpdateColorArrow(0);
    //        transform.position = currentPos;
    //        PlayerManager.instance.WhenMoveUpdatePostionTeam();
    //    }
    //    else if ((Input.GetKeyDown(KeyCode.A) || Gamepad.current.dpad.left.wasPressedThisFrame) && !isLockL)
    //    {
    //        currentPos += Vector3.left;
    //        //playerSprite.flipX = true;
    //        PlayerManager.instance.FlipXAllHero(true);
    //        isLockR = true;
    //        isLockD = false;
    //        isLockU = false;
    //        isLockL = false;
    //        UpdateColorArrow(2);
    //        transform.position = currentPos;
    //        PlayerManager.instance.WhenMoveUpdatePostionTeam();

    //    }
    //    else if ((Input.GetKeyDown(KeyCode.D) || Gamepad.current.dpad.right.wasPressedThisFrame) && !isLockR)
    //    {
    //        currentPos += Vector3.right;
    //        PlayerManager.instance.FlipXAllHero(false);
    //        //playerSprite.flipX = false;
    //        isLockL = true;
    //        isLockR = false;
    //        isLockU = false;
    //        isLockD = false;
    //        UpdateColorArrow(3);
    //        transform.position = currentPos;
    //        PlayerManager.instance.WhenMoveUpdatePostionTeam();

    //    }
    //}

    private void InputOnlyKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.W) && PlayerManager.instance.isLockU == false) 
        {
            currentPos += Vector3.up;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            PlayerManager.instance.isLockD = true;
            PlayerManager.instance.isLockL = false;
            PlayerManager.instance.isLockR = false;
            PlayerManager.instance.isLockU = false;
            UpdateColorArrow(1);
            transform.position = currentPos;
            PlayerManager.instance.WhenMoveUpdatePostionTeam();

        }
        else if (Input.GetKeyDown(KeyCode.S) && PlayerManager.instance.isLockD == false)
        {
            currentPos += Vector3.down;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));

            PlayerManager.instance.isLockU = true;
            PlayerManager.instance.isLockL = false;
            PlayerManager.instance.isLockR = false;
            PlayerManager.instance.isLockD = false;
            UpdateColorArrow(0);
            transform.position = currentPos;
            PlayerManager.instance.WhenMoveUpdatePostionTeam();
        }
        else if (Input.GetKeyDown(KeyCode.A) && PlayerManager.instance.isLockL == false)
        {
            currentPos += Vector3.left;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
            //playerSprite.flipX = true;
            PlayerManager.instance.FlipXAllHero(true);

            PlayerManager.instance.isLockR = true;
            PlayerManager.instance.isLockD = false;
            PlayerManager.instance.isLockU = false;
            PlayerManager.instance.isLockL = false;
            UpdateColorArrow(2);
            transform.position = currentPos;
            PlayerManager.instance.WhenMoveUpdatePostionTeam();

        }
        else if (Input.GetKeyDown(KeyCode.D) && PlayerManager.instance.isLockR == false)
        {
            currentPos += Vector3.right;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));

            //playerSprite.flipX = false;
            PlayerManager.instance.FlipXAllHero(false);
            PlayerManager.instance.isLockL = true;
            PlayerManager.instance.isLockR = false;
            PlayerManager.instance.isLockU = false;
            PlayerManager.instance.isLockD = false;
            UpdateColorArrow(3);
            transform.position = currentPos;
            PlayerManager.instance.WhenMoveUpdatePostionTeam();
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
        for (int i = 0; i < PlayerManager.instance.heroList.Count; i++)
        {
            if (i != 0)
            {
                //if (PlayerManager.instance.heroList[0].transform.position == PlayerManager.instance.heroList[i].transform.position)
                //{
                //    PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.GAMEOVER;
                //}
                //else 
                if (mark.transform.position == PlayerManager.instance.heroList[i].transform.position)
                {
                    PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.GAMEOVER;

                }
            }
        }
    }
}
