# Team Blight
**GT Bus Simulator 2019 Game of the Year Edition**

## Members
Name|Email|OIT Account name  
-|-|-
Brooke White|bwhite66@gatech.edu|bwhite66
Katie Cox|kcox40@gatech.edu|kcox40
Lindsey Stowell|lstowell3@gatech.edu|lstowell3
Lucas Liu|lucasliu@gatech.edu|
Nichole Deeb|ndeeb3@gatech.edu|ndeeb3

# GT Bus Simulator
## Installation requirements
We use Unity 2018.3.0+.  
No other components are required to install the game
## Gameplay instructions
Button | Usage
-|-
W/Up Arrow|Accelerate Bus
S/Down Arrow | Decelerate Bus
A/Left Arrow | Turn left
D/Right Arrow | Turn right
Q | Throw Pizza Lure to left
E | Throw Pizza Lure to right
X | Handbrake
L-Ctrl | Speed boost
B | Toggle route view
## Rubric Requirements Satisfied
* 3D Game Feel
  * Game is in 3D
  * Goal of collecting students is clearly defined
  * Victory/Loss screen on win/failure
  * Start Menu implemented as initial scene
  * Ability to restart game on win/loss
  * Not an FPS
* Precursors to Fun Gameplay
  * Goals/Sub-goals are indicated on Instructions screen
  * Interesting choices of whether to go fast and risk hitting students or go slow and get a lower score, etc. 
  * Consequences of hitting players with increased negative scores, hitting objects of reduced health
  * Vehicle to control, ramps to interact with, as well as cars/students to avoid
  * No hollow decisions or fun-killers
  * Hitting cars/students/objects is punished, successfully picking up a student is rewarded
* 3D Character/Vehicle with Real-Time Control 
  * 

