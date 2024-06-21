using EntityStates;
using HellDiverMod.Modules.BaseStates;
using MonoMod.RuntimeDetour;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// please excuse the messy code I am new and still trying to understand most of this... -EZ

namespace HellDiverMod.Survivors.HellDiver.SkillStates
{
    public class DiverFireShotgun : BaseHellDiverSkillState
    {
        public static int bulletCount = 10;
        public static float bulletSpread = 7f;

        public static float damageCoefficient = 1f;
        public static float procCoefficient = 0.8f;
        public static float baseDuration = 1.25f;
        public static float force = 200f;
        public static float recoil = 20f;
        public static float range = 2000f;
        public static GameObject tracerEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerGoldGat");
        public static GameObject critTracerEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerCaptainShotgun");

        private float duration;
        private float fireTime;
        private bool hasFired;
        private bool hasPlayed;
        private string muzzleString;
        private bool isCrit;

        protected virtual float _damageCoefficient => DiverFireShotgun.damageCoefficient;
        protected virtual GameObject tracerPrefab => this.isCrit ? DiverFireShotgun.critTracerEffectPrefab : DiverFireShotgun.tracerEffectPrefab;
        public virtual string shootSoundString => "Play_captain_m1_shootWide";
        public string pumpSoundString = "Play_captain_m1_reload";
        public virtual BulletAttack.FalloffModel falloff => BulletAttack.FalloffModel.DefaultBullet;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = DiverFireShotgun.baseDuration / this.attackSpeedStat;

            this.fireTime = 0.1f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.muzzleString = "GunMuzzle";

            this.isCrit = base.RollCrit();

            this.hasFired = true;
            this.Fire();

            this.PlayAnimation("Gesture, Override", "ShootShotgun", "Shoot.playbackRate", baseDuration * 1.5f / this.attackSpeedStat);
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

                float spread = bulletSpread;
                float recoilAmplitude = DiverFireShotgun.recoil / this.attackSpeedStat;

                base.AddRecoil2(-0.4f * recoilAmplitude, -0.8f * recoilAmplitude, -0.3f * recoilAmplitude, 0.3f * recoilAmplitude);

                BulletAttack bulletAttack = new BulletAttack
                {
                    // bulletCount = 1,
                    aimVector = aimRay.direction,
                    origin = aimRay.origin,
                    damage = this._damageCoefficient * this.damageStat,
                    damageColorIndex = DamageColorIndex.Default,
                    damageType = DamageType.Generic,
                    falloffModel = this.falloff,
                    maxDistance = DiverFireShotgun.range,
                    force = DiverFireShotgun.force,
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
                bulletAttack.minSpread = 0;
                bulletAttack.maxSpread = 0;
                bulletAttack.bulletCount = 1;
                bulletAttack.Fire();

                uint secondShot = (uint)Mathf.CeilToInt(bulletCount / 2f) - 1;
                bulletAttack.minSpread = 0;
                bulletAttack.maxSpread = spread / 1.45f;
                bulletAttack.bulletCount = secondShot;
                bulletAttack.Fire();

                bulletAttack.minSpread = spread / 1.45f;
                bulletAttack.maxSpread = spread;
                bulletAttack.bulletCount = (uint)Mathf.FloorToInt(bulletCount / 2f);
                bulletAttack.Fire();
                bulletAttack.Fire();
            }

            base.characterBody.AddSpreadBloom(1.25f);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if(base.fixedAge >= (baseDuration / 2f) / this.attackSpeedStat && !hasPlayed)
            {
                hasPlayed = true;
                Util.PlaySound(pumpSoundString, base.gameObject);
            }
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