using Taav.Contracts.Interfaces;

namespace ManageShop.Infrastucture.Queries;

public class Sort<T> : ISort
    where T : new()
{
    private string? _expression;
    private Dictionary<string, string> _sortExpression;

    public Sort()
    {
        _sortExpression = new Dictionary<string, string>();
    }

    public string? Expression
    {
        get => _expression;
        set
        {
            _sortExpression = value.Parse<T>();
            _expression = value;
        }
    }

    public Dictionary<string, string> GetSortParameters() => _sortExpression;
}