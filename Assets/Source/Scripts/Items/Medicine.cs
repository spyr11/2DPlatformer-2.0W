public class Medicine : Item
{
    private float _healValue = 50f;

    public override float GetValue()
    {
        return _healValue;
    }
}