using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Core;
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
        [SerializeField] Transform targetingCirclePrefab;

        Transform targetingCircle;

        public override void StartTargeting(TargetingData data, Action<TargetingData> callback)
        {
            new TargetingAction(this, data, callback);
        }

        class TargetingAction : IAction
        {
            PlayerController playerController = null;
            DelayedClickTargeting strategy;
            Coroutine targetingRoutine;
            ActionScheduler scheduler;

            public TargetingAction(DelayedClickTargeting strategy, TargetingData data, Action<TargetingData> callback)
            {
                this.strategy = strategy;
                playerController = data.GetSource().GetComponent<PlayerController>();
                scheduler = data.GetSource().GetComponent<ActionScheduler>();
                scheduler.StartAction(this, 2, 3);
                targetingRoutine = playerController.StartCoroutine(Targeting(data, callback));
            }
            
            private IEnumerator Targeting(TargetingData data, Action<TargetingData> callback)
            {
                yield return new WaitUntil(() => scheduler.isCurrentAction(this));
                playerController.enabled = false;
                if (!strategy.targetingCircle) strategy.targetingCircle = Instantiate(strategy.targetingCirclePrefab);
                Transform targetingCircle = strategy.targetingCircle;

                while (true)
                {
                    Cursor.SetCursor(strategy.effectCursor, strategy.cursorHotspot, CursorMode.Auto);

                    RaycastHit mouseHit;
                    if (Physics.Raycast(PlayerController.GetMouseRay(), out mouseHit, 100, strategy.castingLayer))
                    {
                        targetingCircle.gameObject.SetActive(true);
                        targetingCircle.position = mouseHit.point;
                        targetingCircle.localScale = new Vector3(strategy.areaOfEffectRadius * 2, 1, strategy.areaOfEffectRadius * 2);

                        if (Input.GetMouseButtonDown(0))
                        {
                            data.SetTarget(mouseHit.point);
                            data.SetTargets(GetGameObjectsInArea(mouseHit.point));
                            // Capture the whole of this mouse click so we don't move.
                            yield return new WaitWhile(() => Input.GetMouseButton(0));
                            if (callback != null) callback(data);
                            scheduler.FinishAction(this);
                            Cancel();
                            yield break;
                        }

                    }
                    else
                    {
                        targetingCircle.gameObject.SetActive(false);
                    }

                    yield return null;
                }
            }

            private IEnumerable<GameObject> GetGameObjectsInArea(Vector3 point)
            {
                RaycastHit[] hits = Physics.SphereCastAll(point, strategy.areaOfEffectRadius, Vector3.up, 0);
                foreach (RaycastHit hit in hits)
                {
                    yield return hit.transform.gameObject;
                }
            }

            public void Cancel()
            {
                playerController.StopCoroutine(targetingRoutine);
                strategy.targetingCircle.gameObject.SetActive(false);
                playerController.enabled = true;
            }
        }
    }
}
