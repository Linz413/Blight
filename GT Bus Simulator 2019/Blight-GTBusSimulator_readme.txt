Team Name: Blight
Team Members: Brooke White, Katie Cox, Lindsey Stowell, Nichole Deeb, Lucas Liu
Game Name: GT Bus Simulator 2019

Requirements Completed:

3D Game Feel Game:
Our game has a clearly achievable goal in that the user must pick up a defined number of students scattered across the map.
Our game has a clear communication of failure to the user. One such example would be if the player runs over too many students and loses the game, a menu appears alerting them to their loss and prompting them to restart.
We have a start menu that allows the user to select between two different routes to follow before playing the game. The user can also press the escape key at any point to restart, quit, or return to the main menu.
If the user should fail at the game, there is a start menu that appears giving them the option to restart or return to the main menu.

Precursors to Fun Gameplay:
The choices made by the player come in the form of how they choose to traverse the environment and balance between picking up students and avoiding obstacles such as students, buildings, and other cars.
Choosing to hit a student to pick up a student, for example, will incur a strike. The consequence of doing so is that if too many strikes are given, then the player loses.
Our player must navigate a real map of Georgia Tech where damage is given if cars or buildings are struck and a clear penalty is given if students are hit
The player does not immediately have to try and pick up the student, they can choose to drive around a bit until they feel comfortable with the handling of the bus.
The user can choose to utilize pizza powerups in a strategic way by pressing left shift to throw pizza lures that keep groups of students off the road. This will help avoid hitting students.

Real-Time 3D Control:
Our player bus is a yellow school bus with a robot standing on top of it that shows the motion of the player when the WASD controls are used to control the bus.
The bus model was imported from a unity store asset and the robot’s model was borrowed from one of the existing milestones
Both the bus model and the robot were used without any built-in animations.
The movement of the bus was defined using the unity vehicle tools manager.
This involved the creation of several wheelcollider objects and the configuration of the suspension of these colliders
Once this was done, the colliders were lined up to the bus model.
Wheels are animated by setting wheel models to the transform of the wheelcolliders.
The bus moves by applying a torque on the wheelcolliders and turns by rotating the forward pair of wheelcolliders.
Using Unity’s default input axis, the control of the bus is continuous. 
The model on top of the bus was animated manually and responds to user input
This is done by rotating the transform of certain joints in the robot based on several lines of conditional logic.
The movement of the model’s arms correspond to the direction faced by the bus’ forward axis; if the axis faces left, then the model points left. 
Controls are simple to understand, using the standard WASD for basic bus movement, and X for the handbrake of the bus.

3D World:
We have an audio manager that appropriately displays sounds for collisions between the bus and a student, as well as general bus sounds.
The bus does pass through any physical object in the game.
When collisions occur, audio plays and damage is given to the bus
Students hit by the bus ragdoll and are flung in an appropriate direction.
The bus behaves in a physically accurate way, with simulated suspension, friction and can topple or fall over if the user is careless.
The bus must also interact with and avoid several cars traversing the environment.
Students are animated using mecanim.
The movement of the bus is independent of framerate, as is the animation and movement of the students.

Real-Time NPC Steering Behaviors / Artificial Intelligence:
The cars and students follow waypoints using a Navmesh as in milestone 4
The model for the student and car were used, but the AI was completely hand coded
The student has multiple behaviors such as walk, idle, lured by pizza, hit by the bus, or picked up by the bus. The car has similar states.
The AI smoothly steer to the location of the waypoint
The characters use root motion for their animations
The AI cars follow the roads reasonably, and the students have a relatively believable path 

Polish:
Art-style: We intentionally chose assets with a cartoony or simplistic look to them for cohesiveness in visuals. The bus and robot are examples of models deliberately chosen for their low-poly aesthetics.
There is a start menu that is displayed when the game is first opened where user can choose route to play
There is an in-game pause button that gives user ability to restart, return to the main menu, or quit the game

Basic Bus Controls:
W: accelerate
S: decelerate
A: turn left
D: turn right
LSHIFT: drop lure (pizza)
space: bus hop
X: handbrake

Demonstrating Requirements:
The grader should select a route and drive the bus to pick up 10 different students placed at bus-stops along the selected path. In doing so, the grader will encounter the AI-controlled students and cars. The grader should first try to fail the game by reducing their health to 0 through collisions with cars or run out of strikes by hitting too many students. Then, the grader should complete the game.  

Main Scene to Be Opened: the StartMenu scene in the scenes folder, the blue route is created in the BlueRoute scene, the red route is created in the RedRoute scene. 

Assets Used:
Supercyan Character Pack Free Sample
Standard Assets
Unity Vehicle Tools
Cartoon Vehicles lowpoly
Y_Bot from previous milestones

All audio used was imported from http://soundbible.com/

