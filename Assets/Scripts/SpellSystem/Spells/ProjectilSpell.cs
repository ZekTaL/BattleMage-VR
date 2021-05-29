using System.Collections;
using UnityEngine;

namespace BattleMage.SpellSystem
{
    public class ProjectilSpell : SpellBase
    {
        [SerializeField] float lifeDuration = 8f;
        [SerializeField] float moveSpeed = 8f;
        Rigidbody rb;

        public override void Initialize(Transform shootTransform, Vector3 raycastHitPoint)
        {
            base.Initialize(shootTransform, raycastHitPoint);
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * moveSpeed;
            Destroy(gameObject, lifeDuration);
        }
    }
}