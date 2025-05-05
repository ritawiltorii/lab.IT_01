using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFlow
{
    void GamePrepare();
    void GameStart();
    void GamePause();
    void GameResume();
    void GameOver();
    void GameWin();

}

public enum Game_State
{
    PREPARE,
    START,
    PAUSE,
    RESUME,
    OVER,
    WIN,
    NONE = -1
}
