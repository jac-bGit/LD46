using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    //general gererating settings
    [SerializeField] private GameObject[] _groudObsGO;
    [SerializeField] private GameObject[] _airObsGO;
    [Range(5,100)]
    [SerializeField] private float _minGenRange;
    private int usingPartIndex;
    private float genXPos;
    private int genAirDelayCounting;

    //parts
    [SerializeField] private PartGeneretion[] _parts;

    private Transform _lastGenerated;

    //references
    private Transform _playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        //get references
        _playerTransform = GameObject.FindWithTag("Player").transform;

        //setup
        usingPartIndex = 0;
        genXPos = _parts[0].startPos;
        genAirDelayCounting = 0;

        PartGenerating(_parts[usingPartIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        //set part
        // for(int i = 0; i < _parts.Length; i++){
        //     float playerX = _playerTransform.position.x;
        //     if(playerX > _parts[i].startPos && playerX < _parts[i].endPos)
        //         usingPartIndex = i;
        // }

        //parts progression
        float playerX = _playerTransform.position.x;
        if(playerX > _parts[usingPartIndex].endPos){
            if(usingPartIndex < _parts.Length){
                usingPartIndex++;
                genXPos = _parts[usingPartIndex].startPos;
            }
        }
        PartGeneretion part = _parts[usingPartIndex];

        //gen
        if(_minGenRange > genXPos - _playerTransform.position.x)
            PartGenerating(part);
    }

#region GENERATING
    void PartGenerating(PartGeneretion part){
        GenerateGroundObst(part);
        //air generating
        if(part.generateAir){
            if(genAirDelayCounting >= part.genAirDelay){
                GenerateAirObst(part);
                genAirDelayCounting = 0;
            }
            else
                genAirDelayCounting++;
        }
    }

    void GenerateGroundObst(PartGeneretion part){
        int rand = Random.Range(0, _groudObsGO.Length);
        float randXPos = Random.Range(part.minGroundSpacing, part.maxGroundSpacing);
        Vector3 pos = new Vector3(genXPos + randXPos, transform.position.y, 0);
        GameObject obst = Instantiate(_groudObsGO[rand], pos, transform.rotation);

        _lastGenerated = obst.transform;
        genXPos = _lastGenerated.position.x;
    }

    void GenerateAirObst(PartGeneretion part){
        int rand = Random.Range(0, _airObsGO.Length);
        float randXPos = Random.Range(part.minAirSpacing, part.maxAirSpacing);
        Vector3 pos = new Vector3(genXPos + randXPos, transform.position.y, 0);
         GameObject obst = Instantiate(_airObsGO[rand], pos, transform.rotation);

        _lastGenerated = obst.transform;
        genXPos = _lastGenerated.position.x;
    }
#endregion

}

[System.Serializable]
public class PartGeneretion{
    //gererating settings
    public int startPos, endPos;
    [Range(5, 100)]
    public int maxGroundSpacing, minGroundSpacing;
    //air
    public bool generateAir;
    public int genAirDelay;
    [Range(5, 100)]
    public int maxAirSpacing, minAirSpacing;
}