namespace YT
{
    public interface IAppFolders
    {
         string TempFolder { get; set; }

         string ImageFolder { get; set; }
        /// <summary>
        /// ģ���ļ���
        /// </summary>
         string TempleteFolder { get; set; }
        string LogsFolder { get; set; }
      
    }
}