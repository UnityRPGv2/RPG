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
        [SerializeField] float areaOfEffectRadius;
        [SerializeField] LayerMask castingLayer;
        [SerializeField] Transform targettingCirclePrefab;

        PlayerController playerController;
        Transform targettingCircle;

        public override void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> confirm)
        {
            playerController = user.GetComponent<PlayerController>();
            playerController.StartCoroutine(Targeting(user, confirm));
        }

        private IEnumerator Targeting(GameObject user, Action<IEnumerable<GameObject>> confirm)
        {
            playerController.enabled = false;
            if (!targettingCircle) targettingCircle = Instantiate(targettingCirclePrefab);
            
            while (true)
            {
                Cursor.SetCursor(effectCursor, cursorHotspot, CursorMode.Auto);

                RaycastHit mouseHit;
                if (Physics.Raycast(PlayerController.GetMouseRay(), out mouseHit, 100, castingLayer))
                {
                    targettingCircle.gameObject.SetActive(true);
                    targettingCircle.position = mouseHit.point;
                    targettingCircle.localScale = new Vector3(areaOfEffectRadius*2, 1, areaOfEffectRadius*2);
                }
                else
                {
                    targettingCircle.gameObject.SetActive(false);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    targettingCircle.gameObject.SetActive(false);
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
                RaycastHit[] hits = Physics.SphereCastAll(mouseHit.point, areaOfEffectRadius, Vector3.up, 0);
                foreach (RaycastHit hit in hits)
                {
                    yield return hit.transform.gameObject;
                }
            }
        }
    }
}
