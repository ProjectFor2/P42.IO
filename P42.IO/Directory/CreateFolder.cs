using P42.IO.Helper;
using P42.IO.Properties;
using System;
using System.Activities;
using System.Diagnostics;
using System.IO;

namespace P42.IO.Directory
{
    [LocalizedDisplayName(nameof(Resources.CreateFolderDisplayName))]
    [LocalizedDescription(nameof(Resources.CreateFolderDescription))]
    public sealed class CreateFolder : CodeActivity
    {
        public CreateFolder()
        {
#if DEBUG
            new DesignerMetadata().Register();
#endif        
        }

        [LocalizedCategory(nameof(Resources.CategoryCommon))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnErrorDisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnErrorDescription))]
        public InArgument<bool> ContinueOnError { get; set; }

        [RequiredArgument]
        [LocalizedCategory(nameof(Resources.CategoryDirectory))]
        [LocalizedDisplayName(nameof(Resources.CreateFolderPathDisplayName))]
        [LocalizedDescription(nameof(Resources.CreateFolderPathDescription))]
        public InArgument<string> Path { get; set; }

        [LocalizedCategory(nameof(Resources.CategoryOutput))]
        [LocalizedDisplayName(nameof(Resources.CreateFolderPathInfoDisplayName))]
        [LocalizedDescription(nameof(Resources.CreateFolderPathInfoDescription))]
        public OutArgument<DirectoryInfo> PathInfo { get; set; }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (Path == null) metadata.AddValidationError(string.Format(Resources.ValidationValueError, nameof(Path)));

            base.CacheMetadata(metadata);
        }

        protected override void Execute(CodeActivityContext context)
        {
            var continueOnError = context.GetValue(ContinueOnError);
            var path = context.GetValue(Path);

            try
            {
                var result = DirectoryHelper.CreateDirectory(path);
                context.SetValue(PathInfo, result);
            }
            catch (Exception ex)
            {
                if (continueOnError)
                {
                    Trace.TraceError(ex.ToString());
                }
                else
                {
                    throw;
                }
            }
        }
    }
}