using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{
    public static class GameEvents
    {
        public delegate void TriggerDelegate();
        public static TriggerDelegate OnGameLose;
        public static TriggerDelegate OnGameWin;
    }
}
