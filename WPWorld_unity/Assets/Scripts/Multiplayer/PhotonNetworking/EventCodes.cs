﻿public class EventCodes {

    public enum EVENT_CODES
    {
        //General Events
        PLAYER_POSITION_UPDATE = 0,
        PLAYER_ROTATION_UPDATE,
        PLAYER_EVENT_GAMEOVER,

        //Bomberman Events
        BOMBER_EVENT_DROP_BOMB,
        BOMBER_EVENT_PLAYER_DEATH,
        BOMBER_EVENT_SPAWN_POWERUP,
        BOMBER_EVENT_SPAWN_BREAKABLE,

        //Platformer Events
        PLATFORMER_EVENT_PLAYER_DEATH,
        PLATFORM_EVENT_BLOCK_BOUNCE,
        PLATFORM_EVENT_BLOCK_BREAK,
        PLATFORM_EVENT_BLOCK_FALL,
        PLATFORM_EVENT_BLOCK_SPAWNER,
        PLATFORM_EVENT_BLOCK_HIDDEN,
        PLATFORM_EVENT_BLOCK_MOVING,
        PLATFORM_EVENT_ENEMY_DEATH_AIR,
        PLATFORM_EVENT_ENEMY_DEATH_GROUND,
        PLATFORM_EVENT_COIN_PICKUP,
        PLATFORM_EVENT_POWERUP_PICKUP,
        PLATFORM_EVENT_BUTTON_TRIGGERED,
        PLATOFRM_EVENT_LEVER_TRIGGERED,
        PLATFORMER_EVENT_PLAYER_FIREBALL,

        //Snake Events
        SNAKE_EVENT_SPEEDUP,
        SNAKE_EVENT_STUN,
        SNAKE_EVENT_BLOCKS_POP_UP,
        SNAKE_EVENT_SLOWDOWN,
        SNAKE_EVENT_SPAWNFOOD,
        SNAKE_EVENT_EATFOOD,
        SNAKE_EVENT_BODY_POS,
        SNAKE_EVENT_BODY_ROT,
        SNAKE_EVENT_PLAYER_DEATH,

        //Other
        INFO_OTHER_PLAYER,
    }
}
