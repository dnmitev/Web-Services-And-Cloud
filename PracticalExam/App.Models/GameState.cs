namespace App.Models
{
    using System;
    using System.Linq;

    public enum GameState
    {
        WaitingForOpponent,
        RedInTurn,
        BlueInTurn,
        Finished
    }
}