# Picker3D

A clone of Picker 3D game. Done in 3 days.

## Gameplay video via editor

https://github.com/tahaerel/Picker3D/assets/63150746/4f01cc5e-b335-426e-86f0-73f15304118c

The apk can be accessed from the release file.

## Fundamentals

![levelgenerator](https://github.com/tahaerel/Picker3D/assets/63150746/0183141b-e342-4d37-9ae7-18cac2771a89)

The last level completed by the player is saved. If you want to start a specific level, enter the value in Current Level and check the Change The Level checkbox.

![Screenshot_11](https://github.com/tahaerel/Picker3D/assets/63150746/53956754-a120-427a-9796-bd169b11f2c6)


**Editor:** Level editor and shader script for skybox.  
**Environment:** Codes for Gate, MovingPlatform and Spinner  
**Level:** In this project scriptable objects were used for levels. It contains the necessary codes for registering scriptable objects and creating levels in the game.  
**Managers:** Controllers for Audio, Camera, Game, Singleton and UI.  
**Player:** Character's movements and interactions

**!! Important: Random levels are called for non-existent levels. This is why the game goes on forever.**

## Level Editor Usage
It can be opened from Window > level editor.

![editor](https://github.com/tahaerel/Picker3D/assets/63150746/1ed83133-57a7-4f30-80b7-337b0910bb23)

Here you can add a new level or edit an existing level. When creating a new level, the last existing level index is taken and a new level is automatically created with the next higher index and name.

![leveltab](https://github.com/tahaerel/Picker3D/assets/63150746/362904a3-b97f-4c2c-b3d5-92a4a400c1d5)

Editor Example:

![editorexample](https://github.com/tahaerel/Picker3D/assets/63150746/563f5bb4-f0ec-4700-bdba-ba07a8838fd0)

Of course you can edit the level by clicking on it and you can also create a level from the right click > create > levels panel.

If you specify more level indices than the number of levels, a warning window opens in the editor.

![editorhata](https://github.com/tahaerel/Picker3D/assets/63150746/60cb774a-81cc-4f91-9b90-cef5eb2fc57f)

For example: If there are 5 levels, the last level index should be 5.
