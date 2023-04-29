using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Collectable> _collectables;
    private Transform collectoblesParent;
    private MovementController _movementController;
    // Start is called before the first frame update
    void Start()
    {
        collectoblesParent = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeCollectable(Collectable collectable)
    {
        _collectables.Add(collectable);
        collectable.transform.SetParent(collectoblesParent);
        collectable.transform.localPosition = new Vector3(0,_collectables.Count*2,0);
        _movementController.movementSpeed -= collectable.slowDown;
    }
    public void DropCollectable()
    {
        int num = _collectables.Count-1;
        _collectables[num].transform.SetParent(null);
        _collectables[num].transform.localPosition -= new Vector3(0,_collectables.Count*2,0);
        _movementController.movementSpeed += _collectables[num].slowDown;
        
        _collectables.RemoveAt(num);
    }
    
}