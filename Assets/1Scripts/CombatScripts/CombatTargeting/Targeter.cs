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

    private Camera mainCamera;                                          // We need to know where things are in
                                                                        // our screen so we store a reference to
                                                                        // the main camera

    private void Start()
    {
        mainCamera = Camera.main;
    }

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
        #region Comments

        // We run a check to make sure teh target is on the screen, then we check how close it is to
        // the center of the camera

        #endregion

        if (targets.Count == 0) { return false; }

        Target closestTarget = null;

        #region Comments

        // float closestTargetDistance. Distance to the target to the center of the screen
        // set to the biggest number possible

        #endregion

        float closestTargetDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            #region Comments

            // To find out where the object is in our camera we use WorldToViewPoint but we store in 
            // a Vector 2 called viewPos even thou the method returns a vertor 3
            // If the target is on the screen viewPos will be between (0,0) and (1,1)

            #endregion

            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            if(viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
            {
                continue;
            }

            #region Comments

            // distanceToCenter return a vector from our target to the center of the screen
            // then we compare to closetTargetDistance to figure out if its the new closestTarget

            #endregion

            Vector2 vectorToCenter = viewPos - new Vector2(0.5f, 0.5f);

            if(vectorToCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = vectorToCenter.sqrMagnitude;
            }


        }

        #region Comments

        // If there is still no target after the loop we return false

        #endregion

        if (closestTarget == null) { return false; }

        CurrentTarget = closestTarget;
                                                                
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
