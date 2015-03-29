using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

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
	private float Stamina
	{
		get{ return stamina;}
		set{
			stamina = value;
			HandleStamina();
		}
	}
	public float maxStamina = 1000f;

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
	public Text healthText;
	public Text staminaText;
	public float coolDown = .5f;
	public bool onCD;

	// Use this for initialization
	void Start () {
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
		//Damage Cooldown
		onCD = false;
	}
	
	// Update is called once per frame
	void Update () {
		//testing
		if (!onCD && Health > 0 && Input.GetKeyDown (KeyCode.Period))
		{
			StartCoroutine(CoolDownAfterDamage());
			Health -= 1;
			Stamina -= 10;
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

	//does math-y things to figure out where to move the bars in the HUD
	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return(x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	//CoRoutine to control damage frequency
	IEnumerator CoolDownAfterDamage()
	{
		onCD = true;
		yield return new WaitForSeconds(coolDown);
		onCD = false;
	}
}
