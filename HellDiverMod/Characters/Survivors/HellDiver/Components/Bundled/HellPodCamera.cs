using RoR2;
using UnityEngine;

namespace HellDiverMod.Survivors.HellDiver.Components {
    public class HellPodCamera : MonoBehaviour, ICameraStateProvider {

        [SerializeField]
        private Transform cameraBone;

        [SerializeField]
        private VehicleSeat vehicleSeat;

        //all stolen from survivorpodcontroller
        public void GetCameraState(CameraRigController cameraRigController, ref CameraState cameraState) {

            Vector3 position = this.cameraBone.position;
            Vector3 direction = cameraBone.forward;
            Ray ray = new Ray(position, direction);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, direction.magnitude, LayerIndex.world.mask, QueryTriggerInteraction.Ignore)) {
                position = ray.GetPoint(Mathf.Max(raycastHit.distance - 0.25f, 0.25f));
            }
            cameraState = new CameraState {
                position = position,
                rotation = this.cameraBone.rotation,
                fov = 60f
            };
        }

        public bool IsHudAllowed(CameraRigController cameraRigController) {
            return true;
        }

        public bool IsUserControlAllowed(CameraRigController cameraRigController) {
            return false;
        }

        public bool IsUserLookAllowed(CameraRigController cameraRigController) {
            return false;
        }

        private void Update() {
            this.UpdateCameras(this.vehicleSeat.currentPassengerBody ? this.vehicleSeat.currentPassengerBody.gameObject : null);
        }

        // Token: 0x06003165 RID: 12645 RVA: 0x000D1E7C File Offset: 0x000D007C
        private void UpdateCameras(GameObject characterBodyObject) {
            foreach (CameraRigController cameraRigController in CameraRigController.readOnlyInstancesList) {
                if (characterBodyObject && cameraRigController.target == characterBodyObject) {
                    cameraRigController.SetOverrideCam(this, 0f);
                } else if (cameraRigController.IsOverrideCam(this)) {
                    cameraRigController.SetOverrideCam(null, 0.05f);
                }
            }
        }
    }
}
