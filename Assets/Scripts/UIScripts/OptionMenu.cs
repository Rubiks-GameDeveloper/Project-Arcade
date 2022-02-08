using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Audio;

namespace UIScripts
{
    public class OptionMenu : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;

        public static int TargetFrameRate = 60;
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void Sound()
        {
            AudioListener.pause = !AudioListener.pause;
        }

        public void FPSIncrease()
        {
            if (Application.targetFrameRate == 60)
            {
                TargetFrameRate = 90;
                Application.targetFrameRate = 90;
            }
            else
            {
                TargetFrameRate = 60;
                Application.targetFrameRate = 60;
            }
        }
    }
}
