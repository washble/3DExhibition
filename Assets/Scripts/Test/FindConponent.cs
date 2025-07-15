using UnityEngine;

public class FindConponent : MonoBehaviour
{
    [SerializeField] private Animator[] animator;

    [ContextMenu("Remove")]
    private void Sequence()
    {
        RemoveComponents(animator = FindComponents<Animator>());
    }
    
    private T[] FindComponents<T>() where T : Component
    {
        return gameObject.GetComponentsInChildren<T>();
    }

    private void RemoveComponents<T>(T[] components) where T : Component
    {
        for (int i = 0; i < components.Length; i++)
        {
            DestroyImmediate(components[i]);
        }
    }
}
