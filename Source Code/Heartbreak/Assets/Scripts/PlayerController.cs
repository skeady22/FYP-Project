using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private float scrollSpeed;
    private int health = 100;
    private bool isGrounded;
    private Ray ray;
    private RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        // set isGrounded to true so the player can jump and get the scroll speed from the bpm speed * the multiplier that the player chooses
        isGrounded = true;
        scrollSpeed = SceneController.getScrollSpeed();

        ray = new Ray(new Vector3(0, transform.position.y+5f, 0), Vector3.down);
    }

    void FixedUpdate()
    {
        // move the player according to the scroll speed
        transform.position += Vector3.right * (scrollSpeed*2+1.25f) * Time.deltaTime;
        ray.origin = new Vector3(0, transform.position.y + 5f, 0);
        Debug.Log("player pos�tion: " + transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.)
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

    public void loseHP(int hp)
    {
        health -= hp;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(SceneController.getSecPerBeat());
        isGrounded = true;
    }
}
