using System;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;

// Do NOT add anything that needs system.IO here

namespace Skyline.DataMiner.CICD.FileSystem
{
    public static class AccessPermissions
    {
        ///// <summary>
        ///// Check if directory or file has write permissions.
        ///// </summary>
        ///// <param name="accessControlList">The access control list for the directory or file.</param>
        ///// <returns>True if directory has the correct write permissions.</returns>
        //public static bool HasWritePermissionOnDir(DirectorySecurity accessControlList)
        //{
        //    var writeAllow = false;
        //    var writeDeny = false;

        //    if (accessControlList == null)
        //        return false;
        //    AuthorizationRuleCollection accessRules = accessControlList.GetAccessRules(
        //        true,
        //        true,
        //        typeof(SecurityIdentifier));
        //    if (accessRules == null)
        //        return false;

        //    foreach (FileSystemAccessRule rule in accessRules)
        //    {
        //        if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
        //            continue;

        //        if (rule.AccessControlType == AccessControlType.Allow)
        //        {
        //            writeAllow = true;
        //        }
        //        else
        //        {
        //            writeDeny = true;
        //        }
        //    }

        //    return writeAllow && !writeDeny;
        //}

        ///// <summary>
        ///// Waits until timeout time or until the directory has write permissions. 
        ///// </summary>
        ///// <param name="accessControlList">The access control list for the directory or file.</param>
        ///// <param name="timeout">Maximum time to wait.</param>
        ///// <returns>True if directory has the correct write permissions.</returns>
        //public static bool WaitOnWritePermission(DirectorySecurity accessControlList, TimeSpan timeout)
        //{
        //    var sw = new Stopwatch();
        //    sw.Start();

        //    while (!HasWritePermissionOnDir(accessControlList) && sw.ElapsedMilliseconds < timeout.TotalMilliseconds)
        //    {
        //        // Do nothing.
        //    }

        //    return HasWritePermissionOnDir(accessControlList);
        //}

        /// <summary>
        /// Check if directory or file has write permissions.
        /// </summary>
        /// <param name="accessControlList">The access control list for the directory or file.</param>
        /// <returns>True if directory has the correct write permissions.</returns>
        public static bool HasWritePermissionOnDir(FileSecurity fileSecurity)
        {
            var writeAllow = false;
            var writeDeny = false;

            if (fileSecurity == null)
                return false;
            AuthorizationRuleCollection accessRules = fileSecurity.GetAccessRules(
                true,
                true,
                typeof(SecurityIdentifier));
            if (accessRules == null)
                return false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                    continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                {
                    writeAllow = true;
                }
                else
                {
                    writeDeny = true;
                }
            }

            return writeAllow && !writeDeny;
        }

        /// <summary>
        /// Waits until timeout time or until the directory has write permissions. 
        /// </summary>
        /// <param name="accessControlList">The access control list for the directory or file.</param>
        /// <param name="timeout">Maximum time to wait.</param>
        /// <returns>True if directory has the correct write permissions.</returns>
        public static bool WaitOnWritePermission(FileSecurity fileSecurity, TimeSpan timeout)
        {
            var sw = new Stopwatch();
            sw.Start();

            while (!HasWritePermissionOnDir(fileSecurity) && sw.ElapsedMilliseconds < timeout.TotalMilliseconds)
            {
                // Do nothing.
            }

            return HasWritePermissionOnDir(fileSecurity);
        }
    }
}