 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class multiplayerPlayer : NetworkBehaviour
{
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Camera playerCamera;

    float rotationX = 0f;
    float rotationY = 0f;

    public static float sensitivity = 2f;

    bool facingRight = true;
    public static multiplayerPlayer localPlayer;
    [SyncVar] public string matchID;

    //public Transform orientation;
    /*public override void OnStartAuthority()
    {
        playerCamera.gameObject.SetActive(true);
    }*/


    private NetworkMatch networkMatch;


    void Start()
    {
        /*if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
        }*/
        //Cursor.lockState = CursorLockMode.Locked;
        networkMatch = GetComponent<NetworkMatch>();

        if (isLocalPlayer)
        {
            localPlayer = this;
        }
        else
        {
            MainMenuMP.instance.SpawnPlayerUIPrefab(this);

        }
    }
    void Update()
    {
        /*if (!isLocalPlayer)
        {
            return;
        }*/
        networkMatch = GetComponent<NetworkMatch>();

        if (isLocalPlayer)
        {
            localPlayer = this;
        }
        else
        {
            MainMenuMP.instance.SpawnPlayerUIPrefab(this);
        }
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        rotationX += Input.GetAxis("Mouse Y") * -1 * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        transform.localEulerAngles = new Vector3(rotationX, 90 + rotationY, 0);
    }
    public void HostGame()
    {
        string ID = MainMenuMP.GetRandomID();
        CmdHostGame(ID);
    }

    [Command]
    public void CmdHostGame(string ID)
    {
        matchID = ID;
        if (MainMenuMP.instance.HostGame(ID, gameObject))
        {
            Debug.Log("Лобби было создано успешно");
            networkMatch.matchId = ID.ToGuid();
            TargetHostGame(true, ID);
        }
        else
        {
            Debug.Log("Ошибка в создании лобби");
            TargetHostGame(false, ID);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string ID)
    {
        matchID = ID;
        Debug.Log($"ID {matchID} == {ID}");
        MainMenuMP.instance.HostSuccess(success, ID);
    }

    public void JoinGame(string inputID)
    {
        CmdJoinGame(inputID);
    }

    [Command]
    public void CmdJoinGame(string ID)
    {
        matchID = ID;
        if (MainMenuMP.instance.JoinGame(ID, gameObject))
        {
            Debug.Log("Успешное подключение к лобби");
            networkMatch.matchId = ID.ToGuid();
            TargetJoinGame(true, ID);
        }
        else
        {
            Debug.Log("Не удалось подключиться");
            TargetJoinGame(false, ID);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string ID)
    {
        matchID = ID;
        Debug.Log($"ID {matchID} == {ID}");
        MainMenuMP.instance.JoinSuccess(success, ID);
    }

    public void BeginGame()
    {
        CmdBeginGame();
    }

    [Command]
    public void CmdBeginGame()
    {
        MainMenuMP.instance.BeginGame(matchID);
        Debug.Log("Игра начилась");
    }

    public void StartGame()
    {
        TargetBeginGame();
    }

    [TargetRpc]
    void TargetBeginGame()
    {
        Debug.Log($"ID {matchID} | начало");
        DontDestroyOnLoad(gameObject);
        MainMenuMP.instance.inGame = true;
        transform.localScale = new Vector3(0.41664f, 0.41664f, 0.41664f); //Размер вашего игрока (x, y, z)
        facingRight = true;
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

}
