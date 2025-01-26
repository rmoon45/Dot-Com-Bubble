using HelloWorld;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class numManager : NetworkBehaviour
{
    public static numManager instance;

    public NetworkVariable<int> number = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public delegate void OnValueChangedDelegate(int previousValue, int newValue);
    public OnValueChangedDelegate OnValueChanged;

    void Awake()
    {
        instance = this;
    }


    public override void OnNetworkSpawn()
    {
        number.OnValueChanged += (int prev, int newValue) =>
        {
            OnValueChanged?.Invoke(prev, newValue);
        };
    }

    public void IncreaseValue()
    {
        number.Value = number.Value + 1;
    }
}

