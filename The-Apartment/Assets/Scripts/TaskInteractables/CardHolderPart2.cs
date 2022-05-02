using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderPart2 : MonoBehaviour
{
    #region Editor_variables
    public GameObject card1InPlace;
    public GameObject card2InPlace;
    public GameObject card3InPlace;
    public GameObject card4InPlace;
    #endregion

    InventoryManager invM;

    int card1ID = 17;
    int card2ID = 18;
    int card3ID = 19;
    int card4ID = 20;

    int numCardsInPlace = 0;
    bool nextStep = false;

    RedCrystalBall symbol; 

    void Awake ()
    {
        invM = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
        card1InPlace.SetActive(false);
        card2InPlace.SetActive(false);
        card3InPlace.SetActive(false);
        card4InPlace.SetActive(false);

        symbol = GameObject.Find("SecretSymbol").GetComponent<RedCrystalBall>();
    }

    void OnMouseDown()
    {
        if (invM.GetSelectedID() == card1ID)
        {
            invM.RemoveItem(card1ID, false);
            card1InPlace.SetActive(true);
            numCardsInPlace++;
        } else if (invM.GetSelectedID() == card2ID)
        {
            invM.RemoveItem(card2ID, false);
            card2InPlace.SetActive(true);
            numCardsInPlace++;
        } else if (invM.GetSelectedID() == card3ID)
        {
            invM.RemoveItem(card3ID, false);
            card3InPlace.SetActive(true);
            numCardsInPlace++;
        } else if (invM.GetSelectedID() == card4ID)
        {
            invM.RemoveItem(card4ID, false);
            card4InPlace.SetActive(true);
            numCardsInPlace++;
        }

        if (numCardsInPlace == 4 && !nextStep)
        {
            //Call next step
            nextStep = true;
            Debug.Log("All cards collected!");
            StartCoroutine(symbol.Appear());
        }
    }

    
}
