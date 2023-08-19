using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    #region Comments
    // We store a list of the targets in our range
    // and then remove them from the list once they are out of range
    #endregion

    public List<Target> targets = new List<Target>();                   // Remember to make private after testing
    public Target CurrentTarget {  get; private set; }

    [SerializeField] private CinemachineTargetGroup targetGroup;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        targets.Add(target);
        target.OnDestroyed += RemoveTarget;

    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.TryGetComponent<Target>(out Target target)) { return; }

        RemoveTarget(target);
  
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }
      
        CurrentTarget = targets[0];

        targetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }

    public void CancelTargeting()
    {
        if(CurrentTarget == null) { return; }

        targetGroup.RemoveMember(CurrentTarget.transform);


        CurrentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target)
        {
            targetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }


}
