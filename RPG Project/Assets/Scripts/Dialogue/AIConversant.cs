using RPG.Control;
using UnityEngine;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] Dialogue dialogue;

        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            PlayerConversant conversant = callingController.GetComponent<PlayerConversant>();
            if (conversant == null) return false;
            if (Input.GetMouseButtonDown(0))
            {
                conversant.StartDialogue(this, dialogue);
            }
            return true;
        }
    }
}