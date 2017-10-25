using Abp.Dependency;

namespace YT
{
    /// <summary>
    /// �ļ�·���������
    /// </summary>
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        /// <summary>
        /// ��ʱ�ļ�
        /// </summary>
        public string TempFolder { get; set; }
        /// <summary>
        /// ģ���ļ���
        /// </summary>
        public  string TempleteFolder { get; set; }
        /// <summary>
        /// ͼƬ�ļ���
        /// </summary>
        public string ImageFolder { get; set; }
        /// <summary>
        /// ��־�ļ���
        /// </summary>
        public string LogsFolder { get; set; }
    }
}