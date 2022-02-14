using System.Collections;
using UnityEngine;

namespace TrapsScripts
{
    public class CollapsingPlatform : MonoBehaviour
    {
        [SerializeField] private float destroyTime;
        [SerializeField] private float recoveryDuration;

        private GameObject _platform;

        private void Start()
        {
           _platform = transform.GetChild(0).gameObject;
        }
        public IEnumerator PlatformDestroy()
        {
            yield return new WaitForSeconds(destroyTime);
            _platform.SetActive(false);
            StartCoroutine(PlatformRecovery());
        }
        private IEnumerator PlatformRecovery()
        {
            yield return new WaitForSeconds(recoveryDuration);
            _platform.SetActive(true);
        }
    }
}
