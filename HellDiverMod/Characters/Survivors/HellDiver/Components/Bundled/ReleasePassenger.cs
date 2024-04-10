using UnityEngine;

namespace HellDiverMod.Survivors.HellDiver.Components {
    public class ReleasePassenger : MonoBehaviour {

        [SerializeField]
        private RoR2.VehicleSeat vehicleSeat;

        public void Release() {
            vehicleSeat.EjectPassenger();
        }
        public void Show() {
            vehicleSeat.hidePassenger = false;
            vehicleSeat.passengerInfo.characterModel.invisibilityCount--;
        }
    }
}
