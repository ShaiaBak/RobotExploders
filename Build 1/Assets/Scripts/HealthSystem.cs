using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: make some abstract methods
public abstract class HealthSystem : MonoBehaviour {
	
	public int curHP = 1;
	public int maxHP = 1;
	[SerializeField]
	private List<Collider> damageSources;	// Array of recent damage sources
	private int maxCheck = 3;	// Length of count until damage source clear
	private int curCheck = 0;	// Current count until damage source clear

	private GameController gameController;
	
	// Use this for initialization
	void Start () {
		curHP = maxHP;
		gameController = GameObject.Find("GameController").GetComponent<GameController>();

	}
	
	// FixedUpdate is called once per fixed framerate frame
	void FixedUpdate () {
		if(curCheck > 0){
			curCheck--;
			if(curCheck <= 0){
				damageSources.Clear();
			}
		}
	}

	// Checks whether the collider is in the list of recent damage sources to avoid registering multiple hits from the same source
	private bool CheckDuplicateDamageSource(Collider source){
		for(int i=0; i<damageSources.Count; i++){
			if(source == damageSources[i]){
				return true;
			}
		}
		// Add the new source of damage into the list and set the countdown for damage source clearing
		damageSources.Add(source);
		curCheck = maxCheck;
		return false;
	}

	/// <summary>
	/// Reduces HP by amount, checking for death.
	/// </summary>
	/// <param name="amount">
	/// Amount of HP to reduce.
	/// </param>
	public void HurtHealth(int amount, Collider source){
		if( amount > 0 && !CheckDuplicateDamageSource(source) ){
			SetHealth(curHP-amount);
		}
	}
	
	/// <summary>
	/// Increases HP by amount. If over maxHP, set curHP to maxHP.
	/// </summary>
	/// <param name="amount">
	/// Amount.
	/// </param>
	public void HealHealth(int amount){
		SetHealth(curHP+amount);
	}
	
	/// <summary>
	/// Sets the HP to amount. Amount cannot be over maxHP or lower than 0. Handles death.
	/// </summary>
	/// <param name="amount">
	/// Amount of HP to set.
	/// </param>
	public void SetHealth(int amount){
		if(amount >= maxHP){
			curHP = maxHP;
		}
		else{
			curHP = amount;
		}
		if( curHP <= 0 ){
			curHP = 0;
			HandleDeath();
			gameController.GameOver();
		}
	}
	
	/// <summary>
	/// Sets maxHP. If amount is greater than maxHP, then curHP is healed for that amount.
	/// </summary>
	/// <param name="amount">
	/// Amount of maximum HP to be set.
	/// </param>
	public void SetMaxHealth(int amount){
		int loss = maxHP - curHP;
		SetHealth(amount - loss);
		maxHP = amount;
	}
	
	protected abstract void HandleDeath();
	
}
