using System;
using System.Collections.Generic;

public class EventsContainer
{
    public delegate List<IngredientsController.IngredientDisposition>  SaveLevelEvent();

    public SaveLevelEvent OnSaveLevelEvent;
    public Action OnCreateNewLevelEvent;
    public Action OnRebuildSameLevelEvent;
    public Action<List<IngredientsController.IngredientDisposition>> OnLoadLevelEvent;

    public void RebuildSameLevel()
    {
        OnRebuildSameLevelEvent?.Invoke();
    }

    public void CreateNewLevel()
    {
        OnCreateNewLevelEvent?.Invoke();
    }

    public List<IngredientsController.IngredientDisposition> GetLevelInformationToSave()
    {
        return OnSaveLevelEvent?.Invoke();
    }

    public void LoadLevel(List<IngredientsController.IngredientDisposition> _disposition)
    {
        OnLoadLevelEvent?.Invoke(_disposition);
    }
}
