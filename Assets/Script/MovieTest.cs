using UnityEngine;
using System.Collections;

public class MovieTest : MonoBehaviour {

	public MovieTexture movTexture;	//要播放的影片的電影貼圖
	public GameObject Obj;	//播放的物件
	private MeshRenderer mesh;	//宣告該物件的MeshRenderer
	public AudioSource BGM;	//原本播放的背景音樂
	public AudioSource MovMusic;	//影片音樂
	public bool MovSit;	//影片是否可播放
	void Start()
	{
		//设置当前对象的主纹理为电影纹理
		//renderer.material.mainTexture = movTexture;
		//设置电影纹理播放模式为循环
		movTexture.loop = true;	//設定讓影片為Loop 重複播放
		MovMusic.loop = true;	//設定讓影片的音樂為Loop 重複播放
		mesh = GetComponent<MeshRenderer>();	//取得物件的MeshRenderer
		mesh.enabled = false;	//關閉該物件的MeshRenderer (雖然物件在場上 但如果關閉MeshRenderer的話還是會看不到物件 要播放影片的時候再開啟)
		MovSit = true;	//MovSit為true 表示影片是可以播放的

	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Joystick1Button6))	//如果P1按下按鍵6的話
		{
			if(MovSit)	//如果MovSit為true的話 (完整為if(MovSit==true)後面==true省略不寫代表該變數為true時)
			{
				renderer.material.mainTexture = movTexture;	//將該物件material的貼圖改為影片貼圖
				BGM.mute = true;	//原本的背景音樂改為靜音
				MovMusic.Play();	//影片的音樂開始播放
				mesh.enabled = true;	//播放影片物件的MeshRenderer開啟
				movTexture.Play();	//影片開始播放
				MovSit = false;		//現在記錄為影片播放中
			}else{							//如果MoveSit為false的話
				if(movTexture.isPlaying)	//而且影片還在播放的的時候
				{
					movTexture.Stop ();		//影片暫停 (一般影片暫停有分暫停後再按播放會再繼續播放(一般符號長這樣║)，而這邊的暫停是暫停後如果再按播放會從新開始(一般符號長這樣■)
				}
				MovSit = true;	//記錄成影片可以播放
			}
		}
	}


}
