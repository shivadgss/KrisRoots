using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResponseHandler : MonoBehaviour
{
   [SerializeField] private RectTransform responsebox;
   [SerializeField] private RectTransform responseButtonTemplate;
   [SerializeField] private RectTransform responseContainer;

   private DialougeUI _dialougeUI;
   private List<GameObject> tempResponseButtons = new List<GameObject>();

   private void Start()
   {
      _dialougeUI = GetComponent<DialougeUI>();
   }

   public void ShowResponses(Response[] responses)
   {
      float responseboxHeight = 0;

      foreach (Response response in responses)
      {
         GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
         responseButton.gameObject.SetActive(true);
         responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
         responseButton.GetComponent<Button>().onClick.AddListener(()=>OnPickedResponse(response));
         
         tempResponseButtons.Add(responseButton);
        
         responseboxHeight += responseButtonTemplate.sizeDelta.y;
      }

      responsebox.sizeDelta = new Vector2(responsebox.sizeDelta.x, responseboxHeight);
      responsebox.gameObject.SetActive(true);
   }

   private void OnPickedResponse(Response response)
   {
      responsebox.gameObject.SetActive(false);

      foreach ( GameObject button in tempResponseButtons)
      {
         Destroy(button);
      }
      tempResponseButtons.Clear();
      
      _dialougeUI.ShowDialouge(response.DialougeObject);
   }


}
