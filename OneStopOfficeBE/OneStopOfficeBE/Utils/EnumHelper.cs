using OneStopOfficeBE.Constants;

namespace OneStopOfficeBE.Utils
{
    public class EnumHelper
    {
        public static bool IsValidStatus(string status)
        {
            foreach (StatusEnum s in Enum.GetValues(typeof(StatusEnum)))
            {
                if (status.Equals(s.ToString()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
