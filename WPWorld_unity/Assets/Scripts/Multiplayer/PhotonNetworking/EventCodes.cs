﻿public class EventCodes {

    public enum EVENT_CODES
    {
        //Bomberman Events
        BOMBER_EVENT_DROP_BOMB = 0,
        BOMBER_EVENT_PLAYER_DEATH,
        BOMBER_EVENT_SPAWN_POWERUP,

        //Platformer Events
        PLATFORM_EVENT_BLOCK_BOUNCE,
        PLATFORM_EVENT_BLOCK_BREAK,
        PLATFORM_EVENT_BLOCK_FALL,
        PLATFORM_EVENT_BLOCK_SPAWNER,
        PLATFORM_EVENT_BLOCK_HIDDEN,
        PLATFORM_EVENT_ENEMY_DEATH_AIR,
        PLATFORM_EVENT_ENEMY_DEATH_GROUND,
        PLATFORM_EVENT_COIN_PICKUP,
        PLATFORM_EVENT_POWERUP_PICKUP,
        PLATFORM_EVENT_BUTTON_TRIGGERED,
        PLATOFRM_EVENT_LEVER_TRIGGERED,
        PLATFORM_EVENT_CHECKPOINT_TRIGGERED,
    }
}