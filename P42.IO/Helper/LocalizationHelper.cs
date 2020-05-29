using P42.IO.Properties;
using System;
using System.ComponentModel;

namespace P42.IO.Helper
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LocalizedCategoryAttribute : CategoryAttribute
    {
        public LocalizedCategoryAttribute(string category) : base(category)
        {
        }

        protected override string GetLocalizedString(string value) => Resources.ResourceManager.GetString(value) ?? base.GetLocalizedString(value);
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(string displayName) : base(displayName)
        {
        }

        public override string DisplayName => Resources.ResourceManager.GetString(DisplayNameValue) ?? base.DisplayName;
    }

    public sealed class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        public LocalizedDescriptionAttribute(string displayName) : base(displayName)
        {
        }

        public override string Description => Resources.ResourceManager.GetString(DescriptionValue) ?? base.Description;
    }
}