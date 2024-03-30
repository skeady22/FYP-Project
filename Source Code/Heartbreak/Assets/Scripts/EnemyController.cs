using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private float scrollSpeed = SceneController.getScrollSpeed();

    // Start is called before the first frame update
    void Start()
    {
        transform.position += (Vector3.right * ((scrollSpeed * 2) + 1.25f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemy.SetActive(!enemy.activeSelf);
    }
}
