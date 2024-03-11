iterations: The first thing I did was iterate the actual field of play a couple of times, expanding the size and adding various obstacles, adjusting until I was satisfied.
I then iterated  the variables like movement speed to find a feel that was relatively satisfying for the game.
I had to consult copilot and iterate the actual script several times to get the scoring to work, and even now it occasionally bugs in a way I don't understand, but it seems reliahle for the most part.
I attempted to make a portal game object that would teleport the player on collision, but after hours of revising, following tutorials and consulting copilot I just wasn't able to get it to work and had to scrap the idea.
design tools: I finally settled on a change to goal and skill, adding a collectible that would automatically increment the player's score by 5, but is very difficult to reach due to the parkour required.
These are the changes I made!

UPDATE: I have now added a functional GUI to display scores, as well as win text in the middle of the screen that will display when one player reaches 5 points.

UPDATE: I have added manager systems to manage things such as the player and their inventory, as well as adding an inventory feature which allows the player to collect items which they can then equip (though this currently doesn't do anything). 
Also I added a push force to the player and some free standing box stacks that the player can push over for fun, plus changed the collectibles from adding to the player's score to being collected into their inventory.

UPDATE: The game's objective, rather than jumping over each other to earn points, is to be the first to enter the yellow floorspace after collecting 5 collectibles. In other words, player race to collect collectibles which are scattered across the playing field and added to that player's inventory on contact. Once the player has 5 in their inventory, they will win by touching the yellow area. Also, the ability to jump is not currently present as it is no longer core to the mechanics.