using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private FloatVariable Data1;

    private FloatData Data2;

  //  float Data3 = 0f;

    public Flowchart Data4;

    public string nametest = null;
    private Slider slider;
   
    public float MaxHealth = 100;
    public float Nowhp = 20;
    public Image Handle;
    //public Texture[] Test1;
    public Image Fill;  // assign in the editor the "Fill"
    
    public Image Background;
    
    public Color MaxHealthColor = Color.red;
    public Color MinHealthColor = Color.black;
    public Color oldColor = Color.white;
    public Color bkColor = Color.blue;
    private float valueBuffer ;
    private bool DrawFlage_plus =false;

    private bool DrawFlage_minuns = false;
    private bool BattleFlag = false;

    /**********************************************
     * 
     * 여기는 호감도를 관리하는 플레그 시작입니다
     *
     * *********************************************/

    private bool _main_F;

    private bool _square_F;

    private bool _forest_F;



    /**********************************************
 * 
 * 여기는 호감도를 관리하는 플레그 끝입니다
 *
 * *********************************************/


    private float subBuffer = 0f;

    private FloatData Fd;
    public Flowchart flowTest1;
    private float x = 1;
    public GameObject mainStreet;

    private float valBuffer;
    
        // 아침발기
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        //slider.value = Nowhp;
    }

    private void Start()
    {


        slider.wholeNumbers = true;        // I dont want 3.543 Health but 3 or 4
        //최소값 0
        slider.minValue = 0f;

        //매우중요 잊어먹지말자..
        //slider.value = Fd;

        //최대값 100
        slider.maxValue = MaxHealth;

        //초기호감도 20;
        //slider.value = Nowhp;

        //변화하는 슬라이더 값의 저장 
        //valueBuffer = slider.value;
        //Fill.color = Color.Lerp(bkColor, MaxHealthColor, slider.value);

        
        //사용법!!!!]


        Nowhp =  mainStreet.GetComponent<Flowchart>().GetFloatVariable("HartPoint_main");

       

        Debug.Log("스타트 hp" + Nowhp);

        valueBuffer = Nowhp;

        slider.value = Nowhp;
        

        Debug.Log("test ======"); 
    }


    private void Update()
    {

        Debug.Log("업데이트 hp" + Nowhp);

        Debug.Log("업데이트 중인 현재 HartPoint 값" + mainStreet.GetComponent<Flowchart>().GetFloatVariable("HartPoint"));

        Debug.Log("배틀플레그 값 : " + mainStreet.GetComponent<Flowchart>().GetBooleanVariable("Battle_F"));

        Debug.Log("BattleFlag" + BattleFlag);

        slider.value = mainStreet.GetComponent<Flowchart>().GetFloatVariable("HartPoint");

        BattleFlag = mainStreet.GetComponent<Flowchart>().GetBooleanVariable("Battle_F");
        if ((Nowhp<slider.value)&&(!DrawFlage_plus))
        {
            valueBuffer = Nowhp;
            Debug.Log("버퍼값");

            Debug.Log(valueBuffer);
           
            subBuffer = slider.value;

            Debug.Log("서브버퍼값");

            Debug.Log(subBuffer);

            DrawFlage_plus = true;
        }

        if ((Nowhp > slider.value) && (!DrawFlage_minuns))
        {
            valueBuffer = Nowhp;
            Debug.Log("여기가 출력되는가");

            Debug.Log(valueBuffer);

            subBuffer = slider.value;

            Debug.Log("서브버퍼값");

            Debug.Log(subBuffer);

            DrawFlage_minuns = true;
        }



        if (DrawFlage_plus)
        {
            UpdateHealthBar(valueBuffer);

            valueBuffer++;

            Debug.Log("증가하고있다");

            if (valueBuffer >= subBuffer)
            {
                valueBuffer = subBuffer;
                Debug.Log("증가점까지 도달했다");
                DrawFlage_plus = false;
                Nowhp = slider.value;
            }

        

        }

        if (DrawFlage_minuns)
        {
            UpdateHealthBar(valueBuffer);

            valueBuffer--;

            Debug.Log("증가하고있다");

            if (valueBuffer <= subBuffer)
            {
                valueBuffer = subBuffer;
                Debug.Log("감소점까지 도달했다");
                DrawFlage_minuns = false;
                Nowhp = slider.value;
            }



        }
        if(BattleFlag)
        {
            Debug.Log("플레그는 활성화었는가1" + x);
           

            UpdateSucheBattle(x);
            x += 5;
            if (x >= 100)
            {
                Debug.Log("조건성립");
                BattleFlag = false;
                mainStreet.GetComponent<Flowchart>().SetBooleanVariable("Battle_F", false);
            }
        }
    }

    void UpdateHealthBar(float cnt)
    {
        //호감도의 값은 빨간색이다.
        slider.value = cnt;

        //min r, max g, 
        Fill.color = Color.Lerp(MinHealthColor, MaxHealthColor, cnt / MaxHealth);

    }

    void UpdateSucheBattle(float cnt)
    {
        Debug.Log("플레그는 활성화었는가2");
        
        Background.color = Color.Lerp(MinHealthColor, bkColor, cnt / MaxHealth);

    }
}