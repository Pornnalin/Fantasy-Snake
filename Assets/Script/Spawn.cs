using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Spawn : MonoBehaviour
{
    public bool isSpwan = false;    
    public GameObject playerMainPrefab;
    public GameObject playerChildPrefab;
    public GameObject monsterPrefab;
    public GameObject obstaclePrefab;  
    [Space]
    [Header("Debug")]
    [SerializeField] int row;
    [SerializeField] int colum;
    [Header("SetPosition")]
    [SerializeField] int minX;
    [SerializeField] int minY;
    [SerializeField] int maxX;
    [SerializeField] int maxY;
    [SerializeField] private Transform startPos;
    [Space]
    [SerializeField] private Transform parentPlayer;    
    [Space]
    [SerializeField] private List<Vector2> randomPos;
    [SerializeField] private List<Vector2> setPostionSpawn;
   
    int countSpawn;

    #region DebugInEditor
    [ContextMenu("DebugPositionArea")]
    private void Area()
    {
        Vector2 vec = new Vector2();
        setPostionSpawn = new List<Vector2>();
        for (int i = 0; i < row; i++)
        {
            var xPod = startPos.position.x + i;
            for (int j = 0; j < colum; j++)
            {
                var YPod = startPos.position.y - j;
                vec = new Vector2(xPod, YPod);
                GameObject game = Instantiate(Resources.Load<GameObject>("Prefab/Floor"), vec, Quaternion.identity);
                setPostionSpawn.Add(vec);
                Debug.Log(vec);
            }
        }
        ShuffleList(setPostionSpawn);
    }
    #endregion
    private void Start()
    {
        
        StartSpawn();

    }  
    
    private Vector2 RandomXY()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        return new Vector2(x, y);
    }
    private void StartSpawn()
    {
        //ShuffleList(setPostionSpawn);
        GameObject main = Instantiate(playerMainPrefab, Vector2.zero, Quaternion.identity);
        main.transform.SetParent(parentPlayer, false);
        Debug.Log("<color=green>Spwan_Main_" + main.name + "</color>");

        for (int i = 0; i < GameMananger.instance.statInfo.startNumberPlayerChild; i++)
        {
            countSpawn++;
            GameObject playerAnother = Instantiate(playerChildPrefab, setPostionSpawn[i], Quaternion.identity);
            playerAnother.name = playerChildPrefab.name + "_" + countSpawn;
            PlayerManager.instance.PlayerNotInTeam.Add(playerAnother.transform);
            Debug.Log("<color=green>Spwan_New_" + playerAnother.name + "</color>");

        }
        int plusPlayer = GameMananger.instance.statInfo.startNumberPlayerChild;
        for (int j = 0; j < GameMananger.instance.statInfo.startNumberMonster; j++)
        {
            countSpawn++;
            GameObject monsAnother = Instantiate(monsterPrefab, setPostionSpawn[j + plusPlayer], Quaternion.identity);
            monsAnother.name = monsterPrefab.name + "_" + countSpawn;
            MonsterManager.instance.monsList.Add(monsAnother.transform);
            MonsterManager.instance.monsPosition.Add(monsAnother.transform.position);
            Debug.Log("<color=white>Spwan_New_" + monsAnother.name + "</color>");

        }
        int plusPlayerAndMons = GameMananger.instance.statInfo.startNumberPlayerChild + GameMananger.instance.statInfo.startNumberMonster;
        for (int i = 0; i < GameMananger.instance.statInfo.startNumberObstacle; i++)
        {
            countSpawn++;
            GameObject monsAnother = Instantiate(obstaclePrefab, setPostionSpawn[i + plusPlayerAndMons], Quaternion.identity);
            monsAnother.name = obstaclePrefab.name + "_" + countSpawn;
            MonsterManager.instance.monsList.Add(monsAnother.transform);
            MonsterManager.instance.monsPosition.Add(monsAnother.transform.position);           
            Debug.Log("<color=white>Spwan_New_" + monsAnother.name + "</color>");

        }
        // player can move
        PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.MOVE;       
        Debug.Log("<color=yellow>All_Character_Spawn</color>");
    }
    public void SpwanNewCharacterOrObject(int whichNumberCharOrOb, GameObject prefab, bool isSpwanPlayer)
    {        

        for (int i = 0; i < whichNumberCharOrOb; i++)
        {
            for (int j = 0; j < MonsterManager.instance.monsPosition.Count; j++)
            {
                for (int k = 0; k < PlayerManager.instance.playerPosition.Count; k++)
                {
                    for (int m = 0; m < PlayerManager.instance.PlayerNotInTeam.Count; m++)
                    {
                        do
                        {
                            RandomXY();
                        }
                        while (randomPos.Contains(RandomXY()) 
                        && randomPos.Contains(PlayerManager.instance.playerPosition[k])
                        && randomPos.Contains(MonsterManager.instance.monsPosition[j]) 
                        && randomPos.Contains(PlayerManager.instance.PlayerNotInTeam[m].position));
                        randomPos.Add(RandomXY());
                    }
                }
            }
            if (isSpwanPlayer)
            {
                countSpawn++;
                GameObject playerAnother = Instantiate(prefab, RandomXY(), Quaternion.identity);
                playerAnother.name = prefab.name + "_" + countSpawn;
                PlayerManager.instance.PlayerNotInTeam.Add(playerAnother.transform);               
                Debug.Log("<color=green>Spwan_New_" + playerAnother.name + "</color>");
            }
            else
            {
                countSpawn++;
                GameObject monsAnother = Instantiate(prefab, RandomXY(), Quaternion.identity);
                monsAnother.name = prefab.name + "_" + countSpawn;
                MonsterManager.instance.monsList.Add(monsAnother.transform);
                MonsterManager.instance.monsPosition.Add(monsAnother.transform.position);
                Debug.Log("<color=white>Spwan_New_" + monsAnother.name + "</color>");
                
            }
        }

        randomPos = new List<Vector2>();

    }
  
    private void ShuffleList(List<Vector2> t)
    {
        for (int i = 0; i < t.Count - 1; i++)
        {
            var temp = t[i];
            int rand = Random.Range(i, t.Count);
            t[i] = t[rand];
            t[rand] = temp;
        }

        Debug.Log("<color=yellow>ShuffleList</color>");
    }



}
