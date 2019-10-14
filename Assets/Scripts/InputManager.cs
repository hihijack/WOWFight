using UnityEngine;

public class InputManager : BaseInput
{
    int lastFrameRollBtn;
    int rollBtnPressedF;

    public static float AxisMinVal = 0.2f;

    public InputManager() : base()
    {
        
    }

    public override void OnUpdate()
    {
        float axisH = Input.GetAxis("Horizontal");
        float axisV = Input.GetAxis("Vertical");
        axisV = 0;

        if (Input.GetButton("AtkL"))
        {
//            GameManager.Inst.targetRole.CharaCtl.SendCommand(cAtkL);
            cSkill.skillID = 2;
            GameManager.Inst.targetRole.CharaCtl.SendCommand(cSkill);
        }

        if (Input.GetButtonDown("AtkH"))
        {
            cAtkH.pressing = true;
            GameManager.Inst.targetRole.CharaCtl.SendCommand(cAtkH);
        }
        else if (Input.GetButtonUp("AtkH"))
        {
            bool changed = cAtkH.pressing != false;
            cAtkH.pressing = false;
            if (changed == true)
            {
                GameManager.Inst.targetRole.CharaCtl.SendCommand(cAtkH);
            }
        }

        if (Input.GetButtonDown("Roll"))
        {
            lastFrameRollBtn = Time.frameCount;
            if (Mathf.Abs(axisH) <= InputManager.AxisMinVal && Mathf.Abs(axisV) <= InputManager.AxisMinVal)
            {
                //后跳
                cRoll.dir = Vector3.zero;
                GameManager.Inst.targetRole.CharaCtl.SendCommand(cRoll);
            }
            rollBtnPressedF = 0;
        }
        else if (Input.GetButtonUp("Roll"))
        {
            if (Time.frameCount - lastFrameRollBtn <= 30)
            {
                //短按翻滚
                if (Mathf.Abs(axisH) > InputManager.AxisMinVal || Mathf.Abs(axisV) > InputManager.AxisMinVal)
                {
                    cRoll.dir = new Vector3(axisH, 0f, axisV);
                    GameManager.Inst.targetRole.CharaCtl.SendCommand(cRoll);
                }
            }
            rollBtnPressedF = 0;
        }

        bool rush = false;
        if (Input.GetButton("Roll"))
        {
            rollBtnPressedF++;
            if (rollBtnPressedF > 15)
            {
                //长按 冲刺
                cRush.h = axisH;
                cRush.v = axisV;
                cRush.dir = new Vector3(axisH, 0f, axisV);
                GameManager.Inst.targetRole.CharaCtl.SendCommand(cRush);
                rush = true;
            }
        }

        if (Input.GetButtonDown("LB"))
        {
            GameManager.Inst.targetRole.CharaCtl.SendCommand(cParry);
        }

        if (Input.GetButtonDown("LT"))
        {
            //TODO
            cSkill.skillID = 4;
            GameManager.Inst.targetRole.CharaCtl.SendCommand(cSkill);
        }
        
        if (!rush)
        {
            cMove.h = axisH;
            cMove.v = axisV;
            GameManager.Inst.targetRole.CharaCtl.SendCommand(cMove);
        }

        if (Input.GetButtonDown("A") && GameManager.Inst.curStayTrigger != null)
        {
            GameManager.Inst.curStayTrigger.OnActive();
        }
    }
}
