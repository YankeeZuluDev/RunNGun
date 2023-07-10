# RunNGun
A repository for "Run N Gun: Merge" game source code

[:camera: **See Screenshots**](#screenshots)

[:movie_camera: **See gameplay video**](https://www.youtube.com/watch?v=uu2AngDUh1s)

[:video_game: **Play**](https://play.google.com/store/apps/details?id=com.YankeeZulu.RunNGun)

## About the game
Genre: Hyper-casual

Mechanics: running, shooting, merging

Unity version: 2021.3.18f1 (LTS)

Accessibility: Project can be freely explored in unity

## Screenshots

<div style="display:flex;">
  <img src="https://user-images.githubusercontent.com/129124150/230711931-dc82b082-d3b2-4878-a8ad-6f046a1bdceb.jpg" alt="screenshot_1" width="270" height="480">
  <img src="https://user-images.githubusercontent.com/129124150/230711970-45ec7c31-1996-43eb-b313-b547ff35ba8f.jpg" alt="screenshot_2" width="270" height="480">
  <img src="https://user-images.githubusercontent.com/129124150/230711976-99469490-2178-48a3-8ea2-95858d799111.jpg" alt="screenshot_3" width="270" height="480">
  <img src="https://user-images.githubusercontent.com/129124150/230711980-4272fdfb-7d16-4e4d-889b-61de90dbf6e3.jpg" alt="screenshot_4" width="270" height="480">
</div>

## Best сode practices in this project

### Object pooling
This game uses object pooling to efficiently manage and reuse bullet objects within the game. Object pooling minimizes the overhead of creating and destroying bullet objects dynamically, resulting in improved performance and reduced memory allocation. [Code](https://github.com/YankeeZuluDev/RunNGun/blob/main/Assets/Scripts/Pools/BulletPools.cs)

### Split responsibilities
Class responsibilities in this project are well defined and separated. Each class is responsible for only one thing. [Code](https://github.com/YankeeZuluDev/RunNGun/tree/main/Assets/Scripts/Player)

<div style="display:flex;">
  <img src="https://github.com/YankeeZuluDev/RunNGun/assets/129124150/93b766ab-e3df-4cab-8ec2-8e041730c74e" alt="screenshot_5" width="450" height="566">
  <img src="https://github.com/YankeeZuluDev/RunNGun/assets/129124150/973027ac-df4e-424a-9c47-af1592f2365c" alt="screenshot_6" height="566">
</div>

### Game event system
This game uses an event system to handle in-game events such as GameStartEvent or GameWonEvent. The event system is implemented uisng ScriptableObjects, making it simple, convinient and extendible. The event system consists of 2 classes: GameEvent and GameEventListener. [GameEvent class](https://github.com/YankeeZuluDev/RunNGun/blob/main/Assets/Scripts/Events/GameEvent.cs) provides a way to create custom game events that can be triggered by different components. It allows for flexible event handling and communication between different parts of the game. [GameEventListener class](https://github.com/YankeeZuluDev/RunNGun/blob/main/Assets/Scripts/Events/GameEventListener.cs) is responsible for listening to a specific GameEvent and triggering a UnityEvent response when that event is raised. GameEventListener can be attached to any gameobject. Event system is implemented using [observer pattern](https://en.wikipedia.org/wiki/Observer_pattern).

### Custom level constructor window
This project has [custom editor window](https://github.com/YankeeZuluDev/RunNGun/blob/main/Assets/Editor/RunnerLevelConstructor.cs) that is used to construct levels and save them levels. This approach provides convinient and flexible way for level creation. Levels are stored as [LevelDefinition](https://github.com/YankeeZuluDev/RunNGun/blob/main/Assets/Scripts/LevelLoading/LevelDefinition.cs) ScriptableObjects. LevelDefinition class consists of 3 parts: the length of the level, the width of the level and an array of [data classes for spawnables](https://github.com/YankeeZuluDev/RunNGun/blob/main/Assets/Scripts/Data/SpawnableData.cs), that contain all the necessary information to instantiate spawnables at runtime
<div style="display:flex;">
  <img src="https://github.com/YankeeZuluDev/RunNGun/assets/129124150/1b408416-708b-454b-802d-d5cd50e9c5c9" alt="screenshot_7">
</div>

### Load time dependency injection


### Static classes

