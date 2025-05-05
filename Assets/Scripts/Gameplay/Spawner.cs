using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, IGameFlow
{
    [SerializeField] GameObject[] pipePrefabs;
    [SerializeField] float[] rate = { 0.3f, 0.7f };
    [SerializeField] float intervalTime = 2f;
    [Space]
    [SerializeField] float pipeSpeed;
    [SerializeField] Vector2 clampPosY; // x- minY, y- maxY
    public void GameOver()
    {
        isSpawn = false;
    }

    public void GamePause()
    {
        isSpawn = false;
    }

    public void GamePrepare()
    {
        isSpawn = false;
        time = intervalTime;
    }

    public void GameResume()
    {
        isSpawn = true;
    }

    public void GameStart()
    {
        isSpawn = true;
    }

    public void GameWin()
    {
        isSpawn = false;
    }
    public bool isSpawn = false;
    float time;
    // Update is called once per frame
    void Update()
    {
        if (!isSpawn) return;
        if (time >= intervalTime)
        {
            float ran = UnityEngine.Random.Range(0f, 1f);
            int index = 0;
            if (ran <= rate[0])
                index = 0;
            else
                index = 1;
            var p = Pooling.Instance.GetObj<Pipe>(pipePrefabs[index]);
            p.Setup(new Vector2(20, UnityEngine.Random.Range(clampPosY.x, clampPosY.y)), pipeSpeed);
            p.gameObject.SetActive(true);
            time = 0;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
