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
        public LaserVisualizer LLaser;
        public LaserVisualizer RLaser;
        public SpellManager spellmanager;

        void Start()
        {
            //Laser visualizer
            LController.Input.OnPointerPressed.AddListener(_args =>
            {
                LLaser.EnableLaser();
            });

            LController.Input.OnPointerReleased.AddListener(_args =>
            {
                LLaser.EnableLaser();
            });

            RController.Input.OnPointerPressed.AddListener(_args =>
            {
                RLaser.EnableLaser();
            });

            RController.Input.OnPointerReleased.AddListener(_args =>
            {
                RLaser.DisableLaser();
            });

            //Spell 
            LController.Input.OnGrabPressed.AddListener(_args => //Shoot 
            {
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
    }
}