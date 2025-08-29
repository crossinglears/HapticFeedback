using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using System.Linq;
using UnityEngine.UI;
using CrossingLears;

namespace CrossingLearsEditor
{
    public class HapticFeedbackTab : CL_WindowTab
    {
        private const string AUDIO_KEY = "CrossingLears_HF_Audio";
        private const string ENUM_KEY = "CrossingLears_HF_Strength";

        public override string TabName => "Haptic Feedback";

        public override void DrawContent()
        {
            Button[] buttons = Selection.gameObjects.SelectMany(x => x.GetComponents<Button>()).ToArray();

            EditorGUILayout.LabelField("Buttons Count:", buttons.Length.ToString());

            EditorGUILayout.BeginVertical("helpbox");
            AudioClip newAudioClip = EditorGUILayout.ObjectField("Audio:", audioClip, typeof(AudioClip), false) as AudioClip;
            if (newAudioClip != audioClip)
            {
                audioClip = newAudioClip;
                EditorPrefs.SetString(AUDIO_KEY, AssetDatabase.GetAssetPath(audioClip));
            }

            ButtonFeedback.HapticFeedbackStrength newStrength =
                (ButtonFeedback.HapticFeedbackStrength)EditorGUILayout.EnumPopup("Haptic Feedback Strength", hapticFeedbackStrength);
            if (newStrength != hapticFeedbackStrength)
            {
                hapticFeedbackStrength = newStrength;
                EditorPrefs.SetInt(ENUM_KEY, (int)hapticFeedbackStrength);
            }
            EditorGUILayout.EndVertical();

            bool au = false, hf = false;
            GUILayout.BeginHorizontal();
            au = GUILayout.Button("Apply AudioClip");
            hf = GUILayout.Button("Apply Haptic");
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Apply to all Selected Buttons"))
            {
                au = hf = true;
            }

            if (au || hf)
            {
                foreach (GameObject item in Selection.gameObjects)
                {
                    ButtonFeedback bf = item.GetComponent<ButtonFeedback>() ?? item.AddComponent<ButtonFeedback>();
                    if (au)
                    {
                        bf.audioClip = audioClip;
                    }
                    if (hf)
                    {
                        bf.hapticFeedbackStrength = hapticFeedbackStrength;
                    }
                }
            }
        }

        [SerializeReference] public AudioClip audioClip;
        [SerializeReference] public ButtonFeedback.HapticFeedbackStrength hapticFeedbackStrength;

        public override void OnEnable()
        {
            base.OnEnable();
            string path = EditorPrefs.GetString(AUDIO_KEY, string.Empty);
            if (!string.IsNullOrEmpty(path))
            {
                audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
            }

            hapticFeedbackStrength = (ButtonFeedback.HapticFeedbackStrength)EditorPrefs.GetInt(ENUM_KEY, 0);
        }
    }
}
