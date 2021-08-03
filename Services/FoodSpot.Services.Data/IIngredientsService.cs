namespace FoodSpot.Services.Data
{
    using System.Collections.Generic;

    public interface IIngredientsService
    {
        IEnumerable<T> All<T>();
    }
}
