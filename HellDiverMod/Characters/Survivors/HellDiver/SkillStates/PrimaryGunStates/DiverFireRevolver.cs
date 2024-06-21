using EntityStates;
using HellDiverMod.Modules.BaseStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// yet another scuffed skill

namespace HellDiverMod.Survivors.HellDiver.SkillStates
{
    public class DiverFireRevolver : BaseHellDiverSkillState
    {
        public static float damageCoefficient = 3.9f;
        public static float procCoefficient = 1f;
        public static float baseDuration = 0.5f;
        public static float force = 200f;
        public static float recoil = 6f;
        public static float range = 2000f;
        public static GameObject tracerEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerGoldGat");
        public static GameObject critTracerEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerCaptainShotgun");

        private float duration;
        private float fireTime;
        private bool hasFired;
        private string muzzleString;
        private bool isCrit;

        protected virtual float _damageCoefficient => DiverFireRevolver.damageCoefficient;
        protected virtual GameObject tracerPrefab => this.isCrit ? DiverFireRevolver.critTracerEffectPrefab : DiverFireRevolver.tracerEffectPrefab;
        public virtual string shootSoundString => "Play_commando_R";
        public virtual BulletAttack.FalloffModel falloff => BulletAttack.FalloffModel.DefaultBullet;

        public override void OnEnter()
        {
            base.OnEnter();
            
            this.duration = DiverFireRevolver.baseDuration / this.attackSpeedStat;

            // this.fireTime = 0.1f* this.duration;
            base.characterBody.SetAimTimer(2f);
            this.muzzleString = "GunMuzzle";

            this.isCrit = base.RollCrit();

            this.hasFired = true;
            this.Fire();

            this.PlayAnimation("Gesture, Override", "ShootPistol", "Shoot.playbackRate", 1f / this.attackSpeedStat);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void Fire()
        {
            EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, this.gameObject, this.muzzleString, false);
            Util.PlaySound(this.shootSoundString, this.gameObject);
            if (base.isAuthority)
            {
                Ray aimRay = base.GetAimRay();
                base.AddRecoil2(-1f * DiverFireRevolver.recoil, -1f * DiverFireRevolver.recoil, -0.5f * DiverFireRevolver.recoil, 0.5f * DiverFireRevolver.recoil);

                BulletAttack bulletAttack = new BulletAttack
                {
                    bulletCount = 1,
                    aimVector = aimRay.direction,
                    origin = aimRay.origin,
                    damage = this._damageCoefficient * this.damageStat,
                    damageColorIndex = DamageColorIndex.Default,
                    damageType = DamageType.Generic,
                    falloffModel = this.falloff,
                    maxDistance = DiverFireRevolver.range,
                    force = DiverFireRevolver.force,
                    hitMask = LayerIndex.CommonMasks.bullet,
                    minSpread = 0f,
                    maxSpread = this.characterBody.spreadBloomAngle * 1.5f,
                    isCrit = this.isCrit,
                    owner = base.gameObject,
                    muzzleName = muzzleString,
                    smartCollision = true,
                    procChainMask = default(ProcChainMask),
                    procCoefficient = procCoefficient,
                    radius = 0.75f,
                    sniper = false,
                    stopperMask = LayerIndex.CommonMasks.bullet,
                    weapon = null,
                    tracerEffectPrefab = this.tracerPrefab,
                    spreadPitchScale = 1f,
                    spreadYawScale = 1f,
                    queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                    hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,
                };
                bulletAttack.Fire();
            }

            base.characterBody.AddSpreadBloom(1.75f); // was 1.25f
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            if (base.fixedAge >= this.duration) return InterruptPriority.Any;
            return InterruptPriority.PrioritySkill;
        }
    }
}