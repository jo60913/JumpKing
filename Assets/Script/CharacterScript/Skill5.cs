using UnityEngine;
using System.Collections;

public class Skill5 : MonoBehaviour {


	public UISprite[] PlyPreSkill = new UISprite[4];		///角色sprite所要顯示的圖片

	private int[] PlyNum = new int[4];		///panel3載進來的腳色號碼
	private Panel3 panel3;

	public int[] PlyPreSkillNum = new int[4];		/// 角色左右鍵選選擇的數字
	public GameObject[] playerUI = new GameObject[4]; 
	private Click1 _main;

	private bool Stick1Check;
	private bool Stick2Check;

	public float AddTime;
	void Start () {
		panel3 = transform.GetComponent<Panel3>();
		_main = transform.GetComponent<Click1>();
		PlyPreSkillNum[0] = 0;
		PlyPreSkillNum[1] = 0;
		PlyPreSkillNum[2] = 0;
		PlyPreSkillNum[3] = 0;

		Stick1Check = true;
		Stick2Check = true;
	}
	

	void Update () {
		PlyNum[0] = charater(panel3.OnePreCha,0);		//////將panel3所選到的腳色代碼取出
		PlyNum[1] = charater(panel3.TwoPreCha,0);		//////然後選出該腳色是否可以選擇絕招
		PlyNum[2] = charater(panel3.ThreePreCha,0);
		PlyNum[3] = charater(panel3.FourPreCha,0);

		if(PlyNum[0] == 1)								///////玩家1腳色篩選
		{
			PlyPreSkill[0].spriteName = CharaterImg(PlyPreSkillNum[0],"");
			if(Input.GetKeyDown(KeyCode.A))
			{
				PlyPreSkillNum[0]--;
				
				if(PlyPreSkillNum[0] <1)
				{
					PlyPreSkillNum[0] = 1;
				}
			}
			
			if(Input.GetKeyDown(KeyCode.D))
			{
				PlyPreSkillNum[0]++;
				
				if(PlyPreSkillNum[0] > 2)
				{
					PlyPreSkillNum[0] = 2;
				}
			}
		}else if(PlyNum[0] == 0)
		{
			PlyPreSkillNum[0] = 0;
			PlyPreSkill[0].spriteName = "noskill";
		}else if(PlyNum[0] == 2)
		{
			PlyPreSkillNum[0] = 3;
			PlyPreSkill[0].spriteName = "both";
		}


		if(PlyNum[1] == 1)								///////玩家2腳色篩選
		{
			PlyPreSkill[1].spriteName = CharaterImg(PlyPreSkillNum[1],"");
			if(Input.GetKeyDown(KeyCode.J))
			{
				PlyPreSkillNum[1]--;
				
				if(PlyPreSkillNum[1] <1)
				{
					PlyPreSkillNum[1] = 1;
				}
			}
			
			if(Input.GetKeyDown(KeyCode.L))
			{
				PlyPreSkillNum[1]++;
				
				if(PlyPreSkillNum[1] > 2)
				{
					PlyPreSkillNum[1] = 2;
				}
			}
		}else if(PlyNum[1] == 0)
		{
			PlyPreSkillNum[1] = 0;
			PlyPreSkill[1].spriteName = "noskill";
		}else if(PlyNum[1] == 2)
		{
			PlyPreSkillNum[1] = 3;
			PlyPreSkill[1].spriteName = "both";
		}


		if(PlyNum[2] == 1)								///////玩家3腳色篩選
		{
			PlyPreSkill[2].spriteName = CharaterImg(PlyPreSkillNum[2],"");
			if(Stick1Check == true)
			{
				if(Input.GetAxis("Horizontal3") >= 0.8f)
				{
					Stick1Check = false;
					StartCoroutine(StickCon1(Input.GetAxis("Horizontal3")));
					if(PlyPreSkillNum[2] > 2)
					{
						PlyPreSkillNum[2] = 2;
					}
				}

				if(Input.GetAxis("Horizontal3") <= -0.8f)
				{
					Stick1Check = false;
					StartCoroutine(StickCon1(Input.GetAxis("Horizontal3")));
					if(PlyPreSkillNum[2] <1)
					{
						PlyPreSkillNum[2] = 1;
					}
				}
			}
		}else if(PlyNum[2] == 0)
		{
			PlyPreSkillNum[2] = 0;
			PlyPreSkill[2].spriteName = "noskill";
		}else if(PlyNum[2] == 2)
		{
			PlyPreSkillNum[2] = 3;
			PlyPreSkill[2].spriteName = "both";
		}

		if(PlyNum[3] == 1)								///////玩家4腳色篩選
		{
			PlyPreSkill[3].spriteName = CharaterImg(PlyPreSkillNum[3],"");
			if(Input.GetAxis("Horizontal4") == -1)
			{
				PlyPreSkillNum[3]--;
				
				if(PlyPreSkillNum[3] <1)
				{
					PlyPreSkillNum[3] = 1;
				}
			}
			
			if(Input.GetAxis("Horizontal4") == 1)
			{
				PlyPreSkillNum[3]++;
				
				if(PlyPreSkillNum[3] > 2)
				{
					PlyPreSkillNum[3] = 2;
				}
			}
		}else if(PlyNum[3] == 0)
		{
			PlyPreSkillNum[3] = 0;
			PlyPreSkill[3].spriteName = "noskill";
		}else if(PlyNum[3] == 2)
		{
			PlyPreSkillNum[3] = 0;
			PlyPreSkill[3].spriteName = "both";
		}
	}

