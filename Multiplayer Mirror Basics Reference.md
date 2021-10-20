# Multiplayer Mirror Basics Reference

## Client-Server Model
Input and State
State:
	Data, data about where your player is, health, etc

It's all the data that all game objects store inside components.

Actions: input ie. pressing space bar to change x/y

Update:
	check for actions and changing state

Types of Multiplayer
Peer to Peer
Client-Server

## Network Manager
One component it uses for Transport is Telepathy by default. 
- Default way to send data between client and the server.
Network address: who we are connecting to
- Default it is localhost
Player object: holds existence
- Used to store name, team color, money, etc

Add a network identity component for objects that need to be networked
- i.e. the player needs to have a network identity component

Virtual Functions
- Functions that can be overridden, note override NetworkBehavior to inherit its functions for multiplayer.

When a Client Connects (Client)

- OnStartClient()
- Called when you click the client button when trying to start a connection.
OnClientConnect
- Called when you've connected to a server, this might not always happen ie. when there is no server to connect to. So it is only called on successful connection. 

When a Client Connects (Server)

- OnServerConnect()
- Called on the server when a client connects
OnServerReady
- There is a small period of time before new clients are ready for data transfer, this is for if anything is needed during this time.

(Create Player)
- Sets up player prefab we designate
OnServerAddPlayer
- Once player is created it'll trigger a callback for OnServerAddPlayer, which signals that there is now a player

## Syncing Variables
Network Behaviors and SyncVars
- When events/ actions happen in a network you need all clients in the game to recieve updates.
- In order to do so you can make a game object as networked, by changing it to inherit network behavior instead of monobehavior. 
- Makes certain variables on that component network variables or sync, where if it is updated on the server it will be updated on all clients. With certain limitations. 
- Objects using networkbehaviour need to exist on a network game object, ie. objects with network identity component.

SyncVar
- To sync variables add [SyncVar] before a field, which will be used to synchronize variables from the server to all clients automatically.
- SyncVars must be changed on the server not by client.
[Server] - Prevents clients from running a method

Limits on SyncVar
https://mirror-networking.gitbook.io/docs/guides/data-types

## SyncVar Callbacks
SyncVar: 
https://mirror-networking.gitbook.io/docs/guides/synchronization/syncvar-hooks

## Remote Actions
- Executing logic remotely, not on the same machine
- Pressing a button to execute on the server.

[Command] CmdDoSomething() For clients calling a method on the server
[ClientRpc] RpcDoSomething() For the server calling a method on all clients
[TargetRpc] TargetDoSomething() For the server calling a method on a specific client

## Server Authority

- Handled by Mirror
- Idea is that trying to set command objects without authority due to client not owning the object. 
- Server authority is the server authorizes what happens, clients say what they would like to do, but the server at the end of the day decides on what can happen and its results.
- Most client code is telling what you intend to do.
- Server code is actually doing the stuff.
-[SyncVar] are designed so that if a client tries to change it, nothing happens. If a server changes it, it tells all clients.

## Network Transform
- Keep track of transform of objects by adding a network transform component