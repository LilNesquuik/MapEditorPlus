# MapEditorPlus - Object & Schematic Creator

**MapEditorPlus** is a fork of **[ProjectMER OR MapEditorReborn](https://github.com/Michal78900/ProjectMER)**, originally created to support a specific project.
This fork focuses on implementing additional features, internal improvements, and project-specific adjustments that are not part of the upstream scope.

> [!WARNING]
> This fork are not fully compatible with the original plugin, and some features may not work as original behavior.

---

## 🗃️ Features
*Here is list of features that are not in the original plugin*

### Objects
- 📦 **Clutter boxes**
  - Allows creating **wooden clutter boxes** that can be found in the **HCZ**
- 🤳🏻 **Camera Toy**
  - Allows spawning cameras with different types and configurations
- ⚡ **Generator**
  - Allows spawning generators via the toolGun
- 🦫 **CapybaraToy**
  - Allows spawning a Capybara and you can enable or disable its collision
- 🧿 **Trigger Zone**
  - In your schematic, or directly in the game, create an `Trigger` 
  - You can define the effect that will be applied when a player enters the trigger zone or let it empty to do nothing
  - You can also configure trigger type, choose between `Enter`, `Stay` or `Exit`  
  - An event also exists, subscribe to `PlayerTriggerEventArgs` and make your own logic
- ⛓️‍💥 **Link Object**
  - In your schematic, create an empty object and add the `LinkComponent` to it
  - If you move an object close to an object with `LinkComponent`, it will be linked to it
  - It will improve the map making experience

### Internal improvements
- 🚗 **WheelColliders** Support
  - Configure your wheel colliders and it will be compiled in the schematic
  - Useful if you wanna make a vehicle but to lazy to hardcode the wheel colliders values
- 📝 **TMP FontStyles** Support
  - Too lazy of writing unity rich text tag ? Use the native unity font styles buttons
  - At the compile time, it will convert the unity font styles to the unity rich text tag
  - But... in the other way, if you decompile an `TextToy`, it will use the unity rich text tag

---

## 📬 Installation
- Put your [`MapEditorReborn.dll`](https://github.com/LilNesquuik/MapEditorPlus/releases/latest) file in `LabAPI/plugins` path.
Once your plugin will load, it will create directory `LapAPI/configs/ProjectMER`; This directory will contain two sub-directories **Schematics** and **Maps**

--- 

## 🧩 Unity Editor
*To create your schematic we have also an forked version of the Unity Editor, that is available [here](https://github.com/LilNesquuik/MapEditorPlus-UnityEditor/tree/production)*
> [!IMPORTANT]
> Make sure to stay on the branch **[Production](https://github.com/LilNesquuik/MapEditorPlus-UnityEditor/tree/production)**. the main branch are the original version of the editor.

## 🗺️ Roadmap
*This section is dedicated to the features that are planned to be implemented in the future.*

- **ProjectMER.VisualScripting** 
  - A visual scripting system for Project MER. 
  - Linking interactables and objects to create complex interactions without writing code.

## 🙏🏻 Credits
- Original Plugin made by **[Michal78900](https://github.com/Michal78900)**
- Full Credits [here](https://github.com/Michal78900/ProjectMER?tab=readme-ov-file#credits)