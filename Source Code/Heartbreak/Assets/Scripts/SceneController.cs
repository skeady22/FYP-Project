using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{
    [SerializeField] private new AudioSource audio;
    [SerializeField] private TextAsset settingJson;
    [SerializeField] private float bpm;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private GameObject gridObj;
    [SerializeField] float scrollMult;

    public UnityEvent syncAudio;
    public static Settings settings;

    public AudioSource Audio { get { return audio; } }
    public static float scrollSpeed { get; private set; }
    public static float beatPerSec { get; private set; }
    public static float secPerBeat { get; private set; }

    private static int collectedCoins = 0;
    private static int health;

    private void Awake()
    {
        // load JSON config file
        settings = JsonUtility.FromJson<Settings>(settingJson.text);
        Debug.Log("Config file loaded, one_button: " + settings.one_button);

        //beatPerSec is to get how many beats are in one second
        //secPerBeat is to get how many seconds (or fractions of seconds) are in one beat
        beatPerSec = bpm / 60f;
        secPerBeat = 60f / bpm;

        scrollSpeed = beatPerSec * scrollMult;
        Debug.Log("Scroll speed: " + scrollSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = PlayerController.health;

        gridObj.transform.localScale += new Vector3(scrollSpeed, 0, 0);

        // set score text
        ScoreTextUpdate(scoreText);
        HpTextUpdate(health);
    }

    public void UpdateCoins()
    {
        collectedCoins++;
        ScoreTextUpdate(scoreText);
    }

    void HpTextUpdate(int hp)
    {
        hpText.text = string.Format("{0}%", hp);
        Debug.Log("updated health text");
    }

    void ScoreTextUpdate(TMP_Text scoreText)
    {
        scoreText.text = ("Score: " + collectedCoins);
        Debug.Log("Added 1 coin");
    }
}
