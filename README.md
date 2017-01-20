
# Tank-Shooter game.

In this project Tank-shooter game implementation proceeding.

![alt text](PNG/game_1.png "Game picture")

At current moment, game has been implemented only for two players (But it is easy to expand number of players). 
This game has been made as a testing result of my growing skills during GeekBrains [Unity 1](https://geekbrains.ru/courses/90) courses.

## About game

This game is muliplayer game with two players: player1 (red color) and player2(blue color). 
To win the game, player must get 5 frags (In nearlest future number of frags could be changed in Settings menu).

### Players

The players begin game on the opposite sides of the map and begin to hunting-down each other. For hunting they have got two king of shells:
* Simple shell - deals 5 damage only when intersect the players collider and recharge after a 0.3 sec. Fire force is equal to 800 N (low distance).
* Explosive shell - deals 10 damage to area with affected radius is eqaual to 5 unity units to all players within and recharge after a 1 sec. Fire force is equal to 1200 H (big distance)

When one get damaged it looze hp (Initialy each player has 100 hp) and heat point indicatoe displays current hp using different colors (low hp - red color, middle hp - yellow color, full hp - green color).

For example on the picture below you can see two tanks with different hp bar. (blue tank)

![alt text](PNG/hp_show_1.png)

### Objects

In this game implementation there are some additional objects you can find on the map.

1. Plastic Bombs - explode immediately when player intersect bob collider damage him on 20 hp (Only to one Player, who activate the bomb). 
and spawn each 20 sec.
2. Timer Bombs - explode after a certain amount of time (by default this time is equal 2 sec) and deals damage to all players in affected area with 10 unity units radius.
Bombs damage is equal to 10 hp (For all players thouse possition are in affected area) and bomb spawn time is equal to 20 sec.
3. Medicine - heals player (Only to one Player, who give up the medicine box) on 20 hp and heals and spawns each 30 second on a random position betwen several possible.

![alt text](PNG/objects.png)



## Getting Started.

To play this game, or use it as the base for your own project, first thing you need to do - is just to clone it into your local repository.

If you want to deploy this solution on your local machine for development or testing purposes you need to do next things:
* First thing, you need to do - load Unity Project. Just choose apropriate folder.
* Build the project by pressing Ctrl+Shift+B keyboard combination and folow the building instructions. As a result you will get exe file.
* Double click on exe file and enjoy playing the game.


### Prerequisites.

To deploy or run this solution you need:
* [Visual Studio 2015] (https://www.visualstudio.com/ru/downloads/) - an integrated development environment (IDE) from Microsoft used in this pgoject.
* [Unity] (https://unity3d.com/ru/get-unity/download) - Unity is the ultimate game development platform is used to build high-quality 3D and 2D games, deploy them across mobile, desktop, VR/AR, consoles and other platforms.
