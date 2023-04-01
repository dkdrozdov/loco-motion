# Loco motion platform

## Motivation

Loco motion is a multiplayer-centric platformer game platform (no pun intended). 

It goals to provide a set of tools to develop a game and focuses on the best developer expirience ensuring comprehensive extensibility and customization to fulfill developer's imagination and get a smooth gameplay for a players.

Game performance is achived by following data driven design with allows to achieve efficient event processing, memory layout and leverage parallellism.

## Architecture

Loco motion platform based on C# and OpenTK, and uses client-server model for multiplayer implementation.

### Server

Server is a dotnet C# console application responsible for a 
- physics simulation (including in-game time control)
- game logic execution (custom logic and AI-controlled agents)
- client communication (connection management, outbound events broadcasting ensuring state synchronization, inbound events handling)

Server-side platform includes the following services:
- Network (connection, game state sync, inbound notifications routing)
- Physics and time engine (executes physics and time simulation)
- Game (executes custom game logic)
- Scene (scene objects management, executes queries)
- Event
- AI
- Game object lifecycle manager
- Memory layout
- Log

### Client

Client is a dotnet C# console application powered by OpenTK, it is responsible for a
- graphics (scene rendering, in-game GUI and game menus, camera)
- scene loading and assets management
- player input
- server communication

Client-side platform includes the following services:
- Network
- Assets loader
- Scene loader
- Input
- GUI
- Menu
- Audio
- Camera
- Test (provides all kinds of an in-game graphical debug info and in-game debug tools such as time controller)
- Log

### Tooling

- Assets compiler
- Scene editor (and a viewer)
- Scene compiler

Also learn how to test, debug and profile a server and a client.

# Platform developer guide

This section describes how to setup a development environment, run, debug, test and profile a server and a client.

# Game developer guide

A basic development flow with loco motion breaks down to client-side and server-side development.

## Client side

Here you create a content of your game and a GUI using client's tools and services

- create an assets (audio, textures, sprites and animations) in a supported formats
- declare a scene object prototypes referencing your assets
- declare a scene referencing your prototypes
- customize a GUI and a game menus
- customize a player input reader
- customize a game camera
- add a logs you need

## Server side

Here you define the behaviour of your game using server's services and classes

- define your game object classes by extending the SceneObject class with a custom data, events and behaviour
- define an AI 
- define an additional physics rules
