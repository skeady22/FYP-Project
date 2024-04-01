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

    public static float scrollSpeed { get; private set; }
    public static float beatPerSec { get; private set; }
    public static float secPerBeat { get; private set; }
    private static int collectedCoins = 0;

    public UnityEvent syncAudio;
    public static Settings settings;

    // Start is called before the first frame update
    void Start()
    {
        //beatPerSec is to get how many beats are in one second
        //secPerBeat is to get how many seconds (or fractions of seconds) are in one beat
        beatPerSec = bpm / 60f;
        secPerBeat = 60f / bpm;

        scrollSpeed = beatPerSec * scrollMult;
        Debug.Log("Scroll speed: " + scrollSpeed);

        gridObj.transform.localScale += new Vector3(scrollSpeed, 0, 0);

        // load JSON config file
        settings = JsonUtility.FromJson<Settings>(settingJson.text);
        Debug.Log("Config file loaded, one_button: " + settings.one_button);

        // set score text
        scoreTextUpdate(scoreText);
        hpTextUpdate(100);

        StartCoroutine(AudioSync());
    }

    public void updateCoins()
    {
        collectedCoins++;
        scoreTextUpdate(scoreText);
    }

    void hpTextUpdate(int hp)
    {
        hpText.text = string.Format("{0}%", hp);
    }

    void scoreTextUpdate(TMP_Text scoreText)
    {
        scoreText.text = ("Score: " + collectedCoins);
    }

    void InvokeSyncAudio()
    {
        syncAudio.Invoke();
        Debug.Log("beat invoked");
    }

    // Coroutine for syncing the audio with the game
    IEnumerator AudioSync()
    {
        while (audio.clip.loadState == AudioDataLoadState.Unloaded)
        {
            yield return null; // wait until the audio loads in
        }
        Debug.Log("audio loaded");
        yield return new WaitForSeconds(settings.offset); // offset that the player sets
        Debug.Log("offset: " + settings.offset);
        while (true)
        {
            syncAudio.Invoke();
            Debug.Log("beat invoked");
            yield return new WaitForSeconds(secPerBeat); // 60 divided by the BPM
        }
    }
}
