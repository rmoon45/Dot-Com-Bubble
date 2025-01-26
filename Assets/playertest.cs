using HelloWorld;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class playertest : NetworkBehaviour
{
    public NetworkVariable<int> number = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI numText;
    public TextMeshProUGUI num2Text;

    // public static NetworkVariable<int> num2 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        number.OnValueChanged += (int prev, int newValue) =>
        {
            numText.text = newValue.ToString();
        };

        numManager.instance.OnValueChanged += (int prev, int newValue) =>
       {
           num2Text.text = newValue.ToString();
       };
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.position += Time.deltaTime * Vector3.left * 10;
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.position += Time.deltaTime * Vector3.right * 10;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                number.Value = number.Value + 1;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ToggleServerRpc();
            }

        }

    }

    [Rpc(SendTo.Server)]
    public void ToggleServerRpc()
    {
        // this will cause a replication over the network
        // and ultimately invoke `OnValueChanged` on receivers
        numManager.instance.IncreaseValue();
    }
}
