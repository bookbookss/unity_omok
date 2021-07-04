using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum GameState
{
    ReadyState,
    RandomTurnSettingState,
    StartState,
    DisconnectState,
    GameOverState
}

public enum TurnState
{
    ServerFirstState,
    ClientFirstState
}
public enum WinState
{
    Black,
    White,
    Null
}