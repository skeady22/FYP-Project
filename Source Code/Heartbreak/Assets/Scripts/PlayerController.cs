using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SceneController scene;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private InputController input;

    private PlayerInput inputs;
    private new Rigidbody2D rigidbody;
    public static int Health { get; private set; }
    private bool isGrounded;
    public static int Score { get; private set; }

    private Vector3 direction;

    private float[] lanes = new float[4];

    private void Awake()
    {
        Health = 100;
        Score = 0;

        // lanes for the 4 button controls
        lanes[0] = 3.5f;
        lanes[1] = 1.5f;
        lanes[2] = -0.5f;
        lanes[3] = -2.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        inputs = gameObject.GetComponent<PlayerInput>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        if (gameManager.one_button == true)
        {
            inputs.SwitchCurrentActionMap("One_Button");
            rigidbody.gravityScale = 5;
        }
        else
        {
            inputs.SwitchCurrentActionMap("Four_Button");
            rigidbody.gravityScale = 0;
        }

        // set isGrounded to true so the player can jump and get the scroll speed from the bpm speed * the multiplier that the player chooses
        isGrounded = true;
        scene.syncAudio.AddListener(LogPosition);
        

        direction = new Vector3((SceneController.scrollSpeed + 1.25f), 0, 0);
    }

    void FixedUpdate()
    {
        if (gameManager.one_button == true)
        {
            // move the player according to the scroll speed
            //transform.position += Vector3.right * (scrollSpeed * 2 + 1.25f) * Time.deltaTime;
            //transform.Translate((direction * (Time.deltaTime * (direction.magnitude/SceneController.secPerBeat)) * 2));
            //transform.Translate(direction * Time.deltaTime);
            transform.position += direction * Time.deltaTime * SceneController.beatPerSec;
        }
    }

    private void LateUpdate()
    {
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && context.started)
        {
            input.checkInputTiming(Time.time);
            isGrounded = false;
            StartCoroutine(Wait());
            transform.position += Vector3.up * 3.5f;
            jumpSound.Play();
        }
    }

    public void SwitchLane(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            input.checkInputTiming(Time.time);
            switch (context.action.name)
            {
                case "button_1":
                    transform.position = new Vector3(transform.position.x, lanes[0], transform.position.x);
                    break;
                case "button_2":
                    transform.position = new Vector3(transform.position.x, lanes[1], transform.position.x);
                    break;
                case "button_3":
                    transform.position = new Vector3(transform.position.x, lanes[2], transform.position.x);
                    break;
                case "button_4":
                    transform.position = new Vector3(transform.position.x, lanes[3], transform.position.x);
                    break;
                default:
                    break;
            }
        }
        
    }

    public static void LoseHP(int hp)
    {
        Health -= hp;
        Debug.Log(string.Format("Lost {0} health", hp));
    }

    void LogPosition()
    {
        Debug.Log("player position: " + transform.position);
    }

    public static void AddScore(int score)
    {
        PlayerController.Score += score;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(SceneController.secPerBeat);
        isGrounded = true;
    }
}
