using WebApplication1.Data;

namespace WebApplication1.Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =false)]
    public class CheckPermissionAttribute : Attribute
    {
        public CheckPermissionAttribute(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; }
    }
}
