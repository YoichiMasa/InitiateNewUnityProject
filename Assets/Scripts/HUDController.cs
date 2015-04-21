using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public InventoryManager invent;
	//obvious variables are obvious
	public float health;
	private float Health
	{
		get { return health; }
		set{
			health = value;
			HandleHealth();
		}
	}
	public float maxHealth = 100f;

	public float stamina = 1000f;
	public float Stamina
	{
		get{ return stamina;}
		set{
			stamina = value;
			HandleStamina();
		}
	}
	public float maxStamina = 1000f;
	
	public float CurrentWeight
	{
		get{ return invent.invWeight;}
		set{
			invent.invWeight = value;
			HandleWeight();
		}
	}

	//base values for stamina calculations
	public float healThreshold = 0.5f;
	public float idle = 1f;
	public float move = 0.05f;
	public float sprint = 0.1f;
	public float jump = 2f;
	public float climb = 0.1f;
	public float attack;

	//GUI management

	//health
	public RectTransform healthTransform;
	public Image visualHealth;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	//stamina
	public RectTransform staminaTransform;
	public Image visualStamina;
	private float cachedYStamina;
	private float minXValueStamina;
	private float maxXValueStamina;
	//weight
	public RectTransform weightTransform;
	public Image visualWeight;
	public Image BarBacking;
	private float cachedYWeight;
	private float minXValueWeight;
	private float maxXValueWeight;
	public Text healthText;
	public Text staminaText;
	public Text weightText;
	//Read Out Field
	public Text readOutText;
	public bool printMessage = false;

	//others
	public float coolDown = .5f;
	public bool onCD;

	//Singleton Stuff
	public static HUDController instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
			gameObject.SetActive(false);
	}

	// Use this for initialization
	void Start () {

		invent = GameObject.FindGameObjectWithTag ("GameController").GetComponent<InventoryManager> ();
		//healthinit
		cachedY = healthTransform.position.y;
		maxXValue = healthTransform.position.x;
		minXValue = healthTransform.position.x - healthTransform.rect.width;
		Health = maxHealth;

		//Staminainit
		cachedYStamina = staminaTransform.position.y;
		maxXValueStamina = staminaTransform.position.x;
		minXValueStamina = staminaTransform.position.x - staminaTransform.rect.width;
		Stamina = maxStamina;

		//Weightinit
		cachedYWeight = weightTransform.position.y;
		Debug.Log(cachedYWeight);
		maxXValueWeight = weightTransform.position.x;
		minXValueWeight = weightTransform.position.x - weightTransform.rect.width;
		CurrentWeight = 0;
		BarBacking.enabled = false;
		weightText.enabled = false;

		readOutText.text = "";

		//Damage Cooldown
		onCD = false;

		Application.targetFrameRate = 30;
	}
	
	// Update is called once per frame
	void Update () {
		//testing
		if (!onCD && Health > 0 && Input.GetKey (KeyCode.Period))
		{
			StartCoroutine(CoolDownAfterDamage());
			Health -= 1;
		}

		if(health < maxHealth && stamina >= (maxStamina * healThreshold))
		{
			Health += (1 * Time.deltaTime / 12);
		}

		if(stamina > 0)
		{
			Stamina -= ((idle + (idle * (CurrentWeight/invent.MAX_WEIGHT))) * (Time.deltaTime / 60));
		}

	}

	//changes Health Bar
	private void HandleHealth()
	{
		healthText.text = "" + health.ToString("000") + " |";
		float currentXValue = MapValues (health, 0, maxHealth, minXValue, maxXValue);
		healthTransform.position = new Vector2(currentXValue, cachedY);
	}

	//Changes Stamina Bar
	private void HandleStamina()
	{
		staminaText.text = "" + ((stamina/maxStamina)*100).ToString("000.00") + "% |";
		float currentXValue = MapValues (stamina, 0, maxStamina, minXValueStamina, maxXValueStamina);
		staminaTransform.position = new Vector2(currentXValue, cachedYStamina);
	}

	//changes Weight Bar
	public void HandleWeight()
	{
		weightText.text = "Current Weight | " + CurrentWeight.ToString("00") + " lbs.";
		float currentXValue = MapValues (CurrentWeight, 0, invent.MAX_WEIGHT, minXValueWeight, maxXValueWeight);
		weightTransform.position = new Vector2(currentXValue, cachedYWeight);
		Debug.Log(cachedYWeight);
	}

	//does math-y things to figure out where to move the bars in the HUD
	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return(x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	//Message Read Out On Screen Method for other classes
	public void Message(string message, float time)
	{
		StartCoroutine(ShowMessage(message, time));
	}

	//Message Read Out for persistant messages on triggers. 
	//WARNING: Put this method in the OnTriggerExit of the trigger with an empty string to clear the readout.
	public void MessageStay(string message)
	{
		if (!printMessage) 
		{
			readOutText.text = message;
		}
	}

	//The Coroutine that handles message display
	IEnumerator ShowMessage(string message, float time)
	{
		printMessage = true;
		readOutText.text = message;
		yield return new WaitForSeconds(time);
		readOutText.text = "";
		printMessage = false;
	}

	//CoRoutine to control damage frequency
	IEnumerator CoolDownAfterDamage()
	{
		onCD = true;
		yield return new WaitForSeconds(coolDown);
		onCD = false;
	}

	//Used to calculate movement based Stamina consumption
	public void StaminaMovementHandling(float baseValue)
	{
		Stamina -= (((baseValue) + ((baseValue) * (CurrentWeight / invent.MAX_WEIGHT))) * (Time.deltaTime));
	}

	//Used for more general forms of stamina consumption
	public void RegularStaminaConsumption(float baseValue)
	{
		Stamina -= ((baseValue) + ((baseValue) * (CurrentWeight / invent.MAX_WEIGHT)));
	}
}
