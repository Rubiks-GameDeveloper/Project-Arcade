using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GlobalEventManager : MonoBehaviour
{
    public static readonly UnityEvent<GameObject> enemyAlarm = new UnityEvent<GameObject>();
    public static readonly UnityEvent enemyFalseAlarm;

    public static bool AlarmActive = false;
    public static void RaiseAlarm(GameObject player)
    {
        AlarmActive = true;
        enemyAlarm.Invoke(player);
    }
}
