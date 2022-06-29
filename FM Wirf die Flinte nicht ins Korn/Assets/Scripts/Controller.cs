using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    #region Variables
    //Game State Stuff
    [HideInInspector] public bool isTalking;
    [HideInInspector] public ExploreState exploreState = new ExploreState();
    [HideInInspector] public ShotgunState shotgunState = new ShotgunState();
    [HideInInspector] public TalkingState talkingState = new TalkingState();
    [HideInInspector] public DraggingState draggingState = new DraggingState();
    [HideInInspector] public PauseState pauseState = new PauseState();
    [HideInInspector] public bool isDragging;
    [HideInInspector] public IGameState currentGameState;
    [HideInInspector] public IGameState oldState;
    [SerializeField] private string currentGameStateName;


    //General Variables
    public GameObject player;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer[] childRenderer;
    [HideInInspector] public Image shotgunFilter;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public DialogueManager dialogueManager;
    [HideInInspector] public MonologueManager monologueManager;
    [HideInInspector] public CraftingManager craftingManager;
    [HideInInspector] public GameObject talkingFilterParent;
    [HideInInspector] public Color filterColor;
    [HideInInspector] public Vector2 mousePos;
    [HideInInspector] public Vector2 cursorHotspot;
    [HideInInspector] public RaycastHit2D hit;
    [HideInInspector] public PointerEventData pointerEvent;
    [HideInInspector] public GraphicRaycaster raycaster;
    [HideInInspector] public GameObject pauseMenue;
    [HideInInspector] public GameObject controllsMenue;
    public bool currentSceneWasVisited;

    public SceneInfo sceneInfo;
    public float reachRadius = 2f;
    public Texture2D cursor0;
    public Texture2D cursor1;
    public Texture2D crossair0;
    public Texture2D crossair1;
    #endregion

    #region Functions
    //Intitializing
    private void Awake()
    {
        //for talkingFilter
        talkingFilterParent = GameObject.Find("Environment");
        childRenderer = talkingFilterParent.GetComponentsInChildren<SpriteRenderer>();
        ColorUtility.TryParseHtmlString("#4763FF", out filterColor);

        //cursor
        raycaster = FindObjectOfType<Canvas>().GetComponent<GraphicRaycaster>();
        pointerEvent = new PointerEventData(EventSystem.current);
        cursorHotspot = new Vector2(cursor0.width / 2, cursor0.height / 2);
        Cursor.SetCursor(cursor0, cursorHotspot, CursorMode.ForceSoftware);

        //classes that controller holds
        
        player = GameObject.FindGameObjectWithTag("Player");
        shotgunFilter = GameObject.Find("ShotgunFilter").GetComponent<Image>();
        pauseMenue = GameObject.Find("Pause Menue");
        controllsMenue = GameObject.Find("Controlls Menue");
        playerMovement = player.GetComponent<PlayerMovement>();
        inventory = player.GetComponent<Inventory>();
        animator = player.GetComponent<Animator>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        dialogueManager = gameObject.GetComponent<DialogueManager>();
        monologueManager = gameObject.GetComponent<MonologueManager>();
        craftingManager = gameObject.GetComponent<CraftingManager>();

        //seting up variables
        shotgunFilter.enabled = false;
        pauseMenue.SetActive(false);
        controllsMenue.SetActive(false);
        lineRenderer.enabled = false;
        currentGameState = exploreState;

        //seting up variables from the Scene info
        player.transform.position = sceneInfo.spawnpoint;
        player.transform.rotation = sceneInfo.spawnpointRotation;
        inventory.isFull = sceneInfo.isFull;
        inventory.content = sceneInfo.content;



        /*///Start Button Stuff:
        
        //resets the arrays on sceneInfo (still needs Regina bool)
        sceneInfo.isFull = new bool[13];
        sceneInfo.content = new ScrItem[13];
        sceneInfo.sceneSave = new string[13];
        sceneInfo.Regina = false;

        //Set Spawnpoint
        //sceneInfo.spawnpoint = new Vector3(-7.19f, -2f, 0f);

        //things that should be commented out when testing certain things
        sceneInfo.characters = new();

        ///End Start Button Stuff*/

        //loading invendory
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
        Debug.Log(SceneManager.GetActiveScene().name);
        for(int i = 0; i < sceneInfo.toInstantiateItem.Length; i++)
        {
            if(sceneInfo.toInstantiateItem[i] != null && 
                !Array.Exists(sceneInfo.sceneSave, element => element == sceneInfo.toInstantiateItem[i].name) &&
                sceneInfo.sceneItemLaysIn[i] == SceneManager.GetActiveScene().name)
            {
                Instantiate(sceneInfo.toInstantiateItem[i], sceneInfo.itemsSpawnPos[i], Quaternion.identity );
            }
        }

        for (int a = 0; a < sceneInfo.sceneSave.Length; a++)
        {
            if (GameObject.Find(sceneInfo.sceneSave[a]))
            {
                GameObject.Find(sceneInfo.sceneSave[a]).gameObject.SetActive(false);
            }
        }

        if (!Array.Exists(sceneInfo.visitedScenes, element => element == SceneManager.GetActiveScene().name))
        {
            currentSceneWasVisited = false;
        }
        else
        {
            currentSceneWasVisited = true;
        }
    }

    //Managing the MousePos & Game State
    private void Update()
    {
        pointerEvent.position = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentGameState = currentGameState.RunState(this);
        currentGameStateName = currentGameState.ToString();

        if (sceneInfo.tavernenScore >= 5)
        {
            //Tavernenschlägerei Ende
        }

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

    public void StartButton()
    {
        ///Start Button Stuff:

        //resets sceneInfo
        sceneInfo.isFull = new bool[13];
        sceneInfo.content = new ScrItem[13];
        sceneInfo.sceneSave = new string[15];
        sceneInfo.characters = new();
        sceneInfo.Regina = false;
        sceneInfo.Flintendialog = false;

        //Set Spawnpoint
        sceneInfo.spawnpoint = new Vector3(-7.19f, -2f, 0f);
        sceneInfo.spawnpointRotation = Quaternion.identity;

        ///End Start Button Stuff
        
    }


    #endregion
    /*
    ⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠛⠛⠛⠋⠉⠈⠉⠉⠉⠉⠛⠻⢿⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣿⡿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⢿⣿⣿⣿⣿
    ⣿⣿⣿⣿⡏⣀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣤⣄⡀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣿
    ⣿⣿⣿⢏⣴⣿⣷⠀⠀⠀⠀⠀⢾⣿⣿⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀⠀⠈⣿⣿
    ⣿⣿⣟⣾⣿⡟⠁⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⣷⢢⠀⠀⠀⠀⠀⠀⠀⢸⣿
    ⣿⣿⣿⣿⣟⠀⡴⠄⠀⠀⠀⠀⠀⠀⠙⠻⣿⣿⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⣿
    ⣿⣿⣿⠟⠻⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠶⢴⣿⣿⣿⣿⣿⣧⠀⠀⠀⠀⠀⠀⣿
    ⣿⣁⡀⠀⠀⢰⢠⣦⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⣿⣿⣿⣿⡄⠀⣴⣶⣿⡄⣿
    ⣿⡋⠀⠀⠀⠎⢸⣿⡆⠀⠀⠀⠀⠀⠀⣴⣿⣿⣿⣿⣿⣿⣿⠗⢘⣿⣟⠛⠿⣼
    ⣿⣿⠋⢀⡌⢰⣿⡿⢿⡀⠀⠀⠀⠀⠀⠙⠿⣿⣿⣿⣿⣿⡇⠀⢸⣿⣿⣧⢀⣼
    ⣿⣿⣷⢻⠄⠘⠛⠋⠛⠃⠀⠀⠀⠀⠀⢿⣧⠈⠉⠙⠛⠋⠀⠀⠀⣿⣿⣿⣿⣿
    ⣿⣿⣧⠀⠈⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠟⠀⠀⠀⠀⢀⢃⠀⠀⢸⣿⣿⣿⣿
    ⣿⣿⡿⠀⠴⢗⣠⣤⣴⡶⠶⠖⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡸⠀⣿⣿⣿⣿
    ⣿⣿⣿⡀⢠⣾⣿⠏⠀⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠛⠉⠀⣿⣿⣿⣿
    ⣿⣿⣿⣧⠈⢹⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⣿⣿⣿⣿
    ⣿⣿⣿⣿⡄⠈⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⣴⣾⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣧⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣿⣦⣄⣀⣀⣀⣀⠀⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡄⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠀⠀⠙⣿⣿⡟⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⠀⠁⠀⠀⠹⣿⠃⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⣿⣿⣿⣿⡿⠛⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⢐⣿⣿⣿⣿⣿⣿⣿⣿⣿
    ⣿⣿⣿⣿⠿⠛⠉⠉⠁⠀⢻⣿⡇⠀⠀⠀⠀⠀⠀⢀⠈⣿⣿⡿⠉⠛⠛⠛⠉⠉
    ⣿⡿⠋⠁⠀⠀⢀⣀⣠⡴⣸⣿⣇⡄⠀⠀⠀⠀⢀⡿⠄⠙⠛⠀⣀⣠⣤⣤⠄*/
}
