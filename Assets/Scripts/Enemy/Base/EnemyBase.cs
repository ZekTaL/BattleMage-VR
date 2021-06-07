using System.Collections;
using UnityEngine;

namespace BattleMage.Enemies
{
    public abstract class EnemyBase : MonoBehaviour
    {
        #region Fields and Mono
        const float rotationMaxDelta = 90f;
        const float gravityAcceleration = 100f;
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


        void OnTriggerEnter(Collider other)
        {
            if (HitsPlayer(other))
            {
                other.GetComponent<PlayerManager>().TakeDamage(10);
                Destroy(gameObject);
            }
        }

        //private void OnGUI()
        //{
        //    GUI.Label(new Rect(20, 20, 200, 20), "inLiftMode: " + inLiftMode);
        //    GUI.Label(new Rect(20, 40, 200, 20), "ReachedLiftElevation: " + ReachedLiftElevation);
        //    GUI.Label(new Rect(20, 60, 200, 20), "OnGround: " + OnGround);
        //}

        private void Update()
        {
            if (inLiftMode)
            {
                LiftModeUpdate();
            }
            else
            {
                NormalModeUpdate();
            }

            UpdateDamageFlash();
        }
        #endregion

        #region Public
        public void EnterLiftMode()
        {
            currentGravity = 0f;
            inLiftMode = true;

            liftTargetElevation = Random.Range(30f, 100f);
            objRenderer.material.color = Color.blue;
            liftRotation = new Vector3(
                Random.Range(-100f, 100f),
                Random.Range(-100f, 100f),
                Random.Range(-100f, 100f));
        }

        public void ExitLiftMode()
        {
            currentGravity = 0f;
            inLiftMode = false;
            objRenderer.material.color = Color.white;
        }

        public virtual void TakeDamage(int amount = 1)
        {
            currentHealth -= amount;
            healthBar.SetPercentage((float)currentHealth / maxHealth);
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

        bool HitsPlayer(Collider collider) => playerLayer == (playerLayer | 1 << collider.gameObject.layer);

        bool OnGround => transform.position.y <= 0;
        bool ReachedLiftElevation => transform.position.y > liftTargetElevation;
        #endregion
    }
}