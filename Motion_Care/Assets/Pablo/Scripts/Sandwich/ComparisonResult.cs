using UnityEngine;

public class ComparisonResult
{
    // VARIABLES //

    private int _nCorrect;
    private int _nWrong;

    private int _over;
    private int _under;

    private int _totalWrong;


    // CONSTRUCTOR //

    public ComparisonResult(int nCorrect = 0, int nWrong = 0, int over = 0, int under = 0)
    {
        _nCorrect = nCorrect;
        _nWrong = nWrong;
        _over = over;
        _under = under;
    }
    
    
    // GETTERS && SETTERS //
    
    public int GetCorrect() { return _nCorrect; }
    public int GetWrong() { return _nWrong; }
    public int GetOver() { return _over; }
    public int GetUnder() { return _under; }
    
    public void SetCorrect(int nCorrect) { _nCorrect = nCorrect; }
    public void SetWrong(int nWrong) { _nWrong = nWrong; }
    public void SetOver(int over) { _over = over; }
    public void SetUnder(int under) { _under = under; }
    
    
    // METHODS && FUNCTIONS //

    public void SaveResultData()
    {
        _totalWrong = _nWrong + _over + _under;
        
        // TODO: Llamada a JSON
    }
}
