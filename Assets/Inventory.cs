using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]private InventoryCell[] cells;
    public InventoryCell[] Cells { get { return cells; } }
    private void Awake()
    {  
    }
}
