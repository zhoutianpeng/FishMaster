using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameManager : MonoBehaviour
{
    private static CameManager _instance;

    public static CameManager Instance{
        get{
            return _instance;
        }
    }
    public Image bgImage;
    public GameObject seaWaveEffect; 
    public Sprite[] bgSprite;
    public int bgIndex = 0; 

    public GameObject lvUpTips;
    public GameObject fireEffect; 
    public GameObject changeEffect; 
    public GameObject lvEffect; 
    public GameObject goldEffect; 
    public Text oneShotCostTest;
    public Text goldText;
    public Text lvText;
    public Text lvNameText;
    public Text smallCountdownText;
    public Text bigCountdownText;
    public Button bigCountdownButton;
    public Button backButton;
    public Button settingButton;
    public Slider expSlider;
    public int lv = 0;
    public int exp = 0;
    public int gold = 500;
    public const int bigCountDown = 240;
    public const int smallCountDown = 60;
    public float bigTimer = bigCountDown;
    public float smallTimer = smallCountDown;
    public Color goldColor;
    public Transform bulletHolder;
    public GameObject[] gunGos;
    public GameObject[] bullet1Gos;
    public GameObject[] bullet2Gos;
    public GameObject[] bullet3Gos;
    public GameObject[] bullet4Gos;
    public GameObject[] bullet5Gos;

    public int bulletIndex = 0;
    private int[] oneShotCosts = {5, 10, 20 , 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};

    private string []lvName = {"新手","入门","钢铁","青铜","白银","黄金","白金","钻石","大师","宗师"};
    
    void Awake(){
        _instance = this;
    }

    void ChangeBg(){
        if(bgIndex != lv /20){
            bgIndex = lv/20;
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.seaWaveClip);
            //bgIndex = (bgIndex >= 3) ? 3 : bgIndex;
            Instantiate(seaWaveEffect);
            if(bgIndex >= 3){
                bgImage.sprite = bgSprite[3];
            }
            else{
                 bgImage.sprite = bgSprite[bgIndex];
            }
            
        }
    }

    void Start(){
        gold = PlayerPrefs.GetInt("gold",gold);
        lv = PlayerPrefs.GetInt("lv",lv);
        exp = PlayerPrefs.GetInt("exp",exp);
        smallTimer = PlayerPrefs.GetInt("scd",smallCountDown);
        bigTimer = PlayerPrefs.GetInt("bcd",bigCountDown);
        UpdateUI();
    }

    void UpdateUI(){

        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;

        if(smallTimer <= 0){
            smallTimer = smallCountDown;
            gold += 50;
        }

        if(bigTimer <= 0 && bigCountdownButton.gameObject.activeSelf == false){
            bigCountdownText.gameObject.SetActive(false);
            bigCountdownButton.gameObject.SetActive(true);
        }
        // exp2lv 
        // up exp = 1000 + 200 * current lv 
        while(exp >= 1000 + 200 * lv){
            exp = exp - (1000 + 200 * lv);
            lv++;
            lvUpTips.SetActive(true);
            lvUpTips.transform.Find("Text").GetComponent<Text>().text = lv.ToString();
            StartCoroutine(lvUpTips.GetComponent<Ef_HideSelf>().HideSelf(0.6f));
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.leveUpClip);
            Instantiate(lvEffect);

        }
        goldText.text = "$" + gold;
        lvText.text = lv.ToString();
        if((lv/10) <= 9){
            lvNameText.text = lvName[lv / 10];
        }
        else{
            lvNameText.text = lvName[9];
        }

        smallCountdownText.text = "  "+ (int)smallTimer/10+ "  " + (int)smallTimer%10;
        bigCountdownText.text = (int)bigTimer + "s";
        expSlider.value = ((float)exp) / (1000 + 200 * lv);
    }

    void Update(){
        ChangeBg();
        Fire();
        ChangeBulletCost();
        UpdateUI();
    }


    // TODO: reconstruct fire part
    void Fire(){
        GameObject[] useBullets = bullet5Gos;
        int currentIndex;
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            if( gold - oneShotCosts[bulletIndex] >= 0 ){

                //Debug.Log("Pressed mouse.");
                switch (bulletIndex / 4){
                    case 0:useBullets = bullet1Gos; break;
                    case 1:useBullets = bullet2Gos; break;
                    case 2:useBullets = bullet3Gos; break;
                    case 3:useBullets = bullet4Gos; break;
                    case 4:useBullets = bullet5Gos; break;
                }
                currentIndex = (lv % 10 >= 9) ? 9 : lv % 10;

                gold -=  oneShotCosts[bulletIndex];
                AudioManager.Instance.PlayEffectSound(AudioManager.Instance.fireClip);
                Instantiate(fireEffect);
                GameObject bullet = Instantiate(useBullets[currentIndex]);
                bullet.transform.SetParent(bulletHolder, false);
                bullet.transform.position = gunGos[bulletIndex / 4].transform.Find("FirePos").transform.position;
                bullet.transform.rotation = gunGos[bulletIndex / 4].transform.rotation;
                bullet.GetComponent<BulletAttr>().damage = oneShotCosts[bulletIndex];
                bullet.AddComponent<Ef_AutoMove>().dir = Vector3.up;
                bullet.GetComponent<Ef_AutoMove>().speed = bullet.GetComponent<BulletAttr>().speed;
            }
            else{
                 // Flash the text
                    StartCoroutine(GoldNotEnough());
            }

            
        }
    }

    IEnumerator GoldNotEnough(){
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        goldText.color = goldColor;
    }

    public void ChangeBulletCost(){
        if(Input.GetAxis("Mouse ScrollWheel") < 0){
            onButtonMDown(); 
        }
        if(Input.GetAxis("Mouse ScrollWheel") >0){
            onButtonPDown(); 
        }
    } 

    public void onButtonPDown(){
        gunGos[bulletIndex/4].SetActive(false);

        bulletIndex++; //= (bulletIndex++) % oneShotCost.Length;

        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.changeClip);

        Instantiate(changeEffect);

        bulletIndex = (bulletIndex > oneShotCosts.Length -1) ? 0 : bulletIndex ;

        gunGos[bulletIndex/4].SetActive(true);

        oneShotCostTest.text = "$"+ oneShotCosts[bulletIndex];
    }

    public void onButtonMDown(){
        gunGos[bulletIndex/4].SetActive(false);

        bulletIndex--; 

        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.changeClip);

       Instantiate(changeEffect);

        bulletIndex = (bulletIndex < 0 ) ? oneShotCosts.Length -1 : bulletIndex ;
        
        gunGos[bulletIndex/4].SetActive(true);

        oneShotCostTest.text = "$"+ oneShotCosts[bulletIndex];
    }

    public void OnBigCountDOwnButtonDown(){
        gold += 50;
        
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.rewardClip);

        Instantiate(goldEffect);

        bigCountdownButton.gameObject.SetActive(false);

        bigCountdownText.gameObject.SetActive(true);

        bigTimer = bigCountDown;
    }
}
