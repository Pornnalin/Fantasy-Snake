using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public enum playerStage { MOVE, BATTLE, GAMEOVER }
    public playerStage currentPlayerStage;
    [Space]
    public List<Vector2> heroPosition = new List<Vector2>();
    public List<float> heroRotation = new List<float>();
    public List<Transform> heroList = new List<Transform>();
  //  public List<Vector2> her = new List<Vector2>();
    public List<SpriteRenderer> heroSprite = new List<SpriteRenderer>();
    [Space]
    public int amountKilled;
    private SpriteRenderer heroFirst;
    public Transform playerDieTran;
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
    }
    void Start()
    {
        GetData();
        currentPlayerStage = playerStage.MOVE;
    }
    private void Update()
    {
        //  FlipXAllHero();

    }

    public void FlipXAllHero(bool isFlipX)
    {
        for (int i = 0; i < heroPosition.Count; i++)
        {
            heroSprite[i].flipX = isFlipX;
        }
    }

    private void GetData()
    {
        int childLength = this.transform.childCount;
        for (int i = 0; i < childLength; i++)
        {
            heroPosition.Add(transform.GetChild(i).transform.position);
            heroRotation.Add(transform.GetChild(i).transform.localRotation.eulerAngles.z);
            heroList.Add(transform.GetChild(i));
            heroSprite.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
            ScriptTurnONOFF(i);

        }
        heroFirst = heroList[0].GetComponent<SpriteRenderer>();
    }
    public void WhenMoveUpdatePostionTeam()
    {
        int childLength = this.transform.childCount - 1;
        int childLength_2 = this.transform.childCount - 2;
        for (int i = 0; i < childLength; i++)
        {
            int SkipZero = i + 1;
            heroList[SkipZero].transform.position = heroPosition[i];
           
            heroList[SkipZero].transform.rotation = Quaternion.Euler(new Vector3(0, 0, heroList[0].transform.localRotation.eulerAngles.z));
        }
     //   AlongRot();
        heroPosition[0] = heroList[0].transform.position;
        heroRotation[0] = heroList[0].transform.localRotation.eulerAngles.z;

        //    //update last position
        UpdateNewPositionTeam();
    }
    public void AlongRot()
    {
        int childLength = this.transform.childCount - 1;
        int childLength_2 = this.transform.childCount - 2;

        for (int i = 0; i < heroList.Count; i++)
        {
            int before = i - 1;
            if (i == 0) { continue; }
            if (heroList[0].transform.localRotation.eulerAngles.z == 180)//
            {
                if (heroList[i].transform.position.x != heroList[before].transform.position.x)
                {
                    heroList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
                }
            }
            else if(heroList[0].transform.localRotation.eulerAngles.z == -180)
            {
                if (heroList[i].transform.position.x != heroList[before].transform.position.x)
                {
                    heroList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
                }
            }
            else if (heroList[0].transform.localRotation.eulerAngles.z == 270)//
            {
                //if (heroList[i].transform.position.y != heroList[before].transform.position.y)
                //{
                //    heroList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                //}
            }

            else if (heroList[0].transform.localRotation.eulerAngles.z == 0)//
            {
                //    if (heroList[i].transform.position.x != heroList[before].transform.position.x)
                //    {
                //        heroList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
                //    }
            }
            else if (heroList[0].transform.localRotation.eulerAngles.z == 90)//
            {
                //if (heroList[i].transform.position.y != heroList[before].transform.position.y)
                //{
                //    heroList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                //}
             
            }

        }
    }
    public void UpdateNewPositionTeam()
    {
        for (int i = 0; i < heroPosition.Count; i++)
        {
            heroPosition[i] = heroList[i].position;
        }
    }
    public void UpadteEnabledScript()
    {
        for (int i = 0; i < heroPosition.Count; i++)
        {
            ScriptTurnONOFF(i);
        }
    }

    private void ScriptTurnONOFF(int i)
    {
        if (i == 0)
        {
            heroList[i].gameObject.GetComponent<PlayerController>().enabled = true;
            heroList[i].gameObject.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            heroList[i].gameObject.GetComponent<PlayerController>().enabled = false;
            heroList[i].gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void ResetListAndAddNew()
    {
        heroList = new List<Transform>();
        heroPosition = new List<Vector2>();
        heroSprite = new List<SpriteRenderer>();
        heroRotation = new List<float>();
        GetData();

    }
    public void ManageStatUI()
    {
        for (int i = 0; i < heroList.Count; i++)
        {
            if (i == 0)
            {
                heroList[i].GetComponent<PlayerUI>().arrowGroup.SetActive(true);
                heroList[i].GetComponent<PlayerUI>().statGroup.SetActive(false);
            }
            else
            {
                heroList[i].GetComponent<PlayerUI>().arrowGroup.SetActive(false);
                heroList[i].GetComponent<PlayerUI>().statGroup.SetActive(true);
            }
        }

    }
    public void RemovePlayerDie()
    {    

        List<Vector2> originPos = new List<Vector2>(heroPosition);
        List<Vector2> copy = new List<Vector2>();
        int lastIndex = originPos.Count - 1;
        for (int i = 0; i < originPos.Count; i++)
        {
            if (i < lastIndex)
            {
                copy.Add(originPos[i]);

            }

        }
        heroPosition = new List<Vector2>();
        foreach (var item in copy)
        {
            heroPosition.Add(item);
        }
        
        heroList[0].gameObject.SetActive(false);
        GameObject move = heroList[0].gameObject;
        move.transform.SetParent(playerDieTran);
        Debug.Log(move.name + "___Die");
        heroList.RemoveAt(0);

        for (int i = 0; i < heroList.Count; i++)
        {
            heroList[i].position = new Vector3();
            heroList[i].position = new Vector3(copy[i].x, copy[i].y, 0);
            ScriptTurnONOFF(i);
        }
    }
}