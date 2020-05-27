using P42.IO.Properties;
using System.Activities;
using System.ComponentModel;
using System.IO;

namespace P42.IO.Directory
{
    [LocalizedDisplayName(nameof(Resources.CreateFolderDisplayName))]
    [LocalizedDescription(nameof(Resources.CreateFolderDescription))]
    public sealed class CreateFolder : CodeActivity
    {
        [RequiredArgument]
        [LocalizedCategory(nameof(Resources.CategoryDirectory))]
        [LocalizedDisplayName(nameof(Resources.CreateFolderPathDisplayName))]
        [LocalizedDescription(nameof(Resources.CreateFolderPathDescription))]
        public InArgument<string> Path { get; set; }

        [Category(nameof(Resources.CategoryOutput))]
        [DisplayName(nameof(Resources.CreateFolderPathInfoDisplayName))]
        [Description(nameof(Resources.CreateFolderPathInfoDescription))]
        public OutArgument<DirectoryInfo> PathInfo { get; set; }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (Path == null) metadata.AddValidationError(string.Format(Resources.ValidationValueError, nameof(Path)));

            base.CacheMetadata(metadata);
        }

        protected override void Execute(CodeActivityContext context)
        {
            var path = context.GetValue(Path);
            var result = DirectoryHelper.CreateDirectory(path);
            context.SetValue(PathInfo, result);
        }
    }
}
