using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskObj : MonoBehaviour
{
    [SerializeField] Text taskDetails;
    public void SetData(TaskBase task)
    {
        taskDetails.text = task.Name + ": " + task.Description;
    }
}
