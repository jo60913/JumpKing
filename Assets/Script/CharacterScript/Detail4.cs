using UnityEngine;
using System.Collections;

public class Detail4 : MonoBehaviour {
	
	public UISprite[] CharatorDetailImage;
	private Panel3 panel3;
	private Click1 _main;
	void Start () {
		panel3 = transform.GetComponent<Panel3>();
		_main = transform.GetComponent<Click1>();
	}
	
	
	void Update () {
		
		//CharatorDeatil(0,panel3.OnePreCha);
		//CharatorDeatil(1,panel3.TwoPreCha);
		//CharatorDeatil(2,panel3.ThreePreCha);
		//CharatorDeatil(3,panel3.FourPreCha);
		if(_main.select == 2)
		{
			CharatorDeatil(0,panel3.OnePreCha);
			CharatorDeatil(1,panel3.TwoPreCha);
			CharatorDeatil(2,6);
			CharatorDeatil(3,6);
		}else if(_main.select == 3)
		{
			CharatorDeatil(0,panel3.OnePreCha);
			CharatorDeatil(1,panel3.TwoPreCha);
			CharatorDeatil(2,panel3.ThreePreCha);
			CharatorDeatil(3,6);
		}else if(_main.select == 4)
		{
			CharatorDeatil(0,panel3.OnePreCha);
			CharatorDeatil(1,panel3.TwoPreCha);
			CharatorDeatil(2,panel3.ThreePreCha);
			CharatorDeatil(3,panel3.FourPreCha);
		}
		
		//Debug.Log(panel3.TwoPreCha);
	}
	void CharatorDeatil(int plynumber,int plyimgnumber)
	{
		switch (plyimgnumber)
		{
		case 1:
			CharatorDetailImage[plynumber].spriteName = "butterfly";
			break;
		case 2:
			CharatorDetailImage[plynumber].spriteName = "dragon";
			break;
		case 3:
			CharatorDetailImage[plynumber].spriteName = "lft";
			break;
		case 4:
			CharatorDetailImage[plynumber].spriteName = "wolf";
			break;
		case 5:
			CharatorDetailImage[plynumber].spriteName = "clown";
			break;
		case 6:
			CharatorDetailImage[plynumber].spriteName = "";
			break;
		default:
			print ("charator choose overflow");
			break;
		}	
	}
	
	
	
}
