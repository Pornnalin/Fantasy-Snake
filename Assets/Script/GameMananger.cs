using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMananger : MonoBehaviour
{
    public static GameMananger instance;
    public StatInfo statInfo;
    [Header("GamePad")]
    public bool isGamePadDetect;
    [Space]
    [Header("Switch")]
    public bool isSwtichF_To_S = false;
    public bool isSwtichL_To_F = false;
    public bool canPress = true;
    [Space]
    [Header("Spawn")]
    public bool isPlayerSpwan;
    int amountPlayer;
    int amountMonster;
    public bool isSpwanDone = false;
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
        Debug.Log("prepearData");
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        isGamePadDetect = Gamepad.current == null ? false : true;

    }
    public int ChanceSpawnPlayer()
    {
        int rand = Random.Range(statInfo.amountPlayerSpawanMin, statInfo.anmountPlayerSpawanMax);

        return rand;
    }
    public int ChanceSpawnMonster()
    {
        int rand = Random.Range(statInfo.amountMonsterSpawanMin, statInfo.amountMonsterSpawanMax);

       
        return rand;
    }

    public void ResetGame(int index)
    {
        SceneManager.LoadScene(index);
    }
}

[System.Serializable]
public class StatInfo
{
    [Range(1, 3)]
    public int startNumberPlayerChild;
    [Space]
    [Range(1, 5)]
    public int startNumberMonster;
    [Space]
    [Range(1, 5)]
    public int startNumberObstacle;
    [Space]
    [Range(1, 10), Tooltip("min =1,max=10")]
    public int minPlayerAttack, maxPlayerAttack, minMonsterAttack, maxMonsterrAttack;
    [Space]
    [Range(1, 20), Tooltip("min =1,max=20")]
    public int minPlayerHeart, maxPlayerHeart, minMonsterHeart, maxMonsterHeart;
    [Space]    
    public float growing;
    [Space]
    [Tooltip("chance")]
    [Range(1, 5)]
    public int amountPlayerSpawanMin, anmountPlayerSpawanMax, amountMonsterSpawanMin, amountMonsterSpawanMax;
}
