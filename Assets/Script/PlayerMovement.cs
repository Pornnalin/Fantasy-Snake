using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private Vector2 moveInput;
    private Rigidbody2D rigi;
    private Vector3 currentPos;
    [SerializeField] private bool isGamePadDetect;
    [SerializeField] private bool isLockU, isLockD, isLockR, isLockL;
    [Space]
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private SpriteRenderer[] collidersHit;
    [SerializeField] private SpriteRenderer[] arrow;
    [SerializeField] private Color colorEnable, colorDisable;
    [Space]
    [SerializeField] private GameObject heroMain;
    [SerializeField] private GameObject heroChild;
    [SerializeField]private List<Vector2> heroPosition = new List<Vector2>();
    [SerializeField]private List<Transform> heroChildTran = new List<Transform>();

    // private InputActionAsset inputActionAsset;

    // Start is called before the first frame update
    void Start()
    {
        //   playerInput = GetComponent<PlayerInput>();
        rigi = GetComponent<Rigidbody2D>();
        currentPos = transform.position;
        heroPosition.Add(heroMain.transform.position);
        heroChildTran.Add(heroMain.transform);
        int childLength = heroChild.transform.childCount;
        for (int i = 0; i < childLength; i++)
        {
            heroPosition.Add(heroChild.transform.GetChild(i).transform.position);
            heroChildTran.Add(heroChild.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        // Debug.Log(Gamepad.current);
        isGamePadDetect = Gamepad.current == null ? false : true;

        if (isGamePadDetect)
        {
            if (Input.GetKeyDown(KeyCode.W) || Gamepad.current.dpad.up.wasPressedThisFrame && !isLockD)
            {
                currentPos += Vector3.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Gamepad.current.dpad.down.wasPressedThisFrame && !isLockU)
            {
                currentPos += Vector3.down;

            }
            else if (Input.GetKeyDown(KeyCode.A) || Gamepad.current.dpad.left.wasPressedThisFrame && !isLockL)
            {
                currentPos += Vector3.left;
                playerSprite.transform.localScale = new Vector3(-1, 1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Gamepad.current.dpad.right.wasPressedThisFrame && isLockR)
            {
                currentPos += Vector3.right;
                playerSprite.transform.localScale = new Vector3(1, 1, 0);
            }
            transform.position = currentPos;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) && !isLockU)
            {
                currentPos += Vector3.up;
                isLockD = true;
                isLockL = false;
                isLockR = false;
                isLockU = false;
                UpdateColorArrow(1);
                transform.position = currentPos;
                UpdatePostionHeroTeam();
                // UpdatePostionHeroTeam();
            }
            else if (Input.GetKeyDown(KeyCode.S) && !isLockD)
            {
                currentPos += Vector3.down;
                isLockU = true;
                isLockL = false;
                isLockR = false;
                isLockD = false;
                UpdateColorArrow(0);
                //UpdatePostionHeroTeam();
                transform.position = currentPos;
                UpdatePostionHeroTeam();
            }
            else if (Input.GetKeyDown(KeyCode.A) && !isLockL)
            {
                currentPos += Vector3.left;
                //playerSprite.transform.localScale = new Vector3(-1, 1, 0);
                playerSprite.flipX = true;

                isLockR = true;
                isLockD = false;
                isLockU = false;
                isLockL = false;
                UpdateColorArrow(2);
                // UpdatePostionHeroTeam();
                transform.position = currentPos;
                UpdatePostionHeroTeam();

            }
            else if (Input.GetKeyDown(KeyCode.D) && !isLockR)
            {
                currentPos += Vector3.right;
                // playerSprite.transform.localScale = new Vector3(1, 1, 0);
                playerSprite.flipX = false;
                isLockL = true;
                isLockR = false;
                isLockU = false;
                isLockD = false;
                UpdateColorArrow(3);
                transform.position = currentPos;
                UpdatePostionHeroTeam();

            }
        
        }
    }

    private void UpdateColorArrow(int indexDisable)
    {
        for (int i = 0; i < arrow.Length; i++)
        {            
            arrow[i].color = i == indexDisable ? colorDisable : colorEnable;
        }
    }
    void UpdatePostionHeroTeam()
    {
        int childLength = heroChild.transform.childCount;
        for (int i = 0; i < childLength; i++)
        {
            //  Debug.Log(i);
            int crossHero = i + 1;
            heroChildTran[crossHero].transform.position = heroPosition[i];
           // Debug.Log(heroPosition[i]);
            // Debug.Log(heroChild.transform.GetChild(i).transform);
        }
        heroPosition[0] = heroMain.transform.position;

    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("hit it!!");
        }
    }

}

