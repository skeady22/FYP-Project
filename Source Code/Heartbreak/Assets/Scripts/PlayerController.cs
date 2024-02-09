using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float scrollMult;
    float scrollSpeed;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        // set isGrounded to true so the player can jump and get the scroll speed from the bpm speed * the multiplier that the player chooses
        isGrounded = true;
        scrollSpeed = SceneController.getBeatPerSec() * scrollMult;
    }

    // Update is called once per frame
    void Update()
    {
        // move the player according to the scroll speed
        transform.position += Vector3.right * scrollSpeed * Time.deltaTime;
    }

    public void jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            StartCoroutine(Wait());
            transform.position += Vector3.up * 3f;            
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(SceneController.getSecPerBeat());
        isGrounded = true;
    }
}
