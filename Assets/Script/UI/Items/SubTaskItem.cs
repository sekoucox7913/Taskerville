using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SubTaskItem : MonoBehaviour
{
    [SerializeField] private Text taskNameIF;
    [SerializeField] private Toggle toggle;
    [SerializeField] private CanvasGroup canvasGroup;
    private LSubTask task = null;
    private EntryItem parent = null;
    // Start is called before the first frame update
    void Start()
    {
        toggle.onValueChanged.AddListener(delegate
        {
            toggle_valueChanged(toggle);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTask(LSubTask task, EntryItem parent = null)
    {
        this.task = task;
        this.parent = parent;
        taskNameIF.text = task.taskName;
        float ratio = UnityEngine.Screen.height / (float)UnityEngine.Screen.width;
        taskNameIF.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1330.0f / ratio, 0f);
        toggle.isOn = task.isCompleted();
        //UpdateCanvasGroup(task.isEnabled());
    }

    private void toggle_valueChanged(Toggle toggle)
    {
        if (toggle.isOn && task.isEnabled() == true)
        {
            TaskViewController.Instance.OnComplete(task);
        }
        if (toggle.isOn == false)
        {
            TaskViewController.Instance.CancelComplete(task);
        }

        if (this.parent != null)
        {
            this.parent.ReloadUI();
        }
    }

    private void UpdateCanvasGroup(bool isEnable)
    {
        if (isEnable)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
