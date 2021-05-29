using System.Collections;
using UnityEngine;

namespace BattleMage.Enemies
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] int maxHealth = 10;
        [SerializeField] Renderer objRenderer;
        [SerializeField] LayerMask playerLayer;


        //Status
        int currentHealth;
        float flashTimer = -1f;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        private void Update()
        {
            //Update renderer red
            if (flashTimer >= 0f)
            {
                flashTimer -= Time.deltaTime;
                if (flashTimer < 0f)
                {
                    objRenderer.material.color = Color.white;
                }
            }
        }

        public virtual void TakeDamage(int amount = 1)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                flashTimer = 0.1f;
                objRenderer.material.color = Color.red;
            }
        }



        void OnTriggerEnter(Collider other)
        {
            if (HitsPlayer(other))
            {
                other.GetComponent<PlayerManager>().TakeDamage(10);
                Destroy(gameObject);
            }
        }

        bool HitsPlayer(Collider collider) => playerLayer == (playerLayer | 1 << collider.gameObject.layer);
    }
}