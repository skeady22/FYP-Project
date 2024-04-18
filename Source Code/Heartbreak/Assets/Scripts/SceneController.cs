using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{
    [SerializeField] private new AudioSource audio;
    [SerializeField] private AudioSource metronome;
    [SerializeField] private float bpm;
    [SerializeField] private GameObject gridObj;
    [SerializeField] float scrollMult;
    [SerializeField] public UIController ui;

    [SerializeField] public GameManager gameManager;

    public UnityEvent syncAudio;
    public static Settings settings;
    private string settingFile;
    private string settingText;

    public AudioSource Audio { get { return audio; } }
    public AudioSource Metronome { get { return metronome; } }
    public static float scrollSpeed { get; private set; }
    public static float beatPerSec { get; private set; }
    public static float secPerBeat { get; private set; }

    public static int collectedCoins { get; set; }

    private Vector3 direction;

    private void Awake()
    {
        collectedCoins = 0;
        Time.timeScale = 0;

        settingFile = Application.persistentDataPath + "/config.json";
        // load JSON config file
        settingText = File.ReadAllText(settingFile);
        settings = JsonUtility.FromJson<Settings>(settingText);
        Debug.Log("Config file loaded, one_button: " + settings.one_button);

        //beatPerSec is to get how many beats are in one second
        //secPerBeat is to get how many seconds (or fractions of seconds) are in one beat
        beatPerSec = bpm / 60f;
        secPerBeat = 60f / bpm;

        scrollMult = gameManager.scroll_mult;

        scrollSpeed = beatPerSec * scrollMult;
        Debug.Log("Scroll speed: " + scrollSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager.metronome)
        {
            metronome.mute = false;
        }

        gridObj.transform.localScale += new Vector3(scrollSpeed, 0, 0);

        direction = new Vector3(gridObj.transform.localScale.x, 0, 0);

        // set score text
        ui.CoinTextUpdate();
        ui.HpTextUpdate(PlayerController.Health);
        ui.UpdateScoreText();
    }

    private void FixedUpdate()
    {
        gridObj.transform.position -= direction * Time.deltaTime * beatPerSec;
    }

    public void UpdateCoins()
    {
        collectedCoins++;
        ui.CoinTextUpdate();
    }
}
