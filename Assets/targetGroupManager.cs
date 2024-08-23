using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetGroupManager : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup; // Reference to your Cinemachine Target Group

    // Method to add a target to the group
    public void AddTarget(Transform target, float weight = 1, float radius = 0)
    {
        // Create a new target array with one more slot
        CinemachineTargetGroup.Target[] newTargets = new CinemachineTargetGroup.Target[targetGroup.m_Targets.Length + 1];

        // Copy the existing targets into the new array
        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            newTargets[i] = targetGroup.m_Targets[i];
        }

        // Add the new target at the last position
        newTargets[newTargets.Length - 1] = new CinemachineTargetGroup.Target
        {
            target = target,
            weight = weight,
            radius = radius
        };

        // Assign the new array back to the target group
        targetGroup.m_Targets = newTargets;
    }

    // Method to remove a target from the group
    public void RemoveTarget(Transform target)
    {
        // Create a list to hold the remaining targets
        var newTargets = new System.Collections.Generic.List<CinemachineTargetGroup.Target>();

        // Copy all targets except the one we want to remove
        foreach (var t in targetGroup.m_Targets)
        {
            if (t.target != target)
            {
                newTargets.Add(t);
            }
        }

        // Assign the new array back to the target group
        targetGroup.m_Targets = newTargets.ToArray();
    }
}
