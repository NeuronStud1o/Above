using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneTaskButton : MonoBehaviour
{
    [SerializeField] private TasksManager tasksManager;
    public void DoneTask(int indexItem)
    {
        tasksManager.tasks[indexItem] = true;
        tasksManager.SaveGame();
    }
}
