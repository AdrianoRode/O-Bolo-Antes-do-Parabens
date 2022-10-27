using UnityEngine;
using DG.Tweening;

namespace Script.Item
{
    public class Drop : MonoBehaviour
    {
        private GameObject player;

        void OnEnable()
        {
            player = GameObject.Find("Player");
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            {
                transform.DOMove(player.transform.position,25f * Time.deltaTime);
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
