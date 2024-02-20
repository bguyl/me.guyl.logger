# Unity GLogger

> Better logger for Unity

## Description

GLogger is a Unity logger that offer more granular logs, channels management and an easier flexibility / customisation,
while keeping default Unity's logger features.  


## Short demo

```csharp
// GDebug inherit from UnityEngine.Debug, so used it as you would normally

// Add more logging levels...
GDebug.LogVeryVerbose("This is a VeryVerbose message"); // No stacktrace
GDebug.LogTrace("This is a Trace message"); // When you want verbose logging
GDebug.LogDebug("This is a Debug message"); // Logs only enabled during development
GDebug.LogInfo("This is an Info message"); // Logs enabled in a released game
GDebug.LogCritical("This is a Critical message"); // An error that should break your game
// ... and keep the existing ones
GDebug.Log("This is also an Info message");
GDebug.LogWarning("This is a Warning message");
GDebug.LogError("This is an Error message");

// Add optionnal channels managments
GDebug.LogInfo("GameplaySystem", "Player was hit");                       // INFO [GameplaySystem] Player was hit
GDebug.LogInfo("SaveOperation", "A save was requested");                  // INFO [SaveOperation] A save was requested
GDebug.LogWarning("SaveOperation", "Save ignored, target was not ready"); // WARN [SaveOperation] Save ignored, target was not ready
```

## Installation

There is 3 installation methods: 

From OpenUMP

```
openump add me.guyl.glogger
```

## Configuration

````
````

## Others

Wood icons created by Good Ware - Flaticon