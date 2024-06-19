using RoR2;
using UnityEngine;

namespace HellDiverMod.Survivors.HellDiver.Components {
    public class ReleasePassenger : MonoBehaviour {

        [SerializeField]
        private RoR2.VehicleSeat vehicleSeat;
        [SerializeField]
        private bool hasDelayed;
        [SerializeField]
        private bool hasPlayed;
        [SerializeField]
        private float timer;

        public void Release() {
            vehicleSeat.EjectPassenger();
        }

        public void FixedUpdate()
        {
            if (hasDelayed)
            {
                timer += Time.fixedDeltaTime;
            }

            if(timer >= 0.5f && !hasPlayed)
            {
                hasPlayed = true;
                Util.PlaySound("Play_UI_podBlastDoorOpen", base.gameObject);

            }
        }

        public void PlaySound()
        {
            Util.PlaySound("Play_UI_podImpact", base.gameObject);
            hasDelayed = true;
        }
        public void Show() {
            vehicleSeat.hidePassenger = false;
            vehicleSeat.passengerInfo.characterModel.invisibilityCount--;
        }
    }
}
