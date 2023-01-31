using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int roundNum;

    public List<GameObject> EnemyArray = new List<GameObject>();
    public List<GameObject> CyclopsArray = new List<GameObject>();
    public List<GameObject> BossArray = new List<GameObject>();
    private int totalWaveEnemies = 1;
    private int totalWaveCyclops = 0;
    private int count = 0;
    private bool once = true;
    private bool bossRound = false;
    private bool bossFirstSpawn = true;
    public GameObject playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckWaveOver())
        {
            if(once)
            {
                Debug.Log(roundNum);
                if (roundNum >= 4)
                {
                    totalWaveEnemies++;
                    totalWaveCyclops++;
                }
                else
                {
                    totalWaveEnemies++;
                }

                if ((roundNum % 10) == 0)
                {
                    bossRound = true;
                }
                else if(bossRound && (roundNum % 10) != 0)
                {
                    BossArray[0].GetComponent<Enemy>().active = false;
                    bossRound = false;
                }
                once = false;
                playerManager.GetComponent<PlayerResources>().WaveIncreased();
                roundNum++;

                //Check to see if array needs to expand/ add more enemies
                if(totalWaveEnemies > EnemyArray.Count || totalWaveCyclops > CyclopsArray.Count || bossRound)
                {
                    AddEnemies(roundNum);
                }

                StartCoroutine(spawnNextWave(roundNum));
            }            
        }
        else
        {
            once = true;
        }
    }

    IEnumerator spawnNextWave(int roundNum)
    {

        if (BossArray.Count != 0 && bossRound)
        {
            BossArray[0].GetComponent<Enemy>().active = true;
        }
        else if (!bossRound)
        {
            for (int i = 0; i < roundNum; i++)
            {
                yield return new WaitForSeconds(.5f);
                EnemyArray[i].GetComponent<Enemy>().PassParameters(roundNum, .2f, 0);
                EnemyArray[i].GetComponent<Enemy>().active = true;

                if (i >= 4)
                {
                    CyclopsArray[i - 4].GetComponent<Enemy>().PassParameters(2 * roundNum, .1f, 1);
                    CyclopsArray[i - 4].GetComponent<Enemy>().active = true;
                }

            }
        }
    }

    public bool CheckWaveOver()
    {
        foreach(GameObject e in EnemyArray)
        {
            if(e.GetComponent<Enemy>().active == true)
            {
                return false;
            }
        }

        foreach(GameObject c in CyclopsArray)
        {
            if(c.GetComponent<Enemy>().active == true)
            {
                return false;
            }
        }

        foreach(GameObject b in BossArray)
        {
            if(b.GetComponent<Enemy>().active == true)
            {
                return false;
            }
        }

        return true;
    }

    public void AddEnemies(int roundNum)
    {
        for(int i = 0; i < 10; i++)
        {
            if(roundNum >= 4)
            {
                var cyclops = Instantiate(enemyPrefab, new Vector3(-1000, 0, 0), Quaternion.identity);
                cyclops.GetComponent<Enemy>().PassParameters(2 * roundNum,.1f,1);
                CyclopsArray.Add(cyclops);
            }

            var enemy = Instantiate(enemyPrefab, new Vector3(-1000, 0, 0), Quaternion.identity);
            enemy.GetComponent<Enemy>().PassParameters(roundNum, .2f, 0);
            EnemyArray.Add(enemy);
        }

        if (bossRound && bossFirstSpawn)
        {
            var boss = Instantiate(enemyPrefab, new Vector3(-1000, 0, 0), Quaternion.identity);
            boss.GetComponent<Enemy>().PassParameters(5 * roundNum, .075f, 2);
            BossArray.Add(boss);
            bossFirstSpawn = false;
        }
        else if(bossRound)
        {
            BossArray[0].GetComponent<Enemy>().PassParameters(5 * roundNum, .075f, 2);
        }
    }
}
