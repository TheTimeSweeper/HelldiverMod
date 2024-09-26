using RoR2;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawGizmoDynamicBone
{
    [DrawGizmo(GizmoType.Selected, typeof(DynamicBone))]
    private void DrawGizmos(DynamicBone dynamicBone, GizmoType gizmoType)
    {
        if (!dynamicBone.enabled || dynamicBone.m_Root == null)
        {
            return;
        }
        if (Application.isEditor && !Application.isPlaying && dynamicBone.transform.hasChanged)
        {
            dynamicBone.InitTransforms();
            dynamicBone.SetupParticles();
        }
        Gizmos.color = Color.white;
        for (int i = 0; i < dynamicBone.m_Particles.Count; i++)
        {
            DynamicBone.Particle particle = dynamicBone.m_Particles[i];
            if (particle.m_ParentIndex >= 0)
            {
                DynamicBone.Particle particle2 = dynamicBone.m_Particles[particle.m_ParentIndex];
                Gizmos.DrawLine(particle.m_Position, particle2.m_Position);
            }
            if (particle.m_Radius > 0f)
            {
                Gizmos.DrawWireSphere(particle.m_Position, particle.m_Radius * dynamicBone.m_ObjectScale);
            }
        }
    }

    [DrawGizmo(GizmoType.Selected, typeof(DynamicBoneCollider))]
    private void DrawGizmos(DynamicBoneCollider dynamicBoneCollider, GizmoType gizmoType)
    {
        if (!dynamicBoneCollider.enabled)
        {
            return;
        }
        if (dynamicBoneCollider.m_Bound == DynamicBoneCollider.Bound.Outside)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.magenta;
        }
        float radius = dynamicBoneCollider.m_Radius * Mathf.Abs(dynamicBoneCollider.transform.lossyScale.x);
        float num = dynamicBoneCollider.m_Height * 0.5f - dynamicBoneCollider.m_Radius;
        if (num <= 0f)
        {
            Gizmos.DrawWireSphere(dynamicBoneCollider.transform.TransformPoint(dynamicBoneCollider.m_Center), radius);
            return;
        }
        Vector3 center = dynamicBoneCollider.m_Center;
        Vector3 center2 = dynamicBoneCollider.m_Center;
        switch (dynamicBoneCollider.m_Direction)
        {
            case DynamicBoneCollider.Direction.X:
                center.x -= num;
                center2.x += num;
                break;
            case DynamicBoneCollider.Direction.Y:
                center.y -= num;
                center2.y += num;
                break;
            case DynamicBoneCollider.Direction.Z:
                center.z -= num;
                center2.z += num;
                break;
        }
        Gizmos.DrawWireSphere(dynamicBoneCollider.transform.TransformPoint(center), radius);
        Gizmos.DrawWireSphere(dynamicBoneCollider.transform.TransformPoint(center2), radius);
    }
}
