using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{
    [SerializeField] private new AudioSource audio;
    [SerializeField] private float bpm;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private GameObject gridObj;
    [SerializeField] float scrollMult;
    [SerializeField] public UIController ui;

    public UnityEvent syncAudio;
    public static Settings settings;
    private string settingFile;
    private string settingText;

    public AudioSource Audio { get { return audio; } }
    public static float scrollSpeed { get; private set; }
    public static float beatPerSec { get; private set; }
    public static float secPerBeat { get; private set; }

    private static int collectedCoins = 0;

    private void Awake()
    {
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

        scrollMult = settings.scroll_mult;

        scrollSpeed = beatPerSec * scrollMult;
        Debug.Log("Scroll speed: " + scrollSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        gridObj.transform.localScale += new Vector3(scrollSpeed, 0, 0);

        // set score text
        CoinTextUpdate(coinText);
        HpTextUpdate(PlayerController.Health);
        ui.UpdateScoreText();
    }

    public void UpdateCoins()
    {
        collectedCoins++;
        CoinTextUpdate(coinText);
    }

    public void HpTextUpdate(int hp)
    {
        hpText.text = string.Format("{0}%", hp);
        Debug.Log("updated health text");
    }

    void CoinTextUpdate(TMP_Text scoreText)
    {
        scoreText.text = ("Coins: " + collectedCoins);
        Debug.Log("Added 1 coin");
    }
}
