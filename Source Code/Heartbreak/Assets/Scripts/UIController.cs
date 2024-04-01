using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject beatImage;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private SceneController scene;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Beat(beatImage);
        scene.syncAudio.AddListener(ActivateImage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ActivateImage()
    {
        Debug.Log("image activated");
        beatImage.SetActive(!beatImage.activeSelf);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void PauseGame()
    {

    }

    public void OptionMenu()
    {
        mainUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void BackButton()
    {
        mainUI.SetActive(true);
        optionsUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
