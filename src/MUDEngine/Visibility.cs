namespace MUDEngine
{
    /// <summary>
    /// Visibility states.  Used in the HowSee function to determine what the target's
    /// visilbity level is from the looker's viewpoint.
    /// </summary>
    public enum Visibility
    {
        invisible = 0,
        visible = 1,
        too_dark = 2,
        sense_hidden = 3,
        sense_infravision = 4
    }
}
