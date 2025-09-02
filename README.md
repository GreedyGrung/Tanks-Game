![image](/Documentation/tanks_game_0.png)
# Tanks Game 
This was once a test task, and now it's my demo project :)

This is a 2D top-down shooter where the player controls a tank and must destroy enemy tanks and turrets.

The game's architecture is based on a **finite state machine**. 
**GameBootstrapper** class is an entry point to the game.

Let me highlight some important features in the project:

* **Unity Input System** is used to detect player input
* **Object Pooling** is used for frequently spawned objects
* **Factories** are used to create game objects and UI panels
* **Zenject** is used to resolve dependencies
* **Scriptable Objects** are used to store game configurations
* **Addressables** are used to store and load assets
* Simple **Finite State Machine** is used to create enemies behaviour

A few words about design patterns which are used in the project:
* Object Pool - spawning projectiles
* State (FSM) - a separate machine for handling the global game state and a separate machine for enemies behaviour
* Observer - C# events in multiple classes
* Mediator - connecting game events with the UI service
* DI - no Singletons or Service Locator, just good old Zenject
* Entry Point - must-have for every project
* Abstract Factory - creating game objects

Non-MonoBehaviour logic is separated into independent **services**.
Of course, there are many aspects of this project that can be improved. And they certainly will be (when I have a little more time). But for now, this project is good for demonstrating some code.
