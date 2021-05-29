using System.Collections;
using UnityEngine;

namespace BattleMage.Enemies
{
    public class DummyEnemy : EnemyBase
    {
        public override void TakeDamage(int amount = 1)
        {
            base.TakeDamage(amount);
        }
    }
}