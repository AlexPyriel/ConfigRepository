# GDF Config Repository

ScriptableObject-backed config repository for Unity projects that need a strict `key -> config.Id` invariant.

Package name: `com.gdf.config-repository`  
Assembly name: `GDF.ConfigRepository`

## Features

- Public config model: `Config<TKey>`.
- Central storage layer: `ConfigRepository<TKey, TConfig>`.
- Read access contract: `IConfigReader<TKey, TConfig>`.
- Write access contract: `IConfigWriter<TKey, TConfig>`.
- Combined read/write contract: `IConfigProvider<TKey, TConfig>`.
- Key-to-identity synchronization through repository writes and validation.

## Requirements

- Unity `6000.0+`
- `AYellowpaper.SerializedCollections`

## Installation (UPM via Git)

Add this dependency to `Packages/manifest.json`:

```json
"com.gdf.config-repository": "https://github.com/AlexPyriel/ConfigRepository.git#v1.0.0"
```

For development tracking (not recommended for production), use `#main` instead of a tag.

You can also install it from Unity Package Manager:

- `Window -> Package Manager -> + -> Add package from git URL...`
- `https://github.com/AlexPyriel/ConfigRepository.git#v1.0.0`

## Usage

Store configs in a repository and resolve them by the same key that is mirrored into the config `Id`.

```csharp
if (_repository.TryGet(SceneType.Game, out SceneConfig config))
{
    var reference = config.SceneReference;
}
```

## Assembly Definition Notes

`GDF.ConfigRepository` is a dedicated runtime assembly.

The repository API is intended to be referenced directly by host-project assemblies that consume `Config<TKey>` and `ConfigRepository<TKey, TConfig>`.

EditMode tests live under `Tests/EditMode` and use a separate test assembly definition.
