using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColorRenderer = null;

    [SyncVar(hook =nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing Name";

    [SyncVar(hook =nameof(HandleDisplayColorUpdated))]  // 색이 업데이트 되면 괄호안의 함수가 call
    [SerializeField]
    private Color displayColor = Color.black;


    #region Server
    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server]
    public void SetDisplayColor(Color newDisplayColor)
    {
        displayColor = newDisplayColor;
    }

    [Command] //클라이언트에서 서버의 method를 실행
    private void CmdSetDisplayName(string newDisplayName)
    {
        if(newDisplayName.Length < 2 || newDisplayName.Length > 20) { return; }
        RpcLogNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("SetMyName")] //우클릭 메뉴에서 SetMyName을 선택하면 밑에 함수 실행 (테스트용)
    public void SetMyName()
    {
        CmdSetDisplayName("M");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }
    #endregion
}