## Deficiencies/Known Bugs
* Students sometimes do not have complete collision with the bus (i.e. does not actually completely touch the bus), but the player is still given a mark on their license and docked points.
* Could have extensive performance improvements, such as student spawners rather than all of them being placed on the map. As it currently is, the game is a little slow, especially on lower-end computers
## External Resources Used
### 3D Models/Unity Assets
* [Supercyan Character Pack Free Sample](https://assetstore.unity.com/packages/3d/characters/humanoids/character-pack-free-sample-79870)
* [Standard Assets](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-32351)
* [Unity Vehicle Tools](https://assetstore.unity.com/packages/essentials/tutorial-projects/vehicle-tools-83660)
* [Cartoon Vehicles Lowpoly](https://assetstore.unity.com/packages/3d/vehicles/land/cartoon-vehicles-lowpoly-122412)
* Y_Bot from previous milestones
* [Low Poly Street Pack](https://assetstore.unity.com/packages/3d/environments/urban/low-poly-street-pack-67475)
* [Low Poly Modern City Decorations](https://assetstore.unity.com/packages/3d/environments/urban/lowpoly-modern-city-decorations-set-66070)
* [Industrial Props Kit](https://assetstore.unity.com/packages/3d/props/industrial/industrial-props-kit-84745)
* [Adorable Food Set](https://assetstore.unity.com/packages/3d/props/food/adorable-3d-food-set-31249)
* [Low Poly Fantasy Clouds](https://assetstore.unity.com/packages/3d/environments/awesome-low-poly-fantasy-clouds-97654)
* Polygonal Particles
* [Trash Can](https://assetstore.unity.com/packages/3d/props/exterior/trash-can-23183)
### Audio
* Imported from http://soundbible.com/ :
  * Blop
  * Car Honk
  * Car Rev
  * Garbage Close
  * Glass Break
  * Jump
  * Plastic Bottle Crush
  * Splat
  * Whistle
* Other Sources
  * "My Leg" Sound Effect from Spongebob from YouTube
  * "Tomfoolery" from YouTube
  * "Runaway Food Truck" from http://soundimage.org/
## Division of Labor
### Nichole:
* Unity/Game Work
  * Pizza throwing
    * Added bus ability control
    * Added physics material for reduced sliding/bounciness
    * Added colliders
  * Props & collision
    * Configured colliders for open-source assets
    * Added collision handling for bus interaction with objects
      * Weighted differently for larger objects, no damage for small objects, etc.
    * Added rigidbody physics handling
    * Placed most props in world for game feel on both routes
    * Added road blocks with high collision to help prevent player from straying from path
  * Sound effects (+3D sound stuff)
    * Added 3D sound configuration to cars so that they would have a Doppler Effect/volume drop off as they passed the bus
    * Added sound effects to most interactions with the bus
    * Added background music to the game 
    * Configured sound sources and listeners for various objects
  * Stoplights
    * Light controller 
      * Made it so that they would actually make sense (North-South lights go green while East-West lights go red, etc)
  * Terrain
    * LOD configuration for trees
      * Helps with automatic randomization of size/rotation of trees 
    * Added variety to colors of trees
* External/General Work:
  * 2 people interviewed for playtesting and observations
  * Wrote Game Pitch pdf
  * Helped create list of action items after playtesting
  * Wrote "Results summary/analysis" of playtesting document
    * Created visualizations in Tableau of results

### Katie:
* Unity/Game Work
  * Coding for initial initial demo (before alpha used as base for rest of project)
  * GT map (https://www.openstreetmap.org/relation/3341535, http://osm2world.org/, blender transform obj + materials -> FBX)
  * Map upkeep 
    * Fix z-fighting
    * Add world limiting coliders
    * Add colliders and meshes to all buildings, roads etc
  * Game logic 
    * Win conditions
    * Lose conditions
      * Health decrementing
      * Timer
      * Strikes
  * Game score calculation 
    * Win and lose screens 
  * In game score menu (lower left)
  * Blender made custom construction vehicle (ie ramp)
  * Route lines 
  * Mini Map  
  * Created prefab for bus stops 
  * Pink direction indication arrow
  * Highscore tracking and pages
  * Helped with terrain 
* External/General Work
  * 2 people interviewed for playtesting and observations
  * Demo video (initial and final)
  * Wrote playtesting script

### Brooke:
* Unity/Game Work
  * Student
    * Found student model
    * Made student ragdoll
    * Placed students around the map
    * Student AI
      * Placed waypoints for the students to follow around the map
      * Debugging
  * Car AI
    * Debugging AI
  * In game menu and menu logic
  * Made students easy to see with arrows and circles
* External/General Work
  * 2 people interviewed for playtesting and observations
  * Made game pitch presentation
  * Created game homepage
  
### Lucas:
* Unity/Game Work
  * Core bus behavior
    * Bus mass and suspension adjustments
    * Friction constants
    * Model rigging and WheelCollider placement
    * Bus movement constants
    * Bus self-flipping
  * Bus abilities
    * Bus speed boost mode
    * Bus drift mode
  * Bus visuals 
    * Collision smoke effect
    * Health based exhaust color effect
  * Little animated dude on the bus scripting.
  * Map cell-based occlusion culling (never pushed because it didn’t help at all)
  * Skybox (clouds) for the routes
* External/General Work
  * 2 people interviewed for playtesting and observations
  * First two subsections of the summary and analysis

### Lindsey:
* Unity/Game Work
  * Cars
    * Set car waypoints 
      * Red and blue routes
  * Helped with terrain cleanup 
    * Got rid of trees in buildings and in way of cars/students
  * Props/environment cleanup 
    * Replaced stuff in way of routes
    * Placed more environmental props/trees
  * Menus
    * Start menu
    * Instructions menu
    * Audio
    * Background/How to Play menu
    * Menu logic code
  * Student location updates
  * Made routes two separate scenes
  * Placed bus stops
  * Initial NavMesh
  * Helped debugging of AI code
* External/General Work
  * 2 people interviewed for playtesting and observations
  * Made game pitch presentation
  * Did the “What to fix/what to do next” section of writeup
  * Wrote survey questions
  
## Scenes to Open in Unity
Scene Name|Usage
-|-
Start Menu | Start Screen (*Open this first in Unity to play*)
Controls | Shows controls for the game
High Scores | Shows player's local high scores
Instructions | Explains how to play the game, with win/loss conditions
Red Route | Red Route for the actual game (follows real life red bus route)
Blue Route | Blue Route for the actual game (follows real life blue bus route) 
