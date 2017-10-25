namespace YT.Auditing
{
    /// <summary>
    /// 处理空格
    /// </summary>
    public interface INamespaceStripper
    {/// <summary>
    /// 处理空格
    /// </summary>
    /// <param name="serviceName"></param>
    /// <returns></returns>
        string StripNameSpace(string serviceName);
    }
}