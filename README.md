
1. Window > Package Manager
2. On the top left, click the +, add from git url
3. install the following packages  
git@github.com:PassivePicasso/ThunderKit.git  
git@github.com:risk-of-thunder/RoR2ImportExtensions.git  
optionally, git@github.com:PassivePicasso/SimplyAddress.git

|Configuration | Set To | Why |
|---|-|-----|
|Check Unity Version | On | |
|Disable Assembly Updater | On | |
|Post Processing Unity Package Installer | On |  |
|TextMeshPro Uninstaller | Off |  |
|Unity GUI Uninstaller | Off | We need the editor tools otherwise we're going to be huge masochists. This option would have destroyed our ability to do any UI, which we need to be able to do |
|Assembly Publicizer | leave as is| you should have NStrip in the field, and the assemblies should be RoR2.dll and KinematicCharacterController.dll |
|MMHook Generator | Off | we're not building the mod from the editor so we don't need extra bloat |
|Import assemblies | On |  |
|Import Project Settings | Everything | nice to have layers n shit |
|Set Deferred Shading | On | if you want game shaders in editor. make sure you gitignore them and don't build with them |
|Create Game Package | On |  |
|Import Addressable Catalog | On | addressable browser is dope, we're using it to take a look at the game's hud to add our hud |
|Configure Addressable Graphics Settings | On | if you want game shaders in editor |
|Ensure RoR2 Thunderstore Source | Off | might cause hangs. we don't need this |
|Install BepInEx| On | needed for addressable browser |
|R2API Submodule Installer | Off | ah yes who wouldn't want 28 packages slowing down compiling, playing, and building? |
|Install RoR2 Compatible Unity Multiplayer HLAPI | Off | we're not weaving in editor so we don't need this |
|Install RoR2 Editor Kit | On | let's fuckin try it |
|the rest | On|
