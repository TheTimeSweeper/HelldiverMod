# The most important decision of your life
### Project Setup
We're using Thunderkit to some capacity in order to make UI and Projectile development not a pain in the ass.

Buliding the assetbundle and building the mod will remain done how it is done in the Henry template.

0. Clone the repo and open the unity project.  
Thunderkit, RoR2ImportExtensions, and SimplyAddress should already be in the packages manifest, and they'll install on first startup.  
If not, reach out, because the project won't work from here on out.
1. On the top hit Tools > Thunderkit > settings.  
If you don't see this, the thunderkit installation did not happen correctly. Again, look for errors, reach out, we gotta fix that
2. On the left of the window, hit Import Configuration
3. Configure as follows (it should already be set to these options, but just double check)

|Configuration | Set To | Why |
|---|-|-----|
|Check Unity Version | On | |
|Disable Assembly Updater | On | |
|Post Processing Unity Package Installer | Off | already done in manifest |
|TextMeshPro Uninstaller | Off | already done in manifest |
|Unity GUI Uninstaller | Off | We need the editor tools otherwise we're going to be huge masochists. This option would have destroyed our ability to do any UI, which we need to be able to do |
|Assembly Publicizer | leave as is | you should have NStrip in the field, and the assemblies should be RoR2.dll and KinematicCharacterController.dll |
|MMHook Generator | Off | we're not building the mod from the editor so we don't need extra bloat |
|Import assemblies | On |  |
|Import Project Settings | Everything |  |
|Set Deferred Shading | On |  |
|Create Game Package | On |  |
|Import Addressable Catalog | On | addressable browser is dope, we're using it to take a look at the game's hud to add our hud |
|Configure Addressable Graphics Settings | On |  |
|Ensure RoR2 Thunderstore Source | Off | might cause hangs. we don't need this |
|Install BepInEx| Off | already done |
|R2API Submodule Installer | Off | ah yes who wouldn't want 28 packages slowing down compiling, playing, and building? |
|Install RoR2 Compatible Unity Multiplayer HLAPI | Off | we're not weaving in editor so we don't need this |
|Install RoR2 Editor Kit | Off | I would love to use it, but I haven't tested what it's like or what we need to make it work or what problems it might cause. for now we stay henry-adjacent |
|the rest | On |  |

Note that these differ from the default thunderkit project settings outlined in other tutorials. we're only taking what we need for this project.

4. Head to the ThunderKit Settings on the left. hit browse on the right, locate your ror2 executible, then hit import
5. A prompt will ask you to restart. I would hit yes
6. let thunderkit do its thing
7. All goes smoothly, another prompt will ask you to restart again. I would hit yes again
8. This is where we find out if we fucked everything up. If the unity project opens again, we should be in the clear.  
There is a chance it won't open at all. This is because of the dangerous dance we're playing by keeping Unity UI in the project, which we need for stratagems.  
If this happens, reach out I suppose. I've tested fresh clones of the repo and they have worked. Perhaps delete the whole repo, clone it again, and try again.
9. Hopfeully none of that is an issue and you got yourself a working project!
10. Open the Scene in the Scenes folder and click on the HUD gameobject. If all's well with SimplyAddress, the game's HUDSimple.prefab should have loaded under this, and our HellDiverUI should have spawned where we want it to spawn in game
11. Before you continue, head to your git repo and take a look at your changes. You should have a few lingering changes from the import. Feel free to discard these changes, the project should be good to go without them.  

That should be it, thanks thanks love ya
