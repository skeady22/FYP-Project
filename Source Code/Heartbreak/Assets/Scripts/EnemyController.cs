using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private float scrollSpeed = SceneController.scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position -= (Vector3.right * ((scrollSpeed * 2) + 1.25f)) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHit();
    }

    public void EnemyHit()
    {
        enemy.SetActive(!enemy.activeSelf);
    }
}
