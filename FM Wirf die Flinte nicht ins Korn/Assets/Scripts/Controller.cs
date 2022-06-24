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
    [SerializeField] private string currentGameStateName;
 

    //General Variables
    public SceneInfo sceneInfo;
    public float reachRadius = 2f;
    [HideInInspector] public MonologueManager monologueManager;
    [HideInInspector] public DialogueManager dialogueManager;
    [HideInInspector] public GameObject player; 
    [HideInInspector] public SpriteRenderer[] childRenderer;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Vector2 mousePos;
    [HideInInspector] public Vector2 cursorHotspot;
    [HideInInspector] public RaycastHit2D hit;
    [HideInInspector] public Color filterColor;
    public Image shotgunFilter;
    public string[] sceneSave;
    public GameObject talkingFilterParent;
    public Texture2D cursor0;
    public Texture2D cursor1;
    public Texture2D crossair0;
    public Texture2D crossair1;
    #endregion

    #region Functions
    //Intitializing
    //resets sceneInfo arrays + instantiates items from inventory back into UI-Element
    private void Awake()
    {
        childRenderer = talkingFilterParent.GetComponentsInChildren<SpriteRenderer>();
        ColorUtility.TryParseHtmlString("#4763FF", out filterColor);
        cursorHotspot = new Vector2(cursor0.width / 2, cursor0.height / 2);
        Cursor.SetCursor(cursor0, cursorHotspot, CursorMode.ForceSoftware);
        shotgunFilter.enabled = false;

        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        inventory = player.GetComponent<Inventory>();
        animator = player.GetComponent<Animator>();
        monologueManager = gameObject.GetComponent<MonologueManager>();
        dialogueManager = gameObject.GetComponent<DialogueManager>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        lineRenderer.enabled = false;
        player.transform.position = sceneInfo.spawnpoint;
        currentGameState = exploreState;

        //sets inventory arrays to SceneInfo Arrays
        inventory.isFull = sceneInfo.isFull;
        inventory.content = sceneInfo.content;
        sceneSave = sceneInfo.sceneSave;


        //resets the arrays on sceneInfo (still needs Regina bool)
        sceneInfo.isFull = new bool[13];
        sceneInfo.content = new ScrItem[13];
        sceneInfo.sceneSave = new string[13];


        for (int i = 0; i < inventory.isFull.Length; i++)
        {
            if (inventory.isFull[i] == true)
            {
                Instantiate(inventory.content[i].UIObject, inventory.slots[i].transform, false);
            }
        }

    }
    private void Start()
    {
        for (int a = 0; a < sceneSave.Length; a++)
        {
            if (GameObject.Find(sceneSave[a]))
            {
                GameObject.Find(sceneSave[a]).gameObject.SetActive(false);
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
