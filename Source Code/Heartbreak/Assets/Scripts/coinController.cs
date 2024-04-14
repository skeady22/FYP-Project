using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class coinController : MonoBehaviour
    {
        [SerializeField] SceneController scene;

        // Use this for initialization
        void Start()
        {
            //transform.position += new Vector3(SceneController.scrollSpeed + 1.25f, 0, 0);
            //transform.position = new Vector3((transform.position.x * (SceneController.scrollSpeed + 1.25f)) - transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Scale(transform.position, new Vector3(SceneController.scrollSpeed + 1.25f, 1, 1)) - new Vector3(transform.position.x, 0, 0);
            //transform.position -= new Vector3(21, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollectCoin();
        }

        void CollectCoin()
        {
            gameObject.SetActive(false);
            Debug.Log("coin collected");
            scene.UpdateCoins();
        }
    }
}