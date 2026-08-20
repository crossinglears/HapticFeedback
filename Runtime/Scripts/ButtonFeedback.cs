using UnityEngine;
using UnityEngine.UI;

namespace CrossingLears
{
    public class ButtonFeedback : MonoBehaviour
    {
#if UNITY_EDITOR
        void Reset()
        {
            button = GetComponent<Button>();
        }
#endif

        public Button button;
        public AudioClip audioClip;
        public HapticFeedbackStrength hapticFeedbackStrength;

        void Start()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        void OnDestroy()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        public void OnButtonClicked()
        {
            if (audioClip != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
                
                // AudioSource audioSource = GameObject.FindObjectOfType<AudioListener>().gameObject.AddComponent<AudioSource>();
                // audioSource.playOnAwake = false;
                // audioSource.spatialBlend = 0f; // 2D sound
                // audioSource.clip = audioClip;
                // audioSource.Play();
                // GameObject.Destroy(audioSource, audioClip.length);
            }
            PlayHapticFeedback(hapticFeedbackStrength);
        }

        public static void PlayHapticFeedback(HapticFeedbackStrength strength) // rename this method
        {
            switch (strength)
            {
                case HapticFeedbackStrength.LightFeedback:
                    HapticFeedback.HapticFeedback.LightFeedback();
                    break;
                case HapticFeedbackStrength.MediumFeedback:
                    HapticFeedback.HapticFeedback.MediumFeedback();
                    break;
                case HapticFeedbackStrength.HeavyFeedback:
                    HapticFeedback.HapticFeedback.HeavyFeedback();
                    break;
            }
        }

        public enum HapticFeedbackStrength
        {
            LightFeedback, MediumFeedback, HeavyFeedback
        }
    }
}
