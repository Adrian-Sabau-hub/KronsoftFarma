using System;

namespace KF.WPF.Client.Core.APIClient
{
    public interface IDataLoggingContextProvider
    {
        Guid? GetCurrentUserId();
        string GetCurrentUserName();
        Guid? GetCurrentAreaId();
        string GetCurrentAreaName();
        string GetCurrentModuleName();
    }
}
