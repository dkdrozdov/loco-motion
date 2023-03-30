# Engine

Engine is multiplayer-centric.

- multiplayer
- scene loading
- scene rendering
- scene queries api
- game objects lifecycle management
- physics
- custom game logic components support
- main loop (game logic execution order)

## Server main loop

- recieve commands from player connections
- create shadow scene
- execute lifecycle manager requests and call lcm events, write changes to shadow scene
- execute physics, call physic events, write changes to shadow scene
- execute command events, write changes to shadow scene
- build snapshot from shadow scene and consider shadow scene an actual scene state
- broadcast scene snapshot

## Client main loop

- scan user input
- send commands
- recieve (e.g., snapshot)
- apply snapshot to scene
- render scene

## Protocol

Messages from client to server.

Message
- kind
- id
- payload

ClientMessage
- kind (connection, command)

ConnectionClientMessage
- id (disconnect)

CommandClientMessage
- id (user-defined, e.g., move, fire, reload)

ServerMessage
- kind (connection, update)

ConnectionServerMessage
- id (disconnect)

UpdateServerMessage
- id (scene update)
- data (snapshot)

## Entities

Scene.

- specify scene file format
- scene in-memory representation
- load(scene)
- render()
- change state

Scene state consist of
- mesh (defines game space with (x, y) boundaries)
- platform (platforms are defined by scene file and determine scene geometry; they are static obstacles)
- dynamic object (contain state, could be added/removed/change state)
- agent (dynamic object controlled by user/ai, could initiate interactions with DOs)
- special effect
- doodad (no state, just renderable)
- link (off-mesh link)

Game logic.

tick(scene) // returns new scene state

- physics (gravity, collisions)
- game rules (win condition)

Command.

- create(user_input)

Action.

- from_command(command)

Player (networking-related and info-related).

- connection
- info (nickname, status, bandwidth)

Agent.

- 

## Server event loop

Server listening messages from client and enqueing them

- messages processing
- actions commits
- evaluate response
- broadcast response

## Client event loop

Server listening

- process messages
- apply mutations locally

User listening

- snapshot command
- send messages

## Client-server communication

Client to server

- send message (command)
- 