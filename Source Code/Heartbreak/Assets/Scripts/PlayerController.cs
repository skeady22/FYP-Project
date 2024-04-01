using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private float scrollSpeed;
    public static int health { get; private set; }
    private bool isGrounded;
    private Ray ray;
    private RaycastHit hit;

    private Vector3 direction;

    private void Awake()
    {
        health = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(scrollSpeed + 1.25f, 0, 0);
        
        // set isGrounded to true so the player can jump and get the scroll speed from the bpm speed * the multiplier that the player chooses
        isGrounded = true;
        scrollSpeed = SceneController.scrollSpeed;

        ray = new Ray(new Vector3(0, transform.position.y+5f, 0), Vector3.down);
    }

    void FixedUpdate()
    {
        if (SceneController.settings.one_button == true)
        {
            // move the player according to the scroll speed
            //transform.position += Vector3.right * (scrollSpeed * 2 + 1.25f) * Time.deltaTime;
            transform.Translate((direction * (Time.deltaTime * (direction.magnitude/SceneController.secPerBeat)) * 2));
            ray.origin = new Vector3(0, transform.position.y + 5f, 0);
            Debug.Log("player posìtion: " + transform.position);
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(SceneController.secPerBeat);
        isGrounded = true;
    }
}
