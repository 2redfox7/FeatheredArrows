using Mirror;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ConnectionMenu : MonoBehaviour
{

    public NetworkManager manager;
    public TMP_InputField ip_InputField;
    public GameObject HostConnect_go;
    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }
    public void HostFunction()
    {
        manager.StartHost();
        HostConnect_go.SetActive(false);
    }

    public void ConnectFunction()
    {
        manager.networkAddress = ip_InputField.text;
        manager.StartClient();
        HostConnect_go.SetActive(false);
    }
}