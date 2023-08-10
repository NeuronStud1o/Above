using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneTaskButton : MonoBehaviour
{
    public void DoneTask(int indexItem)
    {
        TasksManager.tasks[indexItem] = true;
        TasksManager.SaveGame();
    }
}
