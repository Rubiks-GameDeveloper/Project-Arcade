using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ForFunOnLevels
{
    public class Ladder : MonoBehaviour
    {
        public bool isPlayerLowerPosition;
        public GameObject upperExit;
        public GameObject lowerExit;
        
        public void PlayerLadderTeleportation(GameObject player)
        {
            if (isPlayerLowerPosition)
            {
                var position = upperExit.transform.position;
                player.transform.position = new Vector3(position.x, position.y, player.transform.position.z);
            }
            else
            {
                var position = lowerExit.transform.position;
                player.transform.position = new Vector3(position.x, position.y, player.transform.position.z);
            }
        }
    }
}
