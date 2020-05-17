# Orbitality
Space shooter (Deffender)


Game play part.

1. Orbital system with 5 planets (not including Sun), hardcoded in Constants.
2. Planets graphics was created by shader graph, atmosphere also to.
3. Planets params random from hardcoded.
4. Weapon can be configured from ScriptableObject "Resources\Rockets". 
5. Enemy wave can be configured from ScriptableObject "EnemyWave\WaveData". After wave complete next start with x2, x3, x3.. etc params.
6. Two Game modes.
6.1 Solo game with out enemy, in this mode you need to destroy Solar system with your weapon. (Can be finished).
6.2 PvE game mode, you need to protect Solar system from space invaders. (Can't be finished).
7. Save states. You can save you game in all modes. (save only planet position and enemy Wave).