using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Start()
        {
            PlayableDirector director = GetComponent<PlayableDirector>();
            director.played += RemoveControl;
            director.stopped += RestoreControl;

            player = GameObject.FindWithTag("Player");
        }

        private void RemoveControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void RestoreControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}