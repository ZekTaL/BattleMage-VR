using System.Collections;
using UnityEngine;
using BattleMage.VR;
using BattleMage.SpellSystem;

namespace BattleMage
{
    public class VRInputFuncitonalityConnector : MonoBehaviour
    {
        public VRController LController;
        public VRController RController;
        public PlayerManager playerManager;

        void Start()
        {
            //Press grab = Laser visualizer 
            //Release grab = shoot attack
            LController.Input.OnGrabPressed.AddListener(_args => //Shoot 
            {
                playerManager.PressedTrigger(true);
            });
            LController.Input.OnGrabReleased.AddListener(_args => //Shoot 
            {
                playerManager.ReleasedTrigger(true);
            });

            LController.Input.OnGrabPressed.AddListener(_args => //Shoot 
            {
                playerManager.PressedTrigger(false);
            });
            LController.Input.OnGrabReleased.AddListener(_args => //Shoot 
            {
                playerManager.ReleasedTrigger(false);
            });

            //Spell 
            LController.Input.OnInteractPressed.AddListener(_args => //Cycle 
            {
                playerManager.PressedToggleSpell(true);
            });
            RController.Input.OnInteractPressed.AddListener(_args => //Cycle 
            {
                playerManager.PressedToggleSpell(false);
            });
        }
    }
}

/*
 void Start()
        {
            //Laser visualizer
            LController.Input.OnPointerPressed.AddListener(_args =>
            {
                playerManager.PressedTrigger(true);
            });

            LController.Input.OnPointerReleased.AddListener(_args =>
            {
                playerManager.ReleasedTrigger(true);
            });

            RController.Input.OnPointerPressed.AddListener(_args =>
            {
                playerManager.PressedTrigger(false);
            });

            RController.Input.OnPointerReleased.AddListener(_args =>
            {
                playerManager.ReleasedTrigger(false);
            });

            //Spell 
            LController.Input.OnGrabPressed.AddListener(_args => //Shoot 
            {
                playerManager.PressedToggleSpell(false);
                spellmanager.ShootLeft(LController.transform, LLaser.HitPosition);
            });
            LController.Input.OnInteractPressed.AddListener(_args => //Cycle 
            {
                spellmanager.CycleLeft();
            });

            RController.Input.OnGrabPressed.AddListener(_args => //Shoot 
            {
                spellmanager.ShootRight(RController.transform, RLaser.HitPosition);
            });
            RController.Input.OnInteractPressed.AddListener(_args => //Cycle 
            {
                spellmanager.CycleRight();
            });
        }
 */