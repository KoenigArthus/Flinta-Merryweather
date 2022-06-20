using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    #region Variables
    //Game State Stuff
    [HideInInspector] public bool isTalking;
    [HideInInspector] public ExploreState exploreState = new ExploreState();
    [HideInInspector] public ShotgunState shotgunState = new ShotgunState();
    [HideInInspector] public TalkingState talkingState = new TalkingState();

    [HideInInspector] public IGameState currentGameState;
    [SerializeField]  private string currentGameStateName;

    //General Variables
    public SceneInfo sceneInfo;
    public float reachRadius = 2f;
    [HideInInspector] public MonologueManager monologueManager;
    [HideInInspector] public DialogueManager dialogueManager;
    [HideInInspector] public GameObject player;
     public Image shotgunFilter;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public Vector2 mousePos;
    [HideInInspector] public RaycastHit2D hit;
    #endregion

    #region Functions
    //Intitializing
    //resets sceneInfo arrays + instantiates items from inventory back into UI-Element
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        inventory = player.GetComponent<Inventory>();
        shotgunFilter.enabled = false;
        monologueManager = gameObject.GetComponent<MonologueManager>();
        dialogueManager = gameObject.GetComponent<DialogueManager>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        currentGameState = exploreState;
        //sets inventory arrays to SceneInfo Arrays
        inventory.isFull = sceneInfo.isFull;
        inventory.content = sceneInfo.content;

        sceneInfo.isFull = new bool[13];
        sceneInfo.content = new ScrItem[13];

        for (int i = 0; i < inventory.isFull.Length; i++)
        {
            if (inventory.isFull[i] == true)
            {
                Instantiate(inventory.content[i].UIObject,inventory.slots[i].transform, false);
            }
        }
    }


    //Managing the MousePos & Game State
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentGameState = currentGameState.RunState(this);
        currentGameStateName = currentGameState.ToString();
    }

    // Debug Method to See the reachRadius
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(player.transform.position, reachRadius);
    }



    //Checking if the Interactable of hit is in reach of the player
    public bool IsInReach()
    {
        // calculating the distance of Player to Interactable:
        // (PointA - PointB).sqrMagnitude <= dist * dist       // P(x1,y1), I(y2,y1), (distance = √((y2-y1)^2 + (x2-x1)^2)) <-outdated calculation 
        return (player.transform.position - hit.collider.gameObject.transform.position).sqrMagnitude <= reachRadius * reachRadius;
    }

    #endregion
}
