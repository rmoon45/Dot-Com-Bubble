using System.Collections.Generic;
using UnityEngine;

public class BillingManager : MonoBehaviour
{
    [SerializeField] GameObject billingRow;

    [SerializeField] GameObject billingPanel;

    List<GameObject> instantiatedBillingRows;

    void Start() {
        instantiatedBillingRows = new List<GameObject>();

    }
    
    /// <summary>
    /// Set the billing list
    /// </summary>
    void SetBillingList(List<LineItem> lineItems) {
        // clear out the list and destroy
        foreach (GameObject bw in instantiatedBillingRows) {
            Destroy(bw);
        }
        instantiatedBillingRows.Clear();

        // loop thru
        foreach (LineItem li in lineItems) {
            GameObject newBillingRow = Instantiate(billingRow, billingPanel.transform);

            instantiatedBillingRows.Add(newBillingRow);

            newBillingRow.GetComponent<BillingRow>().SetBillingRowText(li);
        }
    }
}
