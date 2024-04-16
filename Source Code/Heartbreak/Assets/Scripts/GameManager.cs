using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/SceneManagerObject", order = 1)]
    public class GameManager : ScriptableObject
    {
        [Header("Settings")]
        public bool one_button;
        public float offset;
        public int scroll_mult;
        public int master_vol;
        public int music_vol;
        public int effect_vol;
        public bool no_fail;
        public bool visuals;
        public bool metronome;
        public bool extra_timing;

        [Header("High Scores")]
        public int[] levelScores;

        [Header("Items")]
        public bool[] items;
    }
}