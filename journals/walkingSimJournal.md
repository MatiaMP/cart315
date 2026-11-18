Matia's Walking Sim Journal

As an avid enjoyer of horror, whether it be horror games or horror films, I want to make a spooky walking sim, not necessarily scary, but for the feel of the sim to be eerie.

My first thought is to make the walking sim be a maze. There is something so eerie about mazes that make this feel like the right approach. Being alone in an unknown area full of dead ends, trying to reach the end. And the idea of adding elements of spookiness is what really draws my attention. Adding sound effects around the maze such as screaming, whispers, maybe some wind sound effects, some eerie music, etc.

I want it to be a walking sim that also has some sort of interaction and thought to it, not just a literal walking sim where you walk through an area with no purpose.

The maze will contain brick walls with dead ends, the user must find their way through the maze and out to an ending. There will be something at the end of the maze that is playing certain sounds, and when approaching it, it will get louder and when right in front of it, it will end the sim.

I want to give some sort of sound cue or visual cue when a user ends up in a dead end.

I found some music that I find suits the maze and that will be playing during the whole walkthrough. An error sound cue  and a red light will also trigger whenever a player walks into a dead end.

I will not add screams because I think that is a little too much and I added whispers, using spatial sound to make it really sound like they are around the maze. I mentioned earlier that being alone in a maze is spooky, and hearing whispers around makes it feel less lonely but gives an even scarier atmosphere.

For the ending, I've added different music that plays and three pedestals that are glowing, with a bell sound effect playing on loop from each of them, when you get close you hear it more, and the spatial audio makes it so that when you get closer it gets louder. When you finally approach it, the game ending will trigger and return you back to the start.

For the part 2 of my walking sim, I brainstormed a few ideas. One being adding pressure plates around the maze that open certain areas of the maze. The others being more environmental/atmospheric, such as fog, making the scene darker, or more sound effects.

For the pressure plates I changed my mind and will not only add one type of pressure plate but two different ones. One that will open up a wall for the user to advance, and one that will bring you back to the start of the maze. They will look identical so that it adds a guessing game for the player.

For the environmental change, I will add fog using the Rendering and Lighting section, adjusting the fog density and colour to black, giving an even eerier feel to the game.

To add the pressure plate feature, I feel like I need to expand the maze as the original is too small. I will make a 2nd part of the maze, with good and bad pressure plates, with the probability of going on a bad pressure plate a lot higher than going on a good one.

To build the pressure plates without writing code, I used the State Machine. Each plate is a flattened cube with a trigger collider and a kinematic Rigidbody so the player can walk onto it and fire an event. Each plate has an object variable called TargetWall, a game object so I could reuse one graph for every plate and drag  a different wall into each plate's variable (for the good plate). In the Pressed state, an On Enter State event calls Set Active on the TarhetWall variable with the value set to false, which removes the wall and opens a path.

I realized also that stepping on a bad pressure plate and sending the player back to the start is too extreme, so instead I will send the player to the start of the second part of the maze.

For the bad plates, I duplicated the graph so the good plates wouldnt change. I changed the Pressed state to where it finds the player with Find With Tag, disables the Character Controller and uses Set Position to move the player to the start of the second part.

