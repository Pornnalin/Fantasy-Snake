using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum playerStage { MOVE, BATTLE, DIE }
    public playerStage currentPlayerStage;
    [Space]
    public List<Vector2> heroPosition = new List<Vector2>();
    public List<Transform> heroList = new List<Transform>();
    public List<SpriteRenderer> heroSprite = new List<SpriteRenderer>();
    [Space]
    public int amountKilled;
    private SpriteRenderer heroFirst;
    // Start is called before the first frame update
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
            heroList.Add(transform.GetChild(i));
            heroSprite.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
            ScriptTurnONOFF(i);

        }
        heroFirst = heroList[0].GetComponent<SpriteRenderer>();
    }
    public void UpdatePostionTeamWhenMove()
    {
        int childLength = this.transform.childCount - 1;
        for (int i = 0; i < childLength; i++)
        {
            int SkipZero = i + 1;
            heroList[SkipZero].transform.position = heroPosition[i];

        }
        heroPosition[0] = heroList[0].transform.position;

        //    //update last position
        UpdateNewPositionTeam();
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

}
