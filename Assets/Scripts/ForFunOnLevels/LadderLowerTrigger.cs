using System;
using UnityEngine;

namespace ForFunOnLevels
{
    public class LadderLowerTrigger : MonoBehaviour
    {
        private Ladder _ladder;
        private GameObject _upDownUI;

        private void Start()
        {
            _ladder = transform.parent.GetComponent<Ladder>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            _ladder.isPlayerLowerPosition = true;
            _upDownUI = col.GetComponent<PlayerProgrammingTransformer>().ladderButton;
            _upDownUI.SetActive(true);
            col.GetComponent<PlayerProgrammingTransformer>().Ladder = gameObject.transform.parent.gameObject;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player")) _upDownUI.SetActive(false);
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            _ladder.isPlayerLowerPosition = true;
            _upDownUI = col.GetComponent<PlayerProgrammingTransformer>().ladderButton;
            _upDownUI.SetActive(true);
            col.GetComponent<PlayerProgrammingTransformer>().Ladder = gameObject.transform.parent.gameObject;
        }
    }
}
