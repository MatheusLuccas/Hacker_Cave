using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class code_logic : MonoBehaviour
{
	public Color [] clrlist;
	public GameObject [] val_obj;
	public Sprite [] flowchart, flowchart_shadow;
	public int [] param;
	public int comm_set;
	public float multiply = 0.75f, reduce_limit = 30, step_len = 0.25f, step_z = 10, top_limit = 2.25f;
	public GameObject cell_prev, cell_next, charge_bar, codeexe, score;
	public bool active;
	public TMP_Text txt, tiptxt, minortiptxt;
	public KeyCode correct_command, correct_command_b;
	public bool demomode, notip = false;
	bool[] declared = new bool[3];
	int aux = 0;
	public SpriteRenderer sprren_shadow;
	SpriteRenderer sprren;
	Rigidbody rigid;
	AudioSource sfx;
	//float timepenalty = 5.0f;
	
	public void alter_speed(float multiply, bool changeVar = false)
	{
		if(changeVar)
		{
			foreach (GameObject obj in val_obj)
			{
				if(obj != null)
				obj.gameObject.GetComponent<bin_logic>().alter_speed(multiply);
			}
		}
		
		this.gameObject.GetComponent<follow_target>().alter_speed(multiply);
		if(cell_next != null)
		{
			cell_next.gameObject.GetComponent<code_logic>().alter_speed(multiply);
		}
	}
	
	public bool declare_variable(int var_to_dec)
	{
		if(cell_prev == null)
		{
			return true;
		}
		else if(cell_prev.gameObject.GetComponent<code_logic>().comm_set == 0 && cell_prev.gameObject.GetComponent<code_logic>().param[0] == var_to_dec){
			return false;
		}else{
			return(cell_prev.gameObject.GetComponent<code_logic>().declare_variable(var_to_dec));
		}
	}

	public void restore_velocity(bool changeVar = false)
	{
		if(changeVar)
		{
			foreach (GameObject obj in val_obj)
			{
				if(obj != null)
				obj.gameObject.GetComponent<bin_logic>().restore_velocity();
			}
		}
		
		this.gameObject.GetComponent<follow_target>().restore_velocity();
		if(cell_prev != null)
		{
			cell_prev.gameObject.GetComponent<code_logic>().restore_velocity();
		}
	}
	/*public void solve_penalty()
	{
		restore_velocity(true);
	}
	public void penalty()
	{
		alter_speed(multiply, true);
        CancelInvoke("solve_penalty");
		Invoke(nameof(solve_penalty), timepenalty);
	}*/

	public void return_to_start()
	{
		sprren.color = clrlist[0];
		this.gameObject.GetComponent<follow_target>().return_to_start();
		this.gameObject.GetComponent<gyro_target>().return_to_start();
		if(cell_prev != null)
		{
			cell_prev.gameObject.GetComponent<code_logic>().return_to_start();
		}
		//minortiptxt.text = "?";
	}

	public void skip_one_line(float stepX, float stepY, float stepZ, bool comm_return = false)
	{
		if(!comm_return)
		{
			if(cell_prev != null){
				cell_prev.gameObject.GetComponent<code_logic>().skip_one_line(stepX, stepY, stepZ);
			}else{
				skip_one_line(stepX, stepY, stepZ, true);
				if(transform.position.y >= val_obj[0].transform.position.y + top_limit)
				{//se no topo				
					if(this.gameObject.GetComponent<gyro_target>().target == 0 && !this.gameObject.GetComponent<gyro_target>().active)
					{//se está com 0 grau e se não está girando
						this.gameObject.GetComponent<gyro_target>().target = 90;
						this.gameObject.GetComponent<gyro_target>().active = true;
					}
				}else
				{
					this.gameObject.GetComponent<follow_target>().step_to(stepX, stepY, stepZ);
				}
			}
		}else
		{
			if(cell_next != null)
			{
				cell_next.gameObject.GetComponent<code_logic>().skip_one_line(stepX, stepY, stepZ, true);
				cell_next.gameObject.GetComponent<follow_target>().targetX = this.gameObject.GetComponent<follow_target>().targetX;
				cell_next.gameObject.GetComponent<follow_target>().targetY = this.gameObject.GetComponent<follow_target>().targetY;
				cell_next.gameObject.GetComponent<follow_target>().targetZ = this.gameObject.GetComponent<follow_target>().targetZ;
				cell_next.gameObject.GetComponent<follow_target>().active = true;
				cell_next.gameObject.GetComponent<gyro_target>().target = this.gameObject.GetComponent<gyro_target>().target;
				cell_next.gameObject.GetComponent<gyro_target>().active = true;
			}
		}
	}

	public void back_one_line(float stepX, float stepY, float stepZ, bool comm_return = false)
	{
		if(!comm_return)
		{
			if(cell_next != null){
				cell_next.gameObject.GetComponent<code_logic>().back_one_line(stepX, stepY, stepZ);
			}else{
				back_one_line(stepX, stepY, stepZ, true);
				if(transform.position.y <= val_obj[0].transform.position.y - top_limit)
				{//se no baixo				
					if(this.gameObject.GetComponent<gyro_target>().target == 0 && !this.gameObject.GetComponent<gyro_target>().active)
					{//se está com 0 grau e se não está girando
						this.gameObject.GetComponent<gyro_target>().target = 90;
						this.gameObject.GetComponent<gyro_target>().active = true;
					}
				}else
				{
					this.gameObject.GetComponent<follow_target>().step_to(-stepX, -stepY, stepZ);
				}
			}
		}else
		{
			if(cell_prev != null)
			{
				cell_prev.gameObject.GetComponent<code_logic>().back_one_line(stepX, stepY, stepZ, true);
				cell_prev.gameObject.GetComponent<follow_target>().targetX = this.gameObject.GetComponent<follow_target>().targetX;
				cell_prev.gameObject.GetComponent<follow_target>().targetY = this.gameObject.GetComponent<follow_target>().targetY;
				cell_prev.gameObject.GetComponent<follow_target>().targetZ = this.gameObject.GetComponent<follow_target>().targetZ;
				cell_prev.gameObject.GetComponent<follow_target>().active = true;
				cell_prev.gameObject.GetComponent<gyro_target>().target = this.gameObject.GetComponent<gyro_target>().target;
				cell_prev.gameObject.GetComponent<gyro_target>().active = true;
			}
		}
	}

	public void draw_loop(int steps, bool erase = false)
	{
		if(cell_next != null && steps > 0)
		{
			cell_next.gameObject.GetComponent<code_logic>().draw_loop(steps - 1, erase);
		}
		if(erase)
		{
			sprren.color = clrlist[0];
		}else{
			sprren.color = clrlist[2];
		}
	}
	
/* commands set:
0 - input
1 - increment valiable
2 - compare, then go (if)
3 - loop
-1 - return
*/

	void comm_execute()
	{
		GameObject val_pointer = null;
		if(comm_set != -1 && comm_set != 3 && comm_set != 4)
		{
			val_pointer = val_obj[param[0]];
		}

		
//	0 - input
		
		if(comm_set == 0)
		{
			if(param[1] == 0){	//receive constant
				val_pointer.gameObject.GetComponent<bin_logic>().val = param[2];
			}else if(param[1] == 1){	//receive variable value
				val_pointer.gameObject.GetComponent<bin_logic>().val = val_obj[param[2]].gameObject.GetComponent<bin_logic>().val;
			}else{	//receive random value
				if(val_pointer.gameObject.GetComponent<bin_logic>().inputFont == -1){
					if(param[2] >= 0)
					{
						val_pointer.gameObject.GetComponent<bin_logic>().val = Random.Range(0, param[2] + 1);
					}else{
						val_pointer.gameObject.GetComponent<bin_logic>().val = Random.Range(-param[2], val_pointer.gameObject.GetComponent<bin_logic>().limit_val + 1);
					}
				}else
					val_pointer.gameObject.GetComponent<bin_logic>().val = val_pointer.gameObject.GetComponent<bin_logic>().inputFont;

			}
			val_pointer.gameObject.GetComponent<bin_logic>().active = true;
			
		
//	1 - increment valiable
		
		}else if(comm_set == 1)
		{
			val_pointer.gameObject.GetComponent<bin_logic>().val = (val_pointer.gameObject.GetComponent<bin_logic>().val + param[1]) % (val_pointer.gameObject.GetComponent<bin_logic>().limit_val + 1);

//	2 - compare, then go (if)
		
		}else if(comm_set == 2)
		{
			draw_loop(param[4], true);
			if(param[1] == 0){	//compare with value
				aux = param[2];
			}else{	//compare with varable
				aux = val_obj[param[2]].gameObject.GetComponent<bin_logic>().val;
			}
		
			if(!(param[3] == 0 && val_pointer.gameObject.GetComponent<bin_logic>().val < aux) && !(param[3] == 1 && val_pointer.gameObject.GetComponent<bin_logic>().val > aux) && !(param[3] == 2 && val_pointer.gameObject.GetComponent<bin_logic>().val == aux)){	//0 - minor 1 - major 2 - equal
				for(int i = 0; i < param[4]; i++)
				{
					//next_step();
					//prev_step(-step_len, step_len, step_z);
					skip_one_line(-step_len, step_len, step_z);
				}
			}
			
		
//	3 - loop
		
		}else if(comm_set == 3)
		{
			draw_loop(param[0], true);
			for(int i = 0; i < param[0]; i++)
			{
				back_one_line(-step_len, step_len, step_z);
			}
			
		
//	4 - jump
		
		}else if(comm_set == 4)
		{
			for(int i = 0; i < param[0]; i++)
			{
				skip_one_line(-step_len, step_len, step_z);
			}


//	-1 - return
		
		}else if(comm_set == -1)
		{
			restore_velocity(true);
		}
		if(comm_set == -1)
		{
			return_to_start();
			val_obj[0].gameObject.GetComponent<bin_logic>().val = 0;
			val_obj[1].gameObject.GetComponent<bin_logic>().val = 0;
			val_obj[2].gameObject.GetComponent<bin_logic>().val = 0;
			val_obj[0].gameObject.GetComponent<bin_logic>().active = false;
			val_obj[1].gameObject.GetComponent<bin_logic>().active = false;
			val_obj[2].gameObject.GetComponent<bin_logic>().active = false;
			tiptxt.text = "";
			if(charge_bar.gameObject.GetComponent<charge_bar>().overclock)
				charge_bar.gameObject.GetComponent<charge_bar>().increase(2);
			else if(demomode)
				charge_bar.gameObject.GetComponent<charge_bar>().increase(10);
			else
				charge_bar.gameObject.GetComponent<charge_bar>().increase(1);
		}else if(comm_set != 3 && comm_set != 4){
			//next_step();
			//prev_step(-step_len, step_len, step_z);
			skip_one_line(-step_len, step_len, step_z);
			sprren.color = clrlist[1];
		}
		tiptxt.color = clrlist[1];
		minortiptxt.color = clrlist[1];
		
	}
	
	void comm_label()
	{
		if(comm_set != -1 && comm_set != 3 && comm_set != 4)
		{
			txt.text = val_obj[param[0]].gameObject.GetComponent<bin_logic>().name.text;
		}
		
//	0 - input 
		
		if(comm_set == 0)
		{
			if(declare_variable(param[0]))
			{
				sprren.sprite = flowchart[1];
				sprren_shadow.sprite = flowchart_shadow[1];
			}
			txt.text += "←";
			if(param[1] == 0){	//receive constant
				txt.text += "" + param[2];
			}else if(param[1] == 1){	//receive variable value
				txt.text += val_obj[param[2]].gameObject.GetComponent<bin_logic>().name.text;
			}
			if(!notip)
				minortiptxt.text = "↓";

//	1 - increment valiable
		
		}else if(comm_set == 1)
		{
			if(param[1] > 0)
			{
				txt.text += "++";
				if(param[1] != 1)
				{
					txt.text += "" + param[1];
				}
			}else if(param[1] < 0)
			{
				
				txt.text += "--";
				if(param[1] != -1)
				{
					txt.text += "" + (-param[1]);
				}
			}
			if(!notip)
				minortiptxt.text = "↓";

//	2 - compare, then go (if)
		
		}else if(comm_set == 2)
		{
			sprren.sprite = flowchart[2];
			sprren_shadow.sprite = flowchart_shadow[2];
			if(param[3] == 0){	//minor
				txt.text += "<";
			}else if(param[3] == 1){	//major
				txt.text += ">";
			}else{	//equal
				txt.text += "=";
			}
			if(param[1] == 0){	//compare with value
				txt.text += "" + param[2];
			}else{	//compare with varable
				txt.text += val_obj[param[2]].gameObject.GetComponent<bin_logic>().name.text;
			}
			txt.text += "?";

//	3 - loop
		
		}else if(comm_set == 3)
		{
			sprren.sprite = flowchart[4];
			sprren_shadow.sprite = flowchart_shadow[4];
			txt.text = "loop";
			if(!notip)
				minortiptxt.text = "↑";

//	4 - jump
		
		}else if(comm_set == 4)
		{
			sprren.sprite = flowchart[5];
			sprren_shadow.sprite = flowchart_shadow[5];
			txt.text = "jump";
			if(!notip)
				minortiptxt.text = "→";
		
//	-1 - return
		
		}else if(comm_set == -1)
		{
			sprren.sprite = flowchart[0];
			sprren_shadow.sprite = flowchart_shadow[0];
			txt.text = "return";
			if(!notip)
				minortiptxt.text = "↑";
		}
	}

	void comm_prepare()
	{
		GameObject val_pointer = null;
		if(comm_set != -1 && comm_set != 3 && comm_set != 4)
		{
			val_pointer = val_obj[param[0]];
		}
		
//	0 - input
		
		if(comm_set == 0)
		{
			if(!notip)
				tiptxt.text = "↓";
			else
				tiptxt.text = "";
			correct_command = KeyCode.DownArrow;
			correct_command_b = KeyCode.S;

//	1 - increment valiable
		
		}else if(comm_set == 1)
		{
			if(!notip)
				tiptxt.text = "↓";
			else
				tiptxt.text = "";
			correct_command = KeyCode.DownArrow;
			correct_command_b = KeyCode.S;
			

//	2 - compare, then go (if)
		
		}else if(comm_set == 2)
		{
			//tiptxt.text = "Y↓ N→";
			
			val_pointer.gameObject.GetComponent<bin_logic>().play_all();
			draw_loop(param[4]);
			if(param[1] == 0){	//compare with value
				aux = param[2];
			}else{	//compare with varable
				aux = val_obj[param[2]].gameObject.GetComponent<bin_logic>().val;
				val_obj[param[2]].gameObject.GetComponent<bin_logic>().play_all();
			}
		
			if(!(param[3] == 0 && val_pointer.gameObject.GetComponent<bin_logic>().val < aux) && !(param[3] == 1 && val_pointer.gameObject.GetComponent<bin_logic>().val > aux) && !(param[3] == 2 && val_pointer.gameObject.GetComponent<bin_logic>().val == aux)){	//0 - minor 1 - major 2 - equal
				if(!notip)
				{
					//tiptxt.text = "←";
					tiptxt.text = "→";
					//minortiptxt.text = "→";
				}else
					tiptxt.text = "";
				//correct_command = KeyCode.LeftArrow;
				//correct_command_b = KeyCode.A;
				correct_command = KeyCode.RightArrow;
				correct_command_b = KeyCode.D;
			}else{
				if(!notip)
				{
					//tiptxt.text = "→";
					tiptxt.text = "↓";
					//minortiptxt.text = "↓";
				}else
					tiptxt.text = "";
				//correct_command = KeyCode.RightArrow;
				//correct_command_b = KeyCode.D;
				correct_command = KeyCode.DownArrow;
				correct_command_b = KeyCode.S;
			}
			

//	3 - loop
		
		}else if(comm_set == 3)
		{
			if(!notip)
				tiptxt.text = "↑";
			else
				tiptxt.text = "";
			correct_command = KeyCode.UpArrow;
			correct_command_b = KeyCode.W;
			
//	4 - jump
		
		}else if(comm_set == 4)
		{
			draw_loop(param[0]);
			if(!notip)
				tiptxt.text = "→";
			else
				tiptxt.text = "";
			correct_command = KeyCode.RightArrow;
			correct_command_b = KeyCode.D;
		
//	-1 - return
		
		}else if(comm_set == -1)
		{
			if(!notip)
				tiptxt.text = "↑";
			else
				tiptxt.text = "";
			correct_command = KeyCode.UpArrow;
			correct_command_b = KeyCode.W;
		}
		sprren.color = clrlist[4];
	}
    void Start()
    {
        sprren = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody>();
		if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
		comm_label();
    }
	
	void Update()
	{
		if(txt != null)
			txt.color = sprren.color;
			minortiptxt.color = sprren.color;
		notip = charge_bar.gameObject.GetComponent<charge_bar>().overclock;
		if(transform.position.y == val_obj[0].transform.position.y && rigid.velocity.magnitude == 0)//it will be the next codeline
		{
			if(active == false){
				comm_prepare();
				active = true;
			}
		}else{
			active = false;
		}
		if(active)
		{
			if(tiptxt.color != clrlist[3])
			{
				tiptxt.color = clrlist[4];
				minortiptxt.color = clrlist[4];
			}
			if(codeexe.gameObject.GetComponent<codeexe>().active){
				if(Input.GetKeyDown(correct_command) ||Input.GetKeyDown(correct_command_b) ||  (demomode && Input.anyKey && !Input.GetKey(KeyCode.Mouse0)))
				{
					comm_execute();
					foreach (GameObject obj in val_obj)
					{
						obj.gameObject.GetComponent<bin_logic>().stop_all();
					}
				}else if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0))
				{
					sprren.color = clrlist[3];
					tiptxt.color = clrlist[3];
					minortiptxt.color = clrlist[3];
					if(charge_bar.gameObject.GetComponent<charge_bar>().difficulty >= 2) 
					{
						if(charge_bar.gameObject.GetComponent<charge_bar>().combo != 0)//se o disp esta ativo, penaliza
							charge_bar.gameObject.GetComponent<charge_bar>().error_count += 2;
						if(reduce_limit < this.gameObject.GetComponent<follow_target>().velocity)
							alter_speed(multiply, true);
							//penalty();
					}
					if (gameObject.GetComponent<AudioSource>() != null)
        	        	sfx.Play();
				}
			}else{ //evitar que dois processos fiquem piando na hora da comparacao
				foreach (GameObject obj in val_obj)
				{
					obj.gameObject.GetComponent<bin_logic>().stop_all();
				}
			}
		}
	}
}
