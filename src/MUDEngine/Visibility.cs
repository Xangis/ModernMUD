namespace MUDEngine
{
    /// <summary>
    /// Visibility states.  Used in the HowSee function to determine what the target's
    /// visilbity level is from the looker's viewpoint.
    /// </summary>
    public enum Visibility
    {
        /// <summary>
        /// Cannot see at all.
        /// </summary>
        invisible = 0,
        /// <summary>
        /// Can see fully.
        /// </summary>
        visible = 1,
        /// <summary>
        /// Too dark to see.
        /// </summary>
        too_dark = 2,
        /// <summary>
        /// Can sense a presence, but not see it.
        /// </summary>
        sense_hidden = 3,
        /// <summary>
        /// Can sense a living being, but can not see it in great detail.
        /// </summary>
        sense_infravision = 4
    }
}
