using UnityEngine;
using System.Collections;

public class SquidAnim : MonoBehaviour {
	
	private Animator anim;
	private SquidMove _SquidMove;
	public GameObject mat;

	public GameObject Start_Eff;
	public GameObject Mid_Eff;
	private float LastBlood;
	private float CurrentBlood;
	public float DamageShowTime;
	private float _DamageShowTime;
	public float JumpCD;
	public float BigJumpCD;
	private bool SkillReCheck;
	public bool CDtimeCheck;
	private float CDTime;
	private bool SkillCheck;
	private float SkillTime;
	public GameObject AngryEffect;
	public Texture StatuDirty;
	public Texture Statu;
	public GameObject InvincibleObj;
	public GameObject SpeedDownObj;
	void Start () {
		AngryEffect.SetActive(false);
		SkillReCheck = false;
		CDtimeCheck = false;
		CDTime = 3;
		SkillTime = 0.7f;
		anim = transform.Find("Roles_05_1008_TPossSkin").GetComponent<Animator> ();
		_SquidMove = GetComponent<SquidMove>();
		Start_Eff.SetActive(false);
		Mid_Eff.SetActive(false);
		LastBlood = _SquidMove.blood;
		CurrentBlood = _SquidMove.blood;
		_DamageShowTime = DamageShowTime;
		InvincibleObj.SetActive(false);
		StartCoroutine(InvincibleTime());
		SpeedDownObj.SetActive(false);
	}
	
	void Update () {
		CurrentBlood = _SquidMove.blood;
		if(CurrentBlood < LastBlood)
		{
			DamageShowTime -=Time.deltaTime;
		}
		
		if(DamageShowTime < _DamageShowTime)
		{
			mat.renderer.material.shader = Shader.Find("Custom/RimLight");
			mat.renderer.material.SetColor("_RimColor",new Color(1,0,0,0));
			
		}
		if(DamageShowTime < 0)
		{
			mat.renderer.material.shader = Shader.Find("myUnlitAlpha");
			DamageShowTime = _DamageShowTime;
			LastBlood = CurrentBlood;
		}

		if(_SquidMove.AngryCheck == true)
		{
			AngryEffect.SetActive(true);
		}else{
			AngryEffect.SetActive(false);
		}
		
		if(_SquidMove.plysit == "CDTime")						/////時間buff變黑
		{
			CDtimeCheck = true;
		}
		
		if(CDtimeCheck == true)
		{
			
			mat.renderer.material.shader = Shader.Find("Character/StatueDiffuse");
			mat.renderer.material.SetTexture("_GrungeTex",StatuDirty);
			mat.renderer.material.SetTexture("_BaseTex",Statu);
			mat.renderer.material.SetFloat("_Tighten",0.45f);
			mat.renderer.material.SetFloat("_DiffuseAmount",0);
			mat.renderer.material.SetFloat("_GrungeAmount",1.8f);
			CDTime -=Time.deltaTime;
			if(CDTime < 0)
			{
				CDTime = 1.5f;
				CDtimeCheck = false;
				mat.renderer.material.shader = Shader.Find("myUnlitAlpha");
			}
		}
		if(_SquidMove.plysit == "Skill")						/////放衝刺變青澀
		{
			SkillCheck = true;
		}
		
		if(SkillCheck == true)
		{
			mat.renderer.material.shader = Shader.Find("Custom/RimLight");
			mat.renderer.material.SetColor("_RimColor",new Color(0.5f,1,0,0));
			anim.SetFloat("Speed",0);
			anim.SetBool("Sprint",true);
			SkillTime -=Time.deltaTime;
			if(SkillTime <= 0)
			{
				SkillTime = 0.7f;
				SkillCheck = false;
				mat.renderer.material.shader = Shader.Find("myUnlitAlpha");
				anim.SetBool("Sprint",false);
			}
		}
		if(_SquidMove.plysit == "Die")
		{
			mat.renderer.material.shader = Shader.Find("GreyScale");
			anim.SetBool("Die",true);
			AngryEffect.SetActive(false);
		}
		

		if(_SquidMove.plysit == "jumpingpar")						
		{
			anim.SetFloat("Speed",0);
			anim.SetBool("JumpPar",true);
			StartCoroutine(TranJumpPar(JumpCD));
		}
		
		
		if(_SquidMove.plysit == "bigjumppar")						
		{
			anim.SetFloat("Speed",0);
			anim.SetBool("BigJumpPar",true);
			Start_Eff.SetActive(true);
			StartCoroutine(TranBigJumpPar(BigJumpCD));
		}		
		

		
		if(_SquidMove.plysit == "ground")
		{
			if(Input.GetAxis(_SquidMove.PlyHor) == 1 || Input.GetAxis(_SquidMove.PlyHor) == -1)	//移動
			{
				anim.SetFloat("Speed",1);
			}
			
			if(Input.GetAxis(_SquidMove.PlyVer) == 1 || Input.GetAxis(_SquidMove.PlyVer) == -1)
			{
				anim.SetFloat("Speed",1);
			}
		}
		
		if(Input.GetAxis(_SquidMove.PlyHor) == 0)			//////放開
		{
			if(Input.GetAxis(_SquidMove.PlyVer) == 0)
			{
				anim.SetFloat("Speed",0);
			}
		}

	
	}
	IEnumerator InvincibleTime()                    
	{
		InvincibleObj.SetActive(true);
		yield return new WaitForSeconds(_SquidMove.InvincibleTime);
		InvincibleObj.SetActive(false);
		
	}
	IEnumerator TranJumpPar(float num)                    
	{
		
		yield return new WaitForSeconds(num);
		anim.SetBool("JumpPar",false);
		anim.SetBool("Jump",true);
		yield return new WaitForSeconds(0.1f);
		anim.SetBool("Jump",false);
	}
	
	
	IEnumerator TranBigJumpPar(float num)                    
	{
		yield return new WaitForSeconds(num);
		anim.SetBool("BigJumpPar",false);
		Start_Eff.SetActive(false);
		anim.SetBool("BigJump",true);
		Mid_Eff.SetActive(true);
		yield return new WaitForSeconds(0.35f);	//第一階段特效展開
		anim.SetBool("BigJump",false);
		Mid_Eff.SetActive(false);
				
	}

	IEnumerator messtime(float num)                    //玩家SpeedBuff
	{
		SpeedDownObj.SetActive(true);
		yield return new WaitForSeconds(num);
		SpeedDownObj.SetActive(false);
	}
	
	
	void OnTriggerEnter(Collider other)	
	{
		if(other.gameObject.name == "buff_speedup(Clone)")      //吃到SpeedBUFF
		{
			StartCoroutine(messtime(5));
		}
		
		
	}
}
