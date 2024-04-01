using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private Vector3 rayOrigin;
    private Vector3 rayDirection;
    private float rayDistance = 0.1f;
    private RaycastHit rayHit;

    // Start is called before the first frame update
    void Start()
    {
        //rayOrigin = new Vector3();
        //rayDirection = new Vector3();
    }

    private void Update()
    {
        if (Physics.Raycast(rayOrigin, rayDirection, out rayHit, rayDistance))
        {
            if (rayHit.collider.gameObject != gameObject)
            {
                EnemyHit();
                Debug.Log("play hit enemy ray");
            }
        }
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
    }
}
