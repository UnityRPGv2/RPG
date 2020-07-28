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
        [SerializeField] Transform choiceContainer;
        [SerializeField] Button choicePrefab;
        [SerializeField] Button nextButton;

        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(() => 
            {
                    playerConversant.Next(); 
                    Redraw();
            });

            Redraw();
        }

        // Update is called once per frame
        void Redraw()
        {
            mainDialogue.gameObject.SetActive(!playerConversant.IsChoosing());
            choiceContainer.gameObject.SetActive(playerConversant.IsChoosing());

            if (!playerConversant.IsChoosing())
            {
                mainDialogue.text = playerConversant.GetCurrentText();
            }
            else
            {
                foreach (Transform child in choiceContainer)
                {
                    Destroy(child.gameObject);
                }
                for (int i = 0; i < 4; i++)
                {
                    var choiceButton = Instantiate(choicePrefab, choiceContainer);
                }

            }
            nextButton.gameObject.SetActive(playerConversant.HasNext());
        }
    }
}
