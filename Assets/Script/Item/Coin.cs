using UnityEngine;

namespace Script.Item
{
    public class Coin : MonoBehaviour
    {
        private GameObject player;
        private float speed = 7f;

        void OnEnable()
        {
            player = GameObject.Find("Player");
        }

        void Update()
        {
            var step = speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
