using UnityEngine;

public class Recipe: MonoBehaviour
{
    [SerializeField] GameObject firstComponent;
    [SerializeField] GameObject secondComponent;
    [SerializeField] GameObject result;

    public bool TryMakeRecipe(GameObject food1, GameObject food2, out GameObject result)
    {
        if ((food1 == firstComponent && food2 == secondComponent) || (food1 == secondComponent && food2 == firstComponent))
        {
            result = this.result;
            return true;
        }

        result = null;
        return false;
    }
}
