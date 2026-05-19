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
        _totalWrong = nWrong + over + under;
    }
    
    
    // GETTERS && SETTERS //
    
    public int GetCorrect() { return _nCorrect; }
    public int GetWrong() { return _nWrong; }
    public int GetOver() { return _over; }
    public int GetUnder() { return _under; }
    public int GetTotalWrong() { return _totalWrong; }
    
    public void SetCorrect(int nCorrect) { _nCorrect = nCorrect; }
    public void SetWrong(int nWrong) { _nWrong = nWrong; }
    public void SetOver(int over) { _over = over; }
    public void SetUnder(int under) { _under = under; }
    public void SetTotalWrong(int totalWrong) { _totalWrong = totalWrong; }
    
    
    // METHODS && FUNCTIONS //

    public void SaveResultData()
    {
        _totalWrong = _nWrong + _over + _under;
        
        // TODO: Llamada a JSON
    }
    
    public int ComputeScore()
    {
        int score = 0;
        
        score += _nCorrect * 10;
        score -= _nWrong * 5;
        score -= _over * 3;
        score -= _under * 3;

        return Mathf.Max(score, 0);
    }
}
