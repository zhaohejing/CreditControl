namespace YT.Configuration.Dto
{/// <summary>
/// 
/// </summary>
    public class GeneralSettingsEditDto
    {/// <summary>
    /// 
    /// </summary>
        public string Timezone { get; set; }

        /// <summary>
        /// This value is only used for comparing user's timezone to default timezone
        /// </summary>
        public string TimezoneForComparison { get; set; }
    }
}