	int charater(int PerNum,int num)			//////num = 0 沒有絕招 1 只能選一招 2有兩招
	{

		switch (PerNum)
		{
			case 1:				/////選擇蝴蝶
				num = 1;
				break;
			case 2:				//////選擇炎龍
				num = 2;
				break;	
			case 3:				///////選擇小咖龜
				num = 1;
				break;
			case 4:				///////選擇冰狼
				num = 0;
				break;
			case 5:				///////選擇小丑賊
				num = 1;
				break;
			default:
				print ("charator choose overflow");
				break;
		}
		return num;
	}

	string CharaterImg(int PlyNum,string ImgName)		/////ChaNum代表捲到的絕招圖片 1 為大跳 2為雙跳
	{
		switch (PlyNum)
		{
		case 0:
			ImgName = "ChoseSkill";
			break;
		case 1:
			ImgName = "dbjump";
			break;
		case 2:
			ImgName = "bigjump";
			break;
		default:
			print ("charator choose overflow");
			break;
		}
		return ImgName ;
	}

	void select(){
		switch(_main.select){
		case 2:
			playerUI[0].SetActive(true);
			playerUI[1].SetActive(true);
			playerUI[2].SetActive(false);
			playerUI[3].SetActive(false);
			break;
		case 3:
			playerUI[0].SetActive(true);
			playerUI[1].SetActive(true);
			playerUI[2].SetActive(true);
			playerUI[3].SetActive(false);
			break;
		case 4:
			playerUI[0].SetActive(true);
			playerUI[1].SetActive(true);
			playerUI[2].SetActive(true);
			playerUI[3].SetActive(true);
			break;
		}
		Debug.Log("Panel3"+"selct");
	}

	IEnumerator StickCon1(float Direct)			
	{
		if(Direct >= 0.5f)
		{
			PlyPreSkillNum[2]++;
			yield return new WaitForSeconds(AddTime);
			Stick1Check = true;
		}else if(Direct <= -0.5f)
		{
			PlyPreSkillNum[2]--;
			yield return new WaitForSeconds(AddTime);
			Stick1Check = true;
		}
		
	}
	
	/*IEnumerator StickCon2(float Direct)			
	{
		if(Direct >= 0.5f)
		{
			FourPreCha++;
			yield return new WaitForSeconds(AddTime);
			Stick2Check = true;
		}else if(Direct <= -0.5f)
		{
			FourPreCha--;
			yield return new WaitForSeconds(AddTime);
			Stick2Check = true;
		}
		
	}*/
}
