public abstract class PlayerState
{
    public abstract PlayerAnimState playerState { get; }
    protected Player player;
    
    public virtual void OnEnter()
    {
        player = Player.instance;
        player.GetAnimControl().PlayAnim(playerState);
    }

    public virtual void OnExit()
    {
        
    }

    public virtual PlayerAnimState OnUpdate()
    {
        return playerState;
    }
}