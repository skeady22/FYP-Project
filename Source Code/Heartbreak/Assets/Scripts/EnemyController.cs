using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private SceneController scene;
    [SerializeField] private Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        ScalePos();
        //transform.position = Vector3.Scale(transform.position, new Vector3(SceneController.scrollSpeed, 0, 0));
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position -= (Vector3.right * ((scrollSpeed * 2) + 1.25f)) * Time.deltaTime;
    }

    public void ScalePos()
    {
        float relativePos = transform.position.x - tilemap.transform.position.x;
        float newPosition = tilemap.transform.position.x + relativePos * (SceneController.scrollSpeed + 1);
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
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
        scene.ui.HpTextUpdate(PlayerController.Health);
    }
}
