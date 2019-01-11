using UnityEngine;


public class PlayerController3 : MonoBehaviour {

    private CsoundUnity csoundUnity;
    public GameObject missile;
    public GameObject eqColumn;
    float shootForce = 40;
    public AudioClip audioClip;

    GameObject[] eqColumns;
    public GameObject[] eqSettings;

    void Awake()
    {
        csoundUnity = GetComponent<CsoundUnity>();
    }

    // Use this for initialization
	void Start ()
    {
        //allocate each of the EQ columns, and the EQ curve columns
        eqColumns = new GameObject[64];
        eqSettings = new GameObject[64];

        //create the columns used to display the FFT data..
        for (int i = 0; i<64; i++)
        {
            Vector3 position = new Vector3(CsoundUnity.Remap(i, 0, 64, -8, 8), -10f, 1.25f);
            GameObject eqBand = (GameObject)Instantiate(eqColumn, position, Quaternion.identity);
            eqBand.name = "Column" + i.ToString();
            eqColumns[i] = eqBand;
        }

        //create the columsn used for setting teh EQ curve
        for (int i = 0; i < 64; i++)
        {
            Vector3 position = new Vector3(CsoundUnity.Remap(i, 0, 64, -8, 8), -10f, 1f);
            GameObject eqBandSetting = (GameObject)Instantiate(eqColumn, position, Quaternion.identity);
            eqBandSetting.GetComponent<Renderer>().material.color = new Color(.1f, .8f, .1f, .5f);
            eqBandSetting.name = "Column" + i.ToString();
            eqSettings[i] = eqBandSetting;
        }

        //send a score event to Csound to start instrument "FFT2TABLE". This could have also been hard coded to the 
        //to the Csound file. See 
        csoundUnity.sendScoreEvent("i\"FFT2Table\" 0 3600");
        InvokeRepeating("DrawFFT", .5f, .1f);
        


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = new Vector3(transform.position.x,
                          transform.position.y + .25f,
                          transform.position.z + 0.5f);
            Vector3 newPos = transform.forward;
            newPos.y = newPos.y + 1;
            GameObject bulletClone = (GameObject)Instantiate(missile, pos, Quaternion.identity);
            bulletClone.gameObject.name = "Missile(Clone)";


            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y+100, 0));
            bulletClone.GetComponent<Rigidbody>().AddForce(ray.direction * shootForce, ForceMode.Impulse);
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 50 * Time.deltaTime, 0));
    }

    void DrawFFT()
    {
        for (int i = 0; i < 64; i++)
        {
            if (csoundUnity != null)
            {
                float yPosition = CsoundUnity.Remap((float)csoundUnity.getTableSample(1001, i), 0, 1, -10, 0);
                eqColumns[i].transform.position = new Vector3(eqColumns[i].transform.position.x, yPosition+5, eqColumns[i].transform.position.z);
            }
        }
    }

    public void setAmpForBand(int bandNo, float amp)
    {
        string band = "band" + bandNo.ToString() + "amp";
        csoundUnity.setChannel(band, amp);
    }
}
