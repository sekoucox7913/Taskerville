namespace SA.CrossPlatform.UI
{
    /// <summary>
    /// Constants indicating the state of the wheel picker result.
    /// </summary>
    public enum UM_WheelPickerState
    {
        /// <summary>
        ///  User picked variant but picker dialog is still open.
        /// </summary>
        InProgress,

        /// <summary>
        /// User picked variant and closed the picker dialog.
        /// </summary>
        Done,

        /// <summary>
        ///  User canceled and closed the picker dialog.
        /// </summary>
        Canceled
    }
}
