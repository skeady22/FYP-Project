using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SceneController scene;
    public static int health { get; private set; }
    private bool isGrounded;

    private Vector3 direction;

    private void Awake()
    {
        health = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        // set isGrounded to true so the player can jump and get the scroll speed from the bpm speed * the multiplier that the player chooses
        isGrounded = true;
        scene.syncAudio.AddListener(LogPosition);

        direction = new Vector3((SceneController.scrollSpeed + 1.25f), 0, 0);
    }

    void FixedUpdate()
    {
        if (SceneController.settings.one_button == true)
        {
            // move the player according to the scroll speed
            //transform.position += Vector3.right * (scrollSpeed * 2 + 1.25f) * Time.deltaTime;
            //transform.Translate((direction * (Time.deltaTime * (direction.magnitude/SceneController.secPerBeat)) * 2));
            //transform.Translate(direction * Time.deltaTime);
            transform.position += direction * Time.deltaTime * SceneController.beatPerSec;


        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            StartCoroutine(Wait());
            transform.position += Vector3.up * 3.5f;            
        }
    }

    public static void LoseHP(int hp)
    {
        health -= hp;
        Debug.Log(string.Format("Lost {0} health", hp));
    }

    void LogPosition()
    {
        Debug.Log("player pos�tion: " + transform.position);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(SceneController.secPerBeat);
        isGrounded = true;
    }
}
