# The most important decision of your life

### upgrading from before sots
if your project existed, follow this before you open the project again (or simply reclone from scratch)

0. Pull the latest. this should basically update your ror2 packages thunderkit settings, and package manifest.
1. go to the unity project files Packages folder
1. delete the `Risk of Rain 2` folder and `packages-lock.json`
1. reimport the game as below

### Project Setup
We're using Thunderkit to some capacity in order to make UI and Projectile development not a pain in the ass.

Buliding the assetbundle and building the mod will remain done how it is done in the Henry template.

0. Clone the repo and open the unity project.  
    - You'll get a warning that there are compile errors. Hit Ignore (do not enter safe mode).
    - Thunderkit, RoR2ImportExtensions, RoR2EditorKit, and SimplyAddress should already be in the packages manifest, and they'll install on first startup.  
        - If not, reach out, because the project won't work from here on out.
1. On the top hit Tools > Thunderkit > settings.  
    - If you don't see this, the thunderkit installation did not happen correctly. Again, look for errors, reach out, we gotta fix that
2. On the left of the window, hit Import Configuration
3. Configure as follows

|Configuration | Set To | Why |
|---|-|-----|
|Check Unity Version | On | |
|Disable Assembly Updater | On | |
|Post Processing Unity Package Installer | Off | already done in manifest |
|Assembly Publicizer | leave as is | you should have NStrip in the field, and the assemblies should be RoR2.dll and KinematicCharacterController.dll |
|MMHook Generator | Off | we're not building the mod from the editor so we don't need extra bloat |
|Import assemblies | On |  |
|Import Project Settings | Everything |  |
|Set Deferred Shading | On |  |
|Create Game Package | On |  |
|Import Addressable Catalog | On | addressable is dope, we're using it to take a look at the game's hud to add our hud |
|Configure Addressable Graphics Settings | On |  |
|Ensure RoR2 Thunderstore Source | Off | thunderstore package downloading is brokey I think |
|Install BepInEx| Off | already there. |
|Install RoR2 RoR2MultiplayerHLAPI | Off | we're not weaving in editor so we don't need this |
|Install RoR2 Editor Kit | OFF | already there. do not add a duplicate of this as it will cause issues. |
|the rest | On |  |  

- Note that these differ from the default thunderkit project settings outlined in other tutorials, because the project is already partly set up, and we're using thunderkit minimally.

4. Head to the ThunderKit Settings on the left. hit browse on the right, locate your ror2 executible, then hit import
5. A prompt will ask you to restart. hit yes
6. let thunderkit do its thing. 
    - if you get windows about compile errors again, Ignore as usual.
7. All goes smoothly, another prompt will ask you to restart again. I would hit yes again
8. This is where we find out if we fucked everything up. If the unity project opens again, we should be in the clear.  
9. Hopfeully none of that is an issue and you got yourself a working project!
10. Open the Scene in the Scenes folder and click on the HUD gameobject. If all's well with SimplyAddress, the game's HUDSimple.prefab should have loaded under this, and our HellDiverUI should have spawned where we want it to spawn in game
11. Before you continue, head to your git repo and take a look at your changes. You should have a few lingering changes from the import. Feel free to discard these changes, the project should be good to go without them.  

That should be it, thanks thanks love ya
