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

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collectCoin();
        }

        void collectCoin()
        {
            Debug.Log("coin collected");
            scene.updateCoins();
        }
    }
}