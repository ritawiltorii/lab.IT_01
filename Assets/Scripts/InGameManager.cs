using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InGameManager : MonoBehaviour, IGameFlow
{
    public static InGameManager Instance = null;

    [SerializeField]private int point;
    public int Point => point;
    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        AddSubscriber(playerController);
        AddSubscriber(spawner);
        GamePrepare();
    }
    #region Simple Observer
    List<IGameFlow> subcriber = new List<IGameFlow>();

    public void AddSubscriber(IGameFlow listener)
    {
        if(!subcriber.Contains(listener))
            subcriber.Add(listener);
    }
    public void RemoveSubcriber(IGameFlow listener)
    {
        subcriber.Remove(listener);
    }
    #endregion

    [SerializeField] PlayerController playerController;
    [SerializeField] Spawner spawner;
    public void GameOver()
    {
        subcriber.ForEach(x=>x.GameOver());
        Time.timeScale = 0;
    }

    public void GamePause()
    {
        subcriber.ForEach(x => x.GamePause());
        Time.timeScale = 0;
    }

    public void GamePrepare()
    {
        subcriber.ForEach(x => x.GamePrepare());
        point = 0;
    }

    public void GameResume()
    {
        subcriber.ForEach(x => x.GameResume());
        Time.timeScale = 1;
    }

    public void GameStart()
    {
        subcriber.ForEach(x => x.GameStart());
        Time.timeScale = 1;
    }

    public void GameWin()
    {
        subcriber.ForEach(x => x.GameWin());
        Time.timeScale = 0;
    }

    public void AddPoint(int value = 1)
    {
        point += value;
    }
}
