using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int bpm;
    [SerializeField] private AudioSource audioTrack;
    [SerializeField] private TMP_Text scoreText;
    static float beatPerSec;
    static float secPerBeat;
    static int collectedCoins;

    // Start is called before the first frame update
    void Start()
    {
        /*
            beatPerSec is to get how many beats are in one second
            secPerBeat is to get how many seconds (or fractions of seconds) are in one beat
         */

        beatPerSec = bpm / 60f;
        secPerBeat = 60f / bpm;

        // set score text
        collectedCoins = 0;
        scoreTextUpdate(scoreText);
    }

    public void updateCoins()
    {
        collectedCoins++;
        scoreTextUpdate(scoreText);
    }

    void scoreTextUpdate(TMP_Text scoreText)
    {
        scoreText.text = ("Score: " + collectedCoins);
    }

    // getters for the beatPerSec and secPerBeat variables for other scripts
    public static float getBeatPerSec()
    {
        return beatPerSec;
    }

    public static float getSecPerBeat()
    {
        return secPerBeat;
    }

}
