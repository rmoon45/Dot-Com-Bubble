using System.Collections.Generic;
using UnityEngine;

public class BillingManager : MonoBehaviour
{
    [SerializeField] GameObject billingRow;

    [SerializeField] GameObject billingPanel;

    List<GameObject> instantiatedBillingRows;


    /// <summary>
    /// Set the billing list
    /// </summary>
    public void SetBillingList(List<LineItem> lineItems)
    {
        // clear out the list and destroy
        if (instantiatedBillingRows != null)
        {
            foreach (GameObject bw in instantiatedBillingRows)
            {
                Destroy(bw);
            }
            instantiatedBillingRows.Clear();
        }
        else
        {
            instantiatedBillingRows = new List<GameObject>();
        }

        // loop thru
        foreach (LineItem li in lineItems)
        {
            GameObject newBillingRow = Instantiate(billingRow, billingPanel.transform);

            instantiatedBillingRows.Add(newBillingRow);

            newBillingRow.GetComponent<BillingRow>().SetBillingRowText(li);
        }
    }
}
