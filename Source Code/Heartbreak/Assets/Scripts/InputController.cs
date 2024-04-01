using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private SceneController scene;

    [SerializeField] private InputActionAsset inputs;
    [SerializeField] private GameObject player;
    private Vector3[] lanes;

    void Start()
    {
        scene = gameObject.GetComponent<SceneController>();
        //StartCoroutine(AudioSync());
    }

    void checkInputTiming(InputAction input)
    {
        //check if its early or late
    }

    IEnumerator AudioSync()
    {
        while (scene.GetComponent<AudioSource>().clip.loadState == AudioDataLoadState.Unloaded)
        {
            yield return null; // wait until the audio loads in
        }
        yield return new WaitForSeconds(SceneController.settings.offset); // offset that the player sets
        Debug.Log("offset: " + SceneController.settings.offset);
        while (true)
        {
            scene.syncAudio.Invoke();
            Debug.Log("beat invoked");
            yield return new WaitForSeconds(SceneController.secPerBeat); // 60 divided by the BPM
        }
    }

}
