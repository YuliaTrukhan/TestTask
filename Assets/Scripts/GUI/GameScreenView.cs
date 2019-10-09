using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InteractableObjectIcon
{
    public ObjectType Type;
    public Sprite Sprite;
}
public class GameScreenView : BaseScreenView
{
    [SerializeField] private InteractableObjectIcon[] interactableObjectsPreferences;
    [SerializeField] private ObjectIconView objectIconTemplate;
    [SerializeField] private Transform iconsParent;
    private List<ObjectIconView> pooledIconViews = new List<ObjectIconView>();

    public void InitSeqenceView(List<ObjectType> sequence)
    {
        for(int i=0; i<sequence.Count; i++)
        {

            if (i >= pooledIconViews.Count)
            {
                var objectIcon = Instantiate(objectIconTemplate, iconsParent, false);
                objectIcon.Init(GetIconByType(sequence[i]));
                pooledIconViews.Add(objectIcon);
            }
            else
            {
                pooledIconViews[i].Init(GetIconByType(sequence[i]));
            }
        }

        Show();
    }

    private Sprite GetIconByType(ObjectType type)
    {
        for(int i=0; i< interactableObjectsPreferences.Length; i++)
        {
            if(interactableObjectsPreferences[i].Type == type)
            {
                return interactableObjectsPreferences[i].Sprite;
            }
        }

        Debug.LogError("Icon with type " + type + " does not exist!");
        return null;
    }
}
