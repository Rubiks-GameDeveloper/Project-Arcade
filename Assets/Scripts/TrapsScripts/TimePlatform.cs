using UnityEngine;

namespace TrapsScripts
{
    public class TimePlatform : MonoBehaviour
    {
        [SerializeField] private CollapsingPlatform platform;
        private void OnCollisionEnter2D(Collision2D col)
        {
            StartCoroutine(platform.PlatformDestroy());
        }
    }
}
