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
        [SerializeField] GameObject root;
        [SerializeField] TextMeshProUGUI mainDialogue;
        [SerializeField] Transform choiceContainer;
        [SerializeField] Button choicePrefab;
        [SerializeField] Button nextButton;

        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.conversationUpdated += Redraw;
            nextButton.onClick.AddListener(() => 
            {
                    playerConversant.Next(); 
            });

            Redraw();
        }

        // Update is called once per frame
        void Redraw()
        {
            root.SetActive(playerConversant.HasActiveConversation());
            mainDialogue.gameObject.SetActive(!playerConversant.IsChoosing());
            choiceContainer.gameObject.SetActive(playerConversant.IsChoosing());

            if (!playerConversant.IsChoosing())
            {
                mainDialogue.text = playerConversant.GetCurrentText();
            }
            else
            {
                DrawChoices();
            }
            nextButton.gameObject.SetActive(playerConversant.HasNext());
        }

        void DrawChoices()
        {
            foreach (Transform child in choiceContainer)
            {
                Destroy(child.gameObject);
            }
            foreach (DialogueNode choice in playerConversant.GetCurrentChoices())
            {
                var choiceButton = Instantiate(choicePrefab, choiceContainer);
                TextMeshProUGUI text = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                text.text = choice.GetText();
                choiceButton.onClick.AddListener(() => 
                {
                    playerConversant.ChooseNext(choice);
                });
            }
        }
    }
}
