

public struct CellIndex
{
    private int _rowIndex;
    private int _columnIndex;
    
    public int RowIndex => _rowIndex;
    public int ColumnIndex => _columnIndex;
    
    public CellIndex(int rowIndex, int columnIndex)
    {
        _rowIndex = rowIndex;
        _columnIndex = columnIndex;
    }

    public void SetValues(int rowIndex, int columnIndex)
    {
        _rowIndex = rowIndex;
        _columnIndex = columnIndex;
    }
}
