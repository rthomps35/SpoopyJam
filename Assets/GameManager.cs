using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
	public int PlayerScore;
	public int PlayerSugar;
	public int PlayerChoco;
	public int PlayerHard;
	bool gameOver;	//if true create the end menu and allow restart

	#region Time
	
	//[SerializeField] int startingTimeMinutes;   //time at the start of the game in seconds	
	//public int MinutesRemaining;
	public int SecondsRemaining;
	[SerializeField] int startingSeconds;
	public float passedSeconds;
	[SerializeField] bool crunchTime;           //Added this. Figured I can up the player speed in the last 30 seconds without telling the player.
												//I just want a slight bump for those last second moments.
	public int TotalGameSeconds;
	public bool timerActive = false;
	
	#endregion

	#region Managers
	[SerializeField] MapCreator MC;	
	#endregion

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		uiStart();
        MC.MapGenerator();
		//Start Timer
		
    } 

    // Update is called once per frame
    void Update()
    {
		if (gameOver == false)
		{
			gameTimer();
		}
		
    }

	//When player hit, spawn candy at player, grabbing a random composition of candy that is a percentage of trhe players candy, maybe 60%?
	//give player immunity for x number of seconds using a coroutine
	//Apply random force to the candy rigidbodies
	//enemy AI will prioritize the candy

	void gameTimer()
	{
		passedSeconds += Time.deltaTime;
		SecondsRemaining = startingSeconds - (int)passedSeconds;
		TimerImage.fillAmount = (float)SecondsRemaining/(float)startingSeconds;
		
		if (SecondsRemaining <= 0)
		{
			gameOver = true;
			//spawn game over
		}

	}

	#region UI Items
	[SerializeField] TextMeshProUGUI CandyCountSugar;
	[SerializeField] TextMeshProUGUI CandyCountChoco;
	[SerializeField] TextMeshProUGUI CandyCountHard;
	[SerializeField] Image TimerImage;

	void uiStart()
	{
		CandyCountSugar.text = "0";
		CandyCountChoco.text = "0";
		CandyCountHard.text = "0";
	}

	public void updateUICount(int i)
	{
		switch(i)
		{
			case 0:
				//PlayerSugar += 1;
				CandyCountSugar.text = PlayerSugar.ToString();
				break;
			case 1:
				//PlayerChoco += 1;
				CandyCountChoco.text = PlayerChoco.ToString();
				break;
			case 2:
				//PlayerHard += 1;
				CandyCountHard.text = PlayerHard.ToString();
				break;
		}
	}


	#endregion
}
