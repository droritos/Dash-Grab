using Game.Client.Enemy;
using Game.Data;
using UnityEngine.Events;

public static class EventObserver
{
    public static UnityAction<PickUpBlock> OnPickUpCollected;
    public static UnityAction<Enemy> OnEnemyHit;

}
