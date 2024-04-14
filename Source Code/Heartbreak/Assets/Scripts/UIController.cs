using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject beatImage;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private SceneController scene;
    [SerializeField] private InputController input;
    [SerializeField] private GameObject earlyTimingImage;
    [SerializeField] private GameObject perfectTimingImage;
    [SerializeField] private GameObject lateTimingImage;
    [SerializeField] private GameObject startButton;
    [SerializeField] private TMP_Text scoreText;

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
        Time.timeScale = 0;
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

    public void StartGame()
    {
        startButton.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(input.AudioSync());
    }

    public void UpdateScoreText()
    {
        scoreText.text = ("Score: " + PlayerController.Score);
    }

    public void EarlyTimingUI(int timing)
    {
        // early = 0, perfect = 1, late = 2;
        switch (timing)
        {
            case 0:
                earlyTimingImage.SetActive(true);
                perfectTimingImage.SetActive(false);
                lateTimingImage.SetActive(false);
                break;
            case 1:
                earlyTimingImage.SetActive(false);
                perfectTimingImage.SetActive(true);
                lateTimingImage.SetActive(false);
                break;
            case 2:
                earlyTimingImage.SetActive(false);
                perfectTimingImage.SetActive(false);
                lateTimingImage.SetActive(true);
                break;
            default:
                break;
        }
    }
}
