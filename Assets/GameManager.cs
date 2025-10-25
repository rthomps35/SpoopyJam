using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int PlayerScore;
	public int PlayerSugar;
	public int PlayerChoco;
	public int PlayerHard;

	#region Managers
	[SerializeField] MapCreator MC;	
	#endregion

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        MC.MapGenerator();
    } 

    // Update is called once per frame
    void Update()
    {
        
    }


	#region UI Items




	#endregion
}
