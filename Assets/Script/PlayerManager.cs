using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Space]
    public List<Vector2> heroPosition = new List<Vector2>();
    public List<Transform> heroList = new List<Transform>();
    [Space]
    public int amountKilled;

    // Start is called before the first frame update
    void Start()
    {
        GetData();

    }
    private void GetData()
    {
        int childLength = this.transform.childCount;
        for (int i = 0; i < childLength; i++)
        {
            heroPosition.Add(transform.GetChild(i).transform.position);
            heroList.Add(transform.GetChild(i));
            ScriptTurnONOFF(i);

        }
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
        GetData();

    }

   
}
