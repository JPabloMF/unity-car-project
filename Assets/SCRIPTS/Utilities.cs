using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Utilities : MonoBehaviour
{
    public void useTimer(int time, System.Action<System.Threading.Tasks.Task> callback) {
        Task.Delay(time).ContinueWith(callback, TaskScheduler.FromCurrentSynchronizationContext());
    }
}
