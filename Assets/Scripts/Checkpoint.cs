using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isFinishLine;

    private void OnTriggerEnter(Collider other)
    {
        var otherLapTracker = other.GetComponent<LapTracker>();
        if(otherLapTracker != null )
        {
            otherLapTracker.AddCheckpoint(this);
        }
    }
}
