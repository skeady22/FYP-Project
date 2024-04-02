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
    private Vector3[] lanes;
    [SerializeField] private float delay;

    enum Timing
    {
        early = 0,
        perfect = 1,
        late = 2
    }

    void Start()
    {
        scene.Audio.clip.LoadAudioData();
        Debug.Log("Audio loaded");
        StartCoroutine(AudioSync());
    }

    private void Update()
    {
        if (inputs.actions["button_0"].WasPressedThisFrame())
        {
            checkInputTiming(inputs.actions["button_0"]);
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
        if (delay > SceneController.secPerBeat/2)
        {
            // early
            Debug.Log("early input");
            scene.ui.EarlyTimingUI(0);
        }
        else if (delay < SceneController.secPerBeat / 2)
        {
            // late
            Debug.Log("late input");
            scene.ui.EarlyTimingUI(2);
        }
        else
        {
            // perfect
            Debug.Log("perfect input");
            scene.ui.EarlyTimingUI(1);
        }
            
    }

    IEnumerator AudioSync()
    {
        while (scene.Audio.clip.loadState != AudioDataLoadState.Loaded)
        {
            yield return null; // wait until the audio loads in
        }
        yield return new WaitForSeconds(SceneController.settings.offset); // offset that the player sets
        Debug.Log("offset: " + SceneController.settings.offset);
        while (true)
        {
            scene.syncAudio.Invoke();
            intervals.Add(Time.time);
            Debug.Log("beat invoked");
            yield return new WaitForSeconds(SceneController.secPerBeat); // 60 divided by the BPM
        }
    }

}
