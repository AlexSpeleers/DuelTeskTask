public static class Helper 
{
    public static bool Timer( float pTimeLeft)
    {
        if (pTimeLeft <= 0.0f)
        {            
            return true;
        }            
        return false;
    }
}
