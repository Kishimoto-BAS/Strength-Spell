using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace Strength
{
    public class StrengthIncrease : SpellCastCharge
    {
        // Hand Increased Strength
        public float LeftHandForce = 320f;
        public float RightHandForce = 320f;
        public float LeftHandForceDuration = 0.3f;
        public float RightHandForceDuration = 0.3f;
        public float LeftHandRagdollPartMultiplier = 1.5f;
        public float RightHandRagdollPartMultiplier = 1.5f;
        public float LeftHandOtherRagdollPartMultiplier = 1.2f;
        public float RightHandOtherRagdollPartMultiplier = 1.2f;
        public float LeftHandSlowMoMultiplier = 1.7f;
        public float RightHandSlowMoMultiplier = 1.7f;
        // Hand Decrease Strength
        private float NormalizeLeftHandForce = 250f;
        private float NormalizeRightHandForce = 250f;
        private float NormalizeLeftHandForceDuration = 0.15f;
        private float NormalizeRightHandForceDuration = 0.15f;
        private float NormalizeLeftHandRagdollPartMultiplier = 2.0f;
        private float NormalizeRightHandRagdollPartMultiplier = 2.0f;
        private float NormalizeLeftHandOtherRagdollPartMultiplier = 0.5f;
        private float NormalizeRightHandOtherRagdollPartMultiplier = 0.5f;
        private float NormalizeLeftHandSlowMoMultiplier = 0.9f;
        private float NormalizeRightHandSlowMoMultiplier = 0.9f;
        // Foot Increased Strength
        public float LeftFootForce = 1300f;
        public float RightFootForce = 1300f;
        public float LeftFootForceDuration = 0.5f;
        public float RightFootForceDuration = 0.5f;
        public float LeftFootRagdollPartMultiplier = 5.0f;
        public float RightFootRagdollPartMultiplier = 5.0f;
        public float LeftFootOtherRagdollPartMultiplier = 4.0f;
        public float RightFootOtherRagdollPartMultiplier = 4.0f;
        public float LeftFootSlowMoMultiplier = 3.5f;
        public float RightFootSlowMoMultiplier = 3.5f;
        // Foot Decrease Strength
        public float NormalizeLeftFootForce = 400f;
        public float NormalizeRightFootForce = 400f;
        public float NormalizeLeftFootForceDuration = 0.15f;
        public float NormalizeRightFootForceDuration = 0.15f;
        public float NormalizeLeftFootRagdollPartMultiplier = 2.0f;
        public float NormalizeRightFootRagdollPartMultiplier = 2.0f;
        public float NormalizeLeftFootOtherRagdollPartMultiplier = 0.5f;
        public float NormalizeRightFootOtherRagdollPartMultiplier = 0.5f;
        public float NormalizeLeftFootSlowMoMultiplier = 1.2f;
        public float NormalizeRightFootSlowMoMultiplier = 1.2f;
        // Easy Climbing
        public bool EasyClimbing = true;
        // Normalize Easy Climbing
        public bool NormalizeClimbing = false;
        public override void Load(SpellCaster spellCaster, Level level)
        {
            base.Load(spellCaster, level);
            if (spellCaster == true)
            {
                // Hand Strength Increase
                Player.currentCreature.handLeft.bodyDamager.data.addForce = LeftHandForce;
                Player.currentCreature.handRight.bodyDamager.data.addForce = RightHandForce;
                Player.currentCreature.handLeft.bodyDamager.data.addForceDuration = LeftHandForceDuration;
                Player.currentCreature.handRight.bodyDamager.data.addForceDuration = RightHandForceDuration;
                Player.currentCreature.handLeft.bodyDamager.data.addForceRagdollPartMultiplier = LeftHandRagdollPartMultiplier;
                Player.currentCreature.handRight.bodyDamager.data.addForceRagdollPartMultiplier = RightHandRagdollPartMultiplier;
                Player.currentCreature.handLeft.bodyDamager.data.addForceRagdollOtherMultiplier = LeftHandOtherRagdollPartMultiplier;
                Player.currentCreature.handRight.bodyDamager.data.addForceRagdollOtherMultiplier = RightHandOtherRagdollPartMultiplier;
                Player.currentCreature.handLeft.bodyDamager.data.addForceSlowMoMultiplier = LeftHandSlowMoMultiplier;
                Player.currentCreature.handRight.bodyDamager.data.addForceSlowMoMultiplier = RightHandSlowMoMultiplier;
                // Leg/Foot Stregth Increase
                Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForce = LeftFootForce;
                Player.local.footRight.ragdollFoot.data.bodyDamagerData.addForce = RightFootForce;
                Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForceDuration = LeftFootForceDuration;
                Player.local.footRight.ragdollFoot.data.bodyDamagerData.addForceDuration = RightFootForceDuration;
                Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForceRagdollPartMultiplier = LeftFootRagdollPartMultiplier;
                Player.local.footRight.ragdollFoot.data.bodyDamagerData.addForceRagdollPartMultiplier = RightFootRagdollPartMultiplier;
                Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForceRagdollOtherMultiplier = LeftFootOtherRagdollPartMultiplier;
                Player.local.footRight.ragdollFoot.data.bodyDamagerData.addForceRagdollOtherMultiplier = RightFootOtherRagdollPartMultiplier;
                Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForceSlowMoMultiplier = LeftFootSlowMoMultiplier;
                Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForceSlowMoMultiplier = RightFootSlowMoMultiplier;
                // Easy Climbing
                RagdollHandClimb.climbFree = EasyClimbing;
            }
        }

        public override void Unload()
        {
            base.Unload();
            // Hand Decrease Strength
            Player.currentCreature.handLeft.bodyDamager.data.addForce = NormalizeLeftHandForce;
            Player.currentCreature.handRight.bodyDamager.data.addForce = NormalizeRightHandForce;
            Player.currentCreature.handLeft.bodyDamager.data.addForceDuration = NormalizeLeftHandForceDuration;
            Player.currentCreature.handRight.bodyDamager.data.addForceDuration = NormalizeRightHandForceDuration;
            Player.currentCreature.handLeft.bodyDamager.data.addForceRagdollPartMultiplier = NormalizeLeftHandRagdollPartMultiplier;
            Player.currentCreature.handRight.bodyDamager.data.addForceRagdollPartMultiplier = NormalizeRightHandRagdollPartMultiplier;
            Player.currentCreature.handLeft.bodyDamager.data.addForceRagdollOtherMultiplier = NormalizeLeftHandOtherRagdollPartMultiplier;
            Player.currentCreature.handRight.bodyDamager.data.addForceRagdollOtherMultiplier = NormalizeRightHandOtherRagdollPartMultiplier;
            Player.currentCreature.handLeft.bodyDamager.data.addForceSlowMoMultiplier = NormalizeLeftHandSlowMoMultiplier;
            Player.currentCreature.handRight.bodyDamager.data.addForceSlowMoMultiplier = NormalizeRightHandSlowMoMultiplier;
            // Leg/Foot Decrease Stregth
            Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForce = NormalizeLeftFootForce;
            Player.local.footRight.ragdollFoot.data.bodyDamagerData.addForce = NormalizeRightFootForce;
            Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForceDuration = NormalizeLeftFootForceDuration;
            Player.local.footRight.ragdollFoot.data.bodyDamagerData.addForceDuration = NormalizeRightFootForceDuration;
            Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForceRagdollPartMultiplier = NormalizeLeftFootRagdollPartMultiplier;
            Player.local.footRight.ragdollFoot.data.bodyDamagerData.addForceRagdollPartMultiplier = NormalizeRightFootRagdollPartMultiplier;
            Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForceRagdollOtherMultiplier = NormalizeLeftFootOtherRagdollPartMultiplier;
            Player.local.footRight.ragdollFoot.data.bodyDamagerData.addForceRagdollOtherMultiplier = NormalizeRightFootOtherRagdollPartMultiplier;
            Player.local.footLeft.ragdollFoot.data.bodyDamagerData.addForceSlowMoMultiplier = NormalizeLeftFootSlowMoMultiplier;
            Player.local.footRight.ragdollFoot.data.bodyDamagerData.addForceSlowMoMultiplier = NormalizeRightFootSlowMoMultiplier;
            // Easy Climbing
            RagdollHandClimb.climbFree = NormalizeClimbing;
        }
    }
}
