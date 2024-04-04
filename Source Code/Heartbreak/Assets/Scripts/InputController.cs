using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private SceneController scene;
    [SerializeField] private PlayerInput inputs;
    [SerializeField] private GameObject player;
    [SerializeField] private List<float> intervals = new List<float>();
    [SerializeField] private float delay;

    private int perfectScore;
    private int partialScore;

    private float timings;
    private Vector3[] lanes = new Vector3[4];

    private void Awake()
    {
        lanes[0] = new Vector3(3, 0, 0);
        lanes[1] = new Vector3(1, 0, 0);
        lanes[2] = new Vector3(-1, 0, 0);
        lanes[3] = new Vector3(-3, 0, 0);

        perfectScore = 300;
        partialScore = 100;
    }

    void Start()
    {
        if (SceneController.settings.extra_timing == true)
        {
            timings = 0.3f;
        }
        else
        {
            timings = 0.1f;
        }

        scene.Audio.clip.LoadAudioData();
        Debug.Log("Audio loaded");
    }

    private void Update()
    {
        if (inputs.actions["button_0"].WasPressedThisFrame())
        {
            if (intervals.Count > 0)
            {
                checkInputTiming(inputs.actions["button_0"]);
            }
        }
    }

    private void checkInputTiming(InputAction input)
    {
        float inputTime;
        if (intervals[^1] >= (SceneController.secPerBeat/2))
        {
            inputTime = intervals[^1] + SceneController.secPerBeat;
        }
        else
        {
            inputTime = intervals[^1];
        }

        delay = inputTime - Time.time;
        if (delay > SceneController.secPerBeat/2 + timings)
        {
            // early
            Debug.Log("early input, delay = " + delay);
            scene.ui.EarlyTimingUI(0);
            PlayerController.AddScore(partialScore);
        }
        else if (delay < SceneController.secPerBeat / 2 - timings)
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
        yield return new WaitForSeconds(SceneController.settings.offset); // offset that the player sets
        Debug.Log("offset: " + SceneController.settings.offset);
        scene.Audio.Play();
        Debug.Log("Audio started");
        while (true)
        {
            scene.syncAudio.Invoke();
            intervals.Add(Time.time);
            Debug.Log("beat invoked");
            yield return new WaitForSeconds(SceneController.secPerBeat); // 60 divided by the BPM
        }
    }

}
