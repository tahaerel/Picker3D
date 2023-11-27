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

## Level Editor Usage
It can be opened from Window > level editor.

![editor](https://github.com/tahaerel/Picker3D/assets/63150746/1ed83133-57a7-4f30-80b7-337b0910bb23)

Here you can add a new level or edit an existing level. When creating a new level, the last existing level index is taken and a new level is automatically created with the next higher index and name.

![leveltab](https://github.com/tahaerel/Picker3D/assets/63150746/362904a3-b97f-4c2c-b3d5-92a4a400c1d5)

Editor Example:

![editorexample](https://github.com/tahaerel/Picker3D/assets/63150746/563f5bb4-f0ec-4700-bdba-ba07a8838fd0)

Of course you can edit the level by clicking on it and you can also create a level from the right click > create > levels panel.

## Research Questions

### Which Rollic games ranked #1 on the US Top Chart?
Power Slap,Pressure Washing Run,Engine Pistons ASMR,Gear Clicker,Balls'n Ropes,Lifting Hero,Class Trivia,Colors Runners!,Car Lot Management!,Crowd Evolution!,Level Up Cars,Parents Run!,Office Fever,Phone Evolution,They Are Coming,Money Honey!,Text Or Die,Nail Stack!,Block'em All,Arrow Fest,Queen Bee!,Hit Guys,Weapon Cloner,High Heels!,Hair Challenge,Blob Runner 3D,Gear Race 3D,Drive Thru 3D,Build Roads, Tangle Master 3D,Repair Master 3D,Go Knots 3D,Touchdrawn,Wheel Smash,Color Circles 3D,Water Shooty,Fit And Squeeze,Picker 3D,Pixel Shot 3D,Onnect – Pair Matching Puzzle

### Which Rollic games are developed with Stacking mechanics?
Color runners, Moneyland, Hand evolution runner, Gem stack, Swords maker, Candle gift, High heels, Coffee stack, I want pizza, Nail stack!, Atm rush, They are coming, Smash runner, Couple shuffle ...

### Compare Office Fever and Fill the Fridge! in terms of game mechanics
**Office Fever** is an idle game in which we manage an employee in a work environment. The basic mechanic of the game is to collect documents from the printer and place them on the workers desks, then collect the moneys earned. These moneys can be used to buy new machines, desks, employees and to upgrade our character and the workers who work for us.  
 From the 3rd person camera, the character is moved by dragging a finger across the screen. The character moves according to the direction of the finger drag. The document & money collection and placement mechanics are actions that are triggered when the character moves from their current position into the boundaries of certain areas.  
 
**Fill The Fridge** is a refrigerator organizing game. Our goal is to fill the fridge with products in an organized way. 
Unlike Office Fever, we don't have a character. The touch controls whether a new object can be created or removed. It is checked whether the item to be placed collides with other items and whether it has enough space.

In both games, process are made according to area-based triggering. 
Example:
Office Fever -> Is the player in the paper collection area? If yes, collect the papers.
Fill The Fridge -> Are there other objects in the area of the object the player will place in the fridge and is this area big enough? If there is enough space and it doesn't hit another object, place the object.

### Which Rollic game did you reach the highest level, and which level did you reach?
The Rollic game where I reached the highest level is Bus Jam. I played about 110 levels.
