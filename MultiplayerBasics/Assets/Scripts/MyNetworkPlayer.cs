using System.Collections;
using System.Collections.Generic;
using TMPro;
using Mirror;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColourRenderer = null;

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing Name";

    [SyncVar(hook = nameof(HandleDisplayColourUpdated))]
    [SerializeField]
    private Color displayColour = Color.black;

    #region Server
    [Server]
    public void SetDisplayName(string newDisplayName){
        displayName = newDisplayName;
    }

    [Server]
    public void SetDisplayColor(Color newDisplayColour){
        displayColour = newDisplayColour;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName){
        if(newDisplayName.Length < 2 || newDisplayName.Length > 20){
            return;
        }
        RpcLogNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }
    
    #endregion

    #region Client
    private void HandleDisplayColourUpdated(Color oldColour, Color newColour){
        displayColourRenderer.material.SetColor("_BaseColor", newColour);
    }

    private void HandleDisplayNameUpdated(string oldName, string newName){
        displayNameText.text = newName;
    }

    [ContextMenu("SetMyName")]
    private void SetMyName(){
        CmdSetDisplayName("My New Name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName){
        Debug.Log(newDisplayName);
    }
    #endregion
}
