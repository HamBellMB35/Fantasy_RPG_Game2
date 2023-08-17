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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target Target)) { return; }

        targets.Add(Target);

    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.TryGetComponent<Target>(out Target Target)) { return; }
        
        targets.Remove(Target);
  
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }
      
       CurrentTarget = targets[0];

        return true;
    }

    public void CancelTargeting()
    {
        CurrentTarget = null;
    }


}
