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
    [SerializeField] private Transform mark;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        currentPos = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {

        switch (PlayerManager.instance.currentPlayerStage)
        {
            case PlayerManager.playerStage.MOVE:
                if (GameMananger.instance.isGamePadDetect)
                {
                    InputGamePadAndKeyborad();

                }
                else
                {
                    InputOnlyKeyboard();
                    // Debug.Log(currentPos);

                }
                CheckPlayerHitItSelf();
                break;
            case PlayerManager.playerStage.BATTLE:
                break;

        }
    }


    private void InputGamePadAndKeyborad()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Gamepad.current.dpad.up.wasPressedThisFrame) && !PlayerManager.instance.isLockU)
        {
            currentPos += Vector3.up;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            PlayerManager.instance.isLockD = true;
            PlayerManager.instance.isLockL = false;
            PlayerManager.instance.isLockR = false;
            PlayerManager.instance.isLockU = false;
            transform.localPosition = currentPos;
            PlayerManager.instance.WhenMoveUpdatePostionTeam();

        }
        else if ((Input.GetKeyDown(KeyCode.S) || Gamepad.current.dpad.down.wasPressedThisFrame) && !PlayerManager.instance.isLockD)
        {
            currentPos += Vector3.down;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
            PlayerManager.instance.isLockU = true;
            PlayerManager.instance.isLockL = false;
            PlayerManager.instance.isLockR = false;
            PlayerManager.instance.isLockD = false;
            transform.localPosition = currentPos;
            PlayerManager.instance.WhenMoveUpdatePostionTeam();
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Gamepad.current.dpad.left.wasPressedThisFrame) && !PlayerManager.instance.isLockL)
        {
            currentPos += Vector3.left;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90f));
            PlayerManager.instance.FlipXAllHero(true);
            PlayerManager.instance.isLockR = true;
            PlayerManager.instance.isLockD = false;
            PlayerManager.instance.isLockU = false;
            PlayerManager.instance.isLockL = false;
            transform.localPosition = currentPos;
            PlayerManager.instance.WhenMoveUpdatePostionTeam();

        }
        else if ((Input.GetKeyDown(KeyCode.D) || Gamepad.current.dpad.right.wasPressedThisFrame) && !PlayerManager.instance.isLockR)
        {
            currentPos += Vector3.right;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
            PlayerManager.instance.FlipXAllHero(false);
            PlayerManager.instance.isLockL = true;
            PlayerManager.instance.isLockR = false;
            PlayerManager.instance.isLockU = false;
            PlayerManager.instance.isLockD = false;
            transform.localPosition = currentPos;
            PlayerManager.instance.WhenMoveUpdatePostionTeam();

        }
    }



    private void InputOnlyKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.W) && !PlayerManager.instance.isLockU)
        {
            PlayerManager.instance.isLockD = true;
            PlayerManager.instance.isLockL = false;
            PlayerManager.instance.isLockR = false;
            PlayerManager.instance.isLockU = false;
            currentPos += Vector3.up;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            transform.localPosition = currentPos;
            transform.localPosition.Normalize();
            PlayerManager.instance.WhenMoveUpdatePostionTeam();

        }
        else if (Input.GetKeyDown(KeyCode.S) && !PlayerManager.instance.isLockD)
        {
            PlayerManager.instance.isLockU = true;
            PlayerManager.instance.isLockL = false;
            PlayerManager.instance.isLockR = false;
            PlayerManager.instance.isLockD = false;
            currentPos += Vector3.down;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
            transform.localPosition = currentPos;
            transform.localPosition.Normalize();

            PlayerManager.instance.WhenMoveUpdatePostionTeam();
        }
        else if (Input.GetKeyDown(KeyCode.A) && !PlayerManager.instance.isLockL)
        {
            PlayerManager.instance.isLockR = true;
            PlayerManager.instance.isLockD = false;
            PlayerManager.instance.isLockU = false;
            PlayerManager.instance.isLockL = false;
            currentPos += Vector3.left;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90f));
            PlayerManager.instance.FlipXAllHero(true);
            transform.localPosition = currentPos;
            transform.localPosition.Normalize();

            PlayerManager.instance.WhenMoveUpdatePostionTeam();

        }
        else if (Input.GetKeyDown(KeyCode.D) && !PlayerManager.instance.isLockR)
        {
            PlayerManager.instance.isLockL = true;
            PlayerManager.instance.isLockR = false;
            PlayerManager.instance.isLockU = false;
            PlayerManager.instance.isLockD = false;
            currentPos += Vector3.right;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
            PlayerManager.instance.FlipXAllHero(false);
            transform.localPosition = currentPos;
            PlayerManager.instance.WhenMoveUpdatePostionTeam();
        }

    }


    public void CheckPlayerHitItSelf()
    {
        for (int i = 0; i < PlayerManager.instance.heroList.Count; i++)
        {
            if (i != 0)
            {
                //if (PlayerManager.instance.heroList[0].transform.position == PlayerManager.instance.heroList[i].transform.position)
                //{
                //    PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.GAMEOVER;
                //}
                //else if (mark.transform.position == PlayerManager.instance.heroList[i].transform.position)
                //{
                //    PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.GAMEOVER;

                //}

                if (mark.transform.position == PlayerManager.instance.heroList[i].transform.position)
                {
                    PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.GAMEOVER;

                }
            }
        }
    }
}
