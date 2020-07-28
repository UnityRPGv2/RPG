using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Dialogue;
using TMPro;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI mainDialogue;
        [SerializeField] Button nextButton;

        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(() => playerConversant.Next());
        }

        // Update is called once per frame
        void Update()
        {
            mainDialogue.text = playerConversant.GetCurrentText();
            nextButton.gameObject.SetActive(playerConversant.HasNext());
        }
    }
}
