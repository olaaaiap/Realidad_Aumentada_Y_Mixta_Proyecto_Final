using UnityEngine;

public class ComparisonResult
{
    // VARIABLES //

    private int _nCorrect;
    private int _nWrong;

    private int _over;
    private int _under;

    private int _totalWrong;

    private float _time;


    // CONSTRUCTOR //

    public ComparisonResult(int nCorrect = 0, int nWrong = 0, int over = 0, int under = 0, float time = 0)
    {
        _nCorrect = nCorrect;
        _nWrong = nWrong;
        _over = over;
        _under = under;
        _totalWrong = nWrong + over + under;

        _time = time;
    }
    
    
    // GETTERS && SETTERS //
    
    public int GetCorrect() { return _nCorrect; }
    public int GetWrong() { return _nWrong; }
    public int GetOver() { return _over; }
    public int GetUnder() { return _under; }
    public int GetTotalWrong() { return _totalWrong; }
    public float GetTime() { return _time; }
    
    public void SetCorrect(int nCorrect) { _nCorrect = nCorrect; }
    public void SetWrong(int nWrong) { _nWrong = nWrong; }
    public void SetOver(int over) { _over = over; }
    public void SetUnder(int under) { _under = under; }
    public void SetTotalWrong(int totalWrong) { _totalWrong = totalWrong; }
    public void SetTime(float time) { _time = time; }
    
    
    // METHODS && FUNCTIONS //

    public void SaveResultData()
    {
        _totalWrong = _nWrong + _over + _under;
        
        // TODO: Llamada a JSON
    }
    
    public int ComputeScore(int correctPoints = 10, int wrongPoints = -5, int overPoints = -3, int underPoints = -3)
    {
        int score = 0;
        
        score += _nCorrect * correctPoints;
        score -= _nWrong * wrongPoints;
        score -= _over * overPoints;
        score -= _under * underPoints;

        return Mathf.Max(score, 0);
    }

    public void Reset()
    {
        _nCorrect = 0;
        _nWrong = 0;
        _over = 0;
        _under = 0;
        _totalWrong = 0;
    }
}
