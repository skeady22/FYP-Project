using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private SceneController scene;
    [SerializeField] private PlayerInput inputs;
    [SerializeField] private GameObject player;
    private List<float> intervals = new List<float>();
    private Vector3[] lanes;

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

        float delay = inputTime - Time.time;
        if (delay > 0)
        {
            // early
            Debug.Log("early input");
        }
        else if (delay < 0)
        {
            // late
            Debug.Log("late input");
        }
        else
        {
            // perfect
            Debug.Log("perfect input");
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
