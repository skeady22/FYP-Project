using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class coinController : MonoBehaviour
    {
        [SerializeField] GameObject coin;
        SceneController scene = new SceneController();

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
            coin.SetActive(false);
            scene.updateCoins();
        }

        void collectCoin()
        {
            coin.SetActive(false);
            scene.updateCoins();
        }
    }
}