using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource EnemyDieSound;
    public AudioSource PlayerDieSound;
    public Deli deli;
    private int count;
    public float revorder;
    public TextMeshProUGUI counterTMP;

    public List<Enemy> enemyPrefabs;
    public List<GameObject> enemySpawnPoints;

    public Animator BlackScreenAnimator;

    private List<Enemy> spawnedEnemies;

    public LineDrawer lineDrawer;
    private int last_count_spawn_enemy;

    private int count_enemy_kill;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        count = 0;
        last_count_spawn_enemy = count;
        count_enemy_kill = 0;
        counterTMP.text = "0";
        spawnedEnemies = new List<Enemy>();

        BlackScreenAnimator.SetTrigger("Hide");
    }
    public Enemy findMinDistPlayer()
    {
        Enemy resultEnemy;
        Debug.Log("Spawned enemies: " + spawnedEnemies.Count);
        if (spawnedEnemies.Count == 0)
        {
            resultEnemy = null;
            return resultEnemy;
        }
        resultEnemy = spawnedEnemies[0];
        for(int i = 1; i < spawnedEnemies.Count; i++)
        {
            if(resultEnemy.transform.position.y > spawnedEnemies[i].transform.position.y)
            {
                resultEnemy = spawnedEnemies[i];
            }
        }
        return resultEnemy;
    }
    private void OnEnable()
    {
        Deli.DeadAction += DieActionReact;
        Enemy.Die += DieEnemyRemove;
    }
    private void OnDisable()
    {
        Deli.DeadAction -= DieActionReact;
        Enemy.Die -= DieEnemyRemove;
    }
    private void DieEnemyRemove(Enemy enemy, int count)
    {
        EnemyDieSound.Play();
        spawnedEnemies.Remove(enemy);
        count_enemy_kill += count;

    }
    IEnumerator ShowBlackScreen()
    {
        BlackScreenAnimator.SetTrigger("Show");
        yield return new WaitForSeconds(0.5f);
        SceneLoad.Die_Load();
    }
    void DieActionReact()
    {
        PlayerDieSound.Play();
        lineDrawer.DisableDrawer();
        Debug.Log(count + count_enemy_kill);
        Binary.Record record = new Binary.Record(count + count_enemy_kill, count_enemy_kill, count);
        Binary.AppendBinaryFile(record, Binary.filename);
        StartCoroutine(ShowBlackScreen());
    }
    // Update is called once per frame
    void Update()
    {
        if(deli.transform.position.y * revorder > count)
        {
            count = Convert.ToInt32(deli.transform.position.y * revorder);
            counterTMP.text = (count + count_enemy_kill).ToString();
            if (UnityEngine.Random.Range(0, 2) == 1 && count - last_count_spawn_enemy > 10)
            {
                last_count_spawn_enemy = count;
                SpawnMonsters();
            }
        }
    }
    void SpawnMonsters()
    {
        GameObject spawnPoint;
        for (int i = 0; i < UnityEngine.Random.Range(0, 2); i++)
        {
            spawnPoint = enemySpawnPoints[UnityEngine.Random.Range(0, enemySpawnPoints.Count)];
            Enemy enemy = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Count)];
            enemy = Instantiate(enemy, spawnPoint.transform.position + new Vector3(0,0,1), Quaternion.identity);
            spawnedEnemies.Add(enemy);
            Debug.Log("REAL POSITION: " + enemy.gameObject.transform.position);
        }

    }
}
