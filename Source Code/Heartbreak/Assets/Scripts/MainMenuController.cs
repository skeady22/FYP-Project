using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject mainUI;
        [SerializeField] private GameObject optionsUI;
        [SerializeField] private TextAsset settingJson;
        [SerializeField] private Slider masterVol;
        [SerializeField] private Slider musicVol;
        [SerializeField] private Slider effectVol;

        private static Settings settings;
        private string settingFile;
        private string settingText;

        private void Awake()
        {
            settingFile = Application.persistentDataPath + "/config.json";
        }


        // Use this for initialization
        void Start()
        {
            // load JSON config file
            settingText = File.ReadAllText(settingFile);
            settings = JsonUtility.FromJson<Settings>(settingText);
            Debug.Log("Config file loaded, one_button: " + settings.one_button);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayGame()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        public void OptionMenu()
        {
            mainUI.SetActive(false);
            optionsUI.SetActive(true);
        }

        public void BackButton()
        {
            mainUI.SetActive(true);
            optionsUI.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quit game");
        }

        public void MasterVol()
        {
            settings.master_vol = (int)masterVol.value;
            Debug.Log("set master vol to " + settings.master_vol);
        }

        public void MusicVol()
        {
            settings.music_vol = (int)musicVol.value;
            Debug.Log("set music vol to " + settings.music_vol);
        }

        public void EffectVol()
        {
            settings.effect_vol = (int)effectVol.value;
            Debug.Log("set effect vol to " + settings.effect_vol);
        }

        public void SetVisuals()
        {
            settings.visuals = !settings.visuals;
            Debug.Log("set visuals to " + settings.visuals);
        }

        public void SetNoFail()
        {
            settings.no_fail = !settings.no_fail;
            Debug.Log("set nofail to " + settings.no_fail);
        }

        public void SetMetronome()
        {
            settings.metronome = !settings.metronome;
            Debug.Log("set metronome to " + settings.metronome);
        }

        public void SetTiming()
        {
            settings.extra_timing = !settings.extra_timing;
            Debug.Log("set extra_timing to" + settings.extra_timing);
        }

        public void SaveChanges()
        {
            settingText = JsonUtility.ToJson(settings, true);
            File.WriteAllText(settingFile, settingText);
            Debug.Log("saved changes to json file");
        }
    }
}