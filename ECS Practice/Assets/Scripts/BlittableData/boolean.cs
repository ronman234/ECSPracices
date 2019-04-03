public struct blittablebool
{
    private readonly byte Value;

    public blittablebool(byte value)
    {
        Value = value;
    }
    public blittablebool(bool value)
    {
        Value = value ? (byte)1 : (byte)0;
    }
    public static implicit operator bool(blittablebool bb)
    {
        return bb.Value != 0;
    }

    public static implicit operator blittablebool(bool b)
    {
        return new blittablebool(b);
    }

    public override string ToString()
    {
        return ((bool)this).ToString();
    }
}
