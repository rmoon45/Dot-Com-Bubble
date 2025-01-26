using TMPro;
using UnityEngine;

public class BillingRow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptionText;

    [SerializeField] TextMeshProUGUI numberText;

    public void SetBillingRowText(LineItem lineItem)
    {
        descriptionText.text = lineItem.name;

        numberText.text = "$" + lineItem.cost.ToString() + "/day";

        Debug.Log("Added " + lineItem.name);
    }
}
