using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystems : MonoBehaviour
{
    public enum battleStage { START, RANDOM, PLAYERTURN, MONSTERTURN, WON, LOST }
    public battleStage state;
    public bool isEven;
    public int rand;
    // Start is called before the first frame update
    void Start()
    {
        state = battleStage.START;
    }

    // Update is called once per frame
    public void RandomNumber()
    {
        int _rand = Random.Range(0, 11);
        rand = _rand;
        
    }
    private void Update()
    {
        if (rand % 2 == 0)
        {
            isEven = true;
        }
        else
        {
            isEven = false;
        }
    }
}
