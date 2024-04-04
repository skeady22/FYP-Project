using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private SceneController scene;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.Scale(transform.position, new Vector3(SceneController.scrollSpeed + 1.25f, 0, 0));
        transform.position -= new Vector3(21, 0, 0);
    }

    private void Update()
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
        Debug.Log("Hit enemy");
        enemy.SetActive(!enemy.activeSelf);
        PlayerController.LoseHP(10);
        scene.HpTextUpdate(10);
    }
}
