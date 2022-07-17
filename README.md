# Loco motion

Mutiplayer platformer engine.

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