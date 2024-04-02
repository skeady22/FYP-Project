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
    [SerializeField] private GameObject earlyTimingImage;
    [SerializeField] private GameObject lateTimingImage;

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

    public void EarlyTimingUI(int timing)
    {
        // early = 0, perfect = 1, late = 2;
        switch (timing)
        {
            case 0:
                earlyTimingImage.SetActive(true);
                lateTimingImage.SetActive(false);
                break;
            case 1:
                earlyTimingImage.SetActive(false);
                lateTimingImage.SetActive(false);
                break;
            case 2:
                earlyTimingImage.SetActive(false);
                lateTimingImage.SetActive(true);
                break;
            default:
                break;
        }
    }
}
