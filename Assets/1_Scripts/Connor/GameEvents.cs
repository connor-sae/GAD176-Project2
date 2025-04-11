using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.Connor
{
    public static class GameEvents
    {
        public delegate void alertDelegate(Vector3 origin, float radius);
        public static alertDelegate AlertEnemies;
    }
}
