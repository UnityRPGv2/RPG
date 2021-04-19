using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "DelayedClickTargeting", menuName = "Abilities/Targeting/Delayed Click", order = 0)]
    public class DelayedClickTargeting : TargetingStrategy
    {
        [SerializeField] Texture2D effectCursor;
        [SerializeField] Vector2 cursorHotspot;
        [SerializeField] float areaOfEffect;
        [SerializeField] LayerMask castingLayer;

        PlayerController playerController;

        public override void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> confirm)
        {
            playerController = user.GetComponent<PlayerController>();
            playerController.StartCoroutine(Targeting(user, confirm));
        }

        private IEnumerator Targeting(GameObject user, Action<IEnumerable<GameObject>> confirm)
        {
            playerController.enabled = false;
            while (true)
            {
                Cursor.SetCursor(effectCursor, cursorHotspot, CursorMode.Auto);
                if (Input.GetMouseButtonDown(0))
                {
                    if (confirm != null) confirm(GetGameObjectsInArea());
                    // Capture the whole of this mouse click so we don't move.
                    yield return new WaitWhile(() => Input.GetMouseButton(0));
                    playerController.enabled = true;
                    yield break;
                }
                yield return null;
            }
        }

        private IEnumerable<GameObject> GetGameObjectsInArea()
        {
            RaycastHit mouseHit;
            if (Physics.Raycast(PlayerController.GetMouseRay(), out mouseHit, 100, castingLayer))
            {
                RaycastHit[] hits = Physics.SphereCastAll(mouseHit.point, areaOfEffect, Vector3.up, 0);
                foreach (RaycastHit hit in hits)
                {
                    yield return hit.transform.gameObject;
                }
            }
        }
    }
}
