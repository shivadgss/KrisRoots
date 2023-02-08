using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialougeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private GameObject dialougeBox;
    private ResponseHandler _responseHandler;
    private Typewriter _typewriter;
    public PlayerMovement player;
    public bool Isopen { get; private set; }

    private void Start()
    {
        _typewriter = GetComponent<Typewriter>();
        CloseDialougeBox();
    }

    public void ShowDialouge(DialougeObject dialougeObject)
    {
        Isopen = true;
        dialougeBox.SetActive(true);
        StartCoroutine(StepThroughDialouge(dialougeObject));
    }
    
    

    private IEnumerator StepThroughDialouge(DialougeObject dialougeObject)
    {

        for (int i = 0; i < dialougeObject.Dialouge.Length; i++)
        {
            string dialouge = dialougeObject.Dialouge[i];
            yield return _typewriter.Run(dialouge, textLabel);

            if (i == dialougeObject.Dialouge.Length - 1 && dialougeObject.HasResponses)
                break;
            
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }

        if (dialougeObject.HasResponses)
        {
            _responseHandler.ShowResponses(dialougeObject.Responses);
        }

        else
        {
            CloseDialougeBox();
        }
    }

    private void CloseDialougeBox()
    {
        Isopen = false;
        dialougeBox.SetActive(false);
        textLabel.text = string.Empty;
        player.movementEnable = true;
    }
}