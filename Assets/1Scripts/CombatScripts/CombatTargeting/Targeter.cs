using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    #region Comments
    // We store a list of the targets in our range
    #endregion

    public List<Target> targets = new List<Target>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target currentTarget)) { return; }

        targets.Add(currentTarget);

    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.TryGetComponent<Target>(out Target currentTarget)) { return; }
        
        targets.Remove(currentTarget);
        

        

        

    }


}
