using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private SceneController scene;
    [SerializeField] public PlayerInput inputs;
    [SerializeField] private GameObject player;
    [SerializeField] private List<float> intervals;
    [SerializeField] private GameManager gameManager;

    private int perfectScore;
    private int partialScore;

    private float timings;

    private void Awake()
    {
        intervals = new();

        // points for when an input is hit correctly or when its early/late
        perfectScore = 300;
        partialScore = 100;
    }

    void Start()
    {
        // timing windows for the early/late input
        if (gameManager.extra_timing == true)
        {
            timings = 0.2f;
        }
        else
        {
            timings = 0.1f;
        }

        scene.Audio.clip.LoadAudioData();
        scene.Metronome.clip.LoadAudioData();
        Debug.Log("Audio loaded");
    }

    private void Update()
    {
        if (inputs.actions["button_0"].WasPressedThisFrame())
        {
            if (intervals.Count > 0)
            {
                //checkInputTiming(Time.time);
            }
        }
    }

    public void checkInputTiming(float inputTime)
    {
        //intervalTime finds either the last interval or the next interval
        float intervalTime;
        float delay;

        intervalTime = (intervals[^1] + (intervals[^1] + SceneController.secPerBeat)) / 2;

        // delay is the time between when the input was pressed and the current time
        delay = (intervalTime + gameManager.offset) - inputTime;
        Debug.Log(string.Format("interval time: {0}, input time: {1}, delay: {2}", intervalTime, inputTime, delay));

        if (delay < 0 - timings + gameManager.offset)
        {
            // early
            Debug.Log("early input, delay = " + delay);
            scene.ui.EarlyTimingUI(0);
            PlayerController.AddScore(partialScore);
        }
        else if (delay > timings + gameManager.offset)
        {
            // late
            Debug.Log("late input, delay = " + delay);
            scene.ui.EarlyTimingUI(2);
            PlayerController.AddScore(partialScore);
        }
        else
        {
            // perfect
            Debug.Log("perfect input, delay = " + delay);
            scene.ui.EarlyTimingUI(1);
            PlayerController.AddScore(perfectScore);
        }

        scene.ui.UpdateScoreText();
    }

    public IEnumerator AudioSync()
    {
        while (scene.Audio.clip.loadState != AudioDataLoadState.Loaded)
        {
            yield return null; // wait until the audio loads in
        }

        Debug.Log("offset: " + gameManager.offset);
        yield return new WaitForSeconds(gameManager.offset); // offset that the player sets

        scene.Audio.Play();
        scene.Metronome.Play();
        Debug.Log("Audio started");
        StartCoroutine(scene.EndLevel());
        while (true)
        {
            scene.syncAudio.Invoke();
            intervals.Add(Time.time);
            Debug.Log("beat invoked");
            yield return new WaitForSeconds(SceneController.secPerBeat);
        }
    }

}
