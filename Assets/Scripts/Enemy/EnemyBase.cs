using System.Collections;
using UnityEngine;

namespace BattleMage.Enemies
{
    /// <summary>
    /// Abstract Base Class that manages the enemy
    /// </summary>
    public abstract class EnemyBase : MonoBehaviour
    {
        const float rotationMaxDelta = 90f;
        const float gravityAcceleration = 10f;
        const float moveSpeed = 2f;

        [SerializeField] int maxHealth = 10;
        [SerializeField] Renderer objRenderer;
        [SerializeField] LayerMask playerLayer;
        [SerializeField] Canvas healthBarCanvas;
        [SerializeField] WorldSpaceHealthBar healthBar;

        //Status
        float liftTargetElevation;
        float currentGravity;
        bool inLiftMode;
        int currentHealth;
        float flashTimer = -1f;
        Vector3 liftRotation;


        private void Awake()
        {
            currentHealth = maxHealth;
        }

        private void Start()
        {
            //healthBarCanvas.worldCamera = RigManager.mainCamera;
            healthBarCanvas.worldCamera = Camera.main;
        }

        private void Update()
        {
            // if the player is dead I destroy the enemies
            if (PlayerManager.PlayerDead)
            {
                Destroy(gameObject);
            }

            // if the enemy got hit by the gravity spell
            if (inLiftMode)
            {
                LiftModeUpdate();
            }
            else
            {
                NormalModeUpdate();
            }

            // flash red when you damage the player
            UpdateDamageFlash();
        }

        /// <summary>
        /// When the enemy hit something
        /// </summary>
        /// <param name="other">the other collider that got hit</param>
        void OnTriggerEnter(Collider other)
        {
            if (HitsPlayer(other))
            {
                other.GetComponent<PlayerManager>().TakeDamage(10);
                Destroy(gameObject);
            }
        }

        #region Public

        /// <summary>
        /// When hit by the gravity spell, disable the gravity and float upwards
        /// </summary>
        public void EnterLiftMode()
        {
            currentGravity = 0f;
            inLiftMode = true;

            liftTargetElevation = Random.Range(2f, 30f);
            objRenderer.material.color = Color.blue;
            liftRotation = new Vector3(
                Random.Range(-100f, 100f),
                Random.Range(-100f, 100f),
                Random.Range(-100f, 100f));
        }

        /// <summary>
        /// Reset to the normal gravity
        /// </summary>
        public void ExitLiftMode()
        {
            currentGravity = 0f;
            inLiftMode = false;
            objRenderer.material.color = Color.white;
        }


        /// <summary>
        /// When the player hit the enemy
        /// </summary>
        /// <param name="amount">amount of damage taken</param>
        public virtual void TakeDamage(int amount = 1)
        {
            currentHealth -= amount;
            healthBar.SetPercentage((float)currentHealth / maxHealth);
            // if the health reach 0, i destroy the object
            if (currentHealth <= 0)
            {
                GameManager.Instance.IncreaseKill();
                Destroy(gameObject);
            }
            else
            {
                flashTimer = 0.1f;
                objRenderer.material.color = Color.red;
            }
        }
        #endregion

        /// <summary>
        /// Function that keeps lifting the object when in LiftMode
        /// </summary>
        void LiftModeUpdate ()
        {
            if (!ReachedLiftElevation)
            {
                currentGravity += gravityAcceleration * Time.deltaTime;
            }
            else
            {
                currentGravity = Mathf.Lerp(currentGravity, 0f, 0.05f);
            }

            transform.Translate(Vector3.up * currentGravity * Time.deltaTime, Space.World);

            //Random rotation
            transform.Rotate(liftRotation * Time.deltaTime);
        }

        /// <summary>
        /// Function that manages the AI of the enemy in the normal condition
        /// </summary>
        void NormalModeUpdate()
        {
            if (OnGround)
            {
                //Rotate towards player
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-transform.position, Vector3.up), rotationMaxDelta * Time.deltaTime);

                Debug.DrawRay(transform.position, transform.forward, Color.red);
                transform.Translate(transform.forward * Time.deltaTime * moveSpeed, Space.World);
                //transform.Translate(-transform.position * Time.deltaTime * moveSpeed);
            }
            else //Move towards ground
            {
                //Gravity
                currentGravity += gravityAcceleration * Time.deltaTime;
                transform.Translate(-Vector3.up * currentGravity * Time.deltaTime, Space.World);

                if (OnGround)
                {
                    Vector3 p = transform.position;
                    p.y = 0f;
                    transform.position = p;
                }
            }
        }

        #region Util

        /// <summary>
        /// Red flash on the screen
        /// </summary>
        void UpdateDamageFlash()
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

        /// <summary>
        /// Check the layer if i hit the player
        /// </summary>
        /// <param name="collider">the collider that got hit</param>
        bool HitsPlayer(Collider collider) => playerLayer == (playerLayer | 1 << collider.gameObject.layer);

        bool OnGround => transform.position.y <= 0;
        bool ReachedLiftElevation => transform.position.y > liftTargetElevation;

        #endregion
    }
}