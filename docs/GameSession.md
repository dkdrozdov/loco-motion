# Game session flow

Roles:
- host (client in host mode) - manages a server process, controls a game session
- server - communicates with a host and clients, serves as a backend for a lobby and a game session
- player - connects to a server and communicates it following the protocol

Host and player roles are supported by a client application.

The game session breaks down into following phases.

## Lobby creation

Host player:
- configure a lobby (eg set a name, password)
- host a lobby

Host:
- spawn a server
- connect to the server
- waiting for the server initialized notification
- send lobby configuration data
- waiting for lobby initialized notification

Server:
- initialize
- waiting for a host to connect
- notify initialized
- wait for a lobby configuration
- initialize a lobby
- notify lobby initialized

## Session configuration

Game session is configured by a host and players via lobby GUI.

Host player:
- configure game session params (eg select a scene, game rules)
- configure player params
- wait for other players to enter a lobby
- wait for other players to confirm readiness
- press a start button

Host:
- wait for a start request
- request session initialization

Client player:
- open a lobby explorer
- connect to the lobby server
- configure player params (eg select an agent)
- confirm a ready state

Server:
- handle player connection requests
- wait for a session initialization request

## Session initialization

Server:
- request host for the game session configuration
- request each player (including host player) for player configuration
- wait for scene data
- initialize scene
- broadcast initial scene data to clients
- start session execution

Host:
- load scene
- send scene data to server

Client:
- wait for initial scene data
- initialize scene
- start player controller
- render gui
- start scene rendering
- start session execution

## Session execution

Server:
- execute session loop

Client:
- execute session loop

## Finilization
