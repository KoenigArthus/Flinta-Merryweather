
//this interface describes any Game State who will need a RunState(controller) function that needs a Controller
public interface IGameState
{
    IGameState RunState(Controller pcon);
}
