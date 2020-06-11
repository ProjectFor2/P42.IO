using P42.IO.Helper;
using P42.IO.Properties;
using System;
using System.Activities;
using System.Diagnostics;

namespace P42.IO.Directory
{
    [LocalizedDisplayName(nameof(Resources.DeleteFolderDisplayName))]
    [LocalizedDescription(nameof(Resources.DeleteFolderDescription))]
    public class DeleteFolder : CodeActivity
    {
#if DEBUG
        public DeleteFolder() => new DesignerMetadata().Register();
#endif
        [LocalizedCategory(nameof(Resources.CategoryCommon))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnErrorDisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnErrorDescription))]
        public InArgument<bool> ContinueOnError { get; set; }

        [RequiredArgument]
        [LocalizedCategory(nameof(Resources.CategoryDirectory))]
        [LocalizedDisplayName(nameof(Resources.DeleteFolderPathDisplayName))]
        [LocalizedDescription(nameof(Resources.DeleteFolderPathDescription))]
        public InArgument<string> Path { get; set; }

        [RequiredArgument]
        [LocalizedCategory(nameof(Resources.CategoryDirectory))]
        [LocalizedDisplayName(nameof(Resources.DeleteFolderRecursiveDisplayName))]
        [LocalizedDescription(nameof(Resources.DeleteFolderRecursiveDescription))]
        public InArgument<bool> Recursive { get; set; }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (Path == null) metadata.AddValidationError(string.Format(Resources.ValidationValueError, nameof(Path)));

            base.CacheMetadata(metadata);
        }

        protected override void Execute(CodeActivityContext context)
        {
            var continueOnError = context.GetValue(ContinueOnError);
            var path = context.GetValue(Path);
            var recursive = context.GetValue(Recursive);

            try
            {
                DirectoryHelper.DeleteDirectory(path, recursive);
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
