using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;
using RPG.Core;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField] PlayerController PlayerController;
    [SerializeField] ActionScheduler actionScheduler;

    
    private void OnTriggerEnter(Collider other) 
    {
        print("triggered");
        if(other.GetComponent<PlayerController>())
        {
            GetComponent<PlayableDirector>().Play();
            actionScheduler.CancelCurrentAction();
            
        }
    }
}
