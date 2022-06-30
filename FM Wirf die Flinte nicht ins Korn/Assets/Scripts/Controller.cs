using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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
    [HideInInspector] public AudioManager audioManager;
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
    public string currentScene;

    private bool endFix = false;
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
        currentScene = SceneManager.GetActiveScene().name;
        //updating currentSceneWasVisited
        if (Array.Exists(sceneInfo.visitedScenes, element => element == currentScene))
        {
            currentSceneWasVisited = true;
        }
        else
        {
            currentSceneWasVisited = false;
        }
        Debug.Log(currentSceneWasVisited);
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
        audioManager = FindObjectOfType<AudioManager>();

        //Audio
        audioManager.Play(SceneManager.GetActiveScene().name);

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


    }

    //Managing the MousePos & Game State
    private void Update()
    {
        pointerEvent.position = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentGameState = currentGameState.RunState(this);
        currentGameStateName = currentGameState.ToString();

        if (sceneInfo.tavernenScore >= 8 && sceneInfo.Regina)
        {
            ItsEnding();

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

    public void ItsEnding()
    {
        
        if (!endFix)
        {
            endFix = true;
            dialogueManager.ExitDialogueMode();

            foreach (SpriteRenderer child in childRenderer)
            {
                StartCoroutine(EndFade(child));

            }

        }

    }

     public IEnumerator EndFade(SpriteRenderer child)
     {
        player.GetComponent<SpriteRenderer>().enabled = false;
        child.color = Color.Lerp( Color.white, Color.black, 0.1f);

        yield return new WaitForSeconds(0.05f);
        child.color = Color.Lerp(Color.white, Color.black, 0.2f);

        yield return new WaitForSeconds(0.05f);
        child.color = Color.Lerp(Color.white, Color.black, 0.3f);

        yield return new WaitForSeconds(0.05f);
        child.color = Color.Lerp(Color.white, Color.black, 0.4f);

        yield return new WaitForSeconds(0.05f);
        child.color = Color.Lerp(Color.white, Color.black, 0.5f);

        yield return new WaitForSeconds(0.05f);
        child.color = Color.Lerp(Color.white, Color.black, 0.6f);

        yield return new WaitForSeconds(0.05f);
        child.color = Color.Lerp(Color.white, Color.black, 0.7f);

        yield return new WaitForSeconds(0.05f);
        child.color = Color.Lerp(Color.white, Color.black, 0.8f);

        yield return new WaitForSeconds(0.05f);
        child.color = Color.Lerp(Color.white, Color.black, 0.9f);

        yield return new WaitForSeconds(0.05f);
        child.color = Color.Lerp(Color.white, Color.black, 1f);

        yield return new WaitForSeconds(0.05f);

        EndGame.TS = true;
        SceneManager.LoadScene("End");
        yield return null; 
     }


    #endregion
    /* Code By Jonathan Buss & Andreas Betz
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
