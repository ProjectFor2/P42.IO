using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using P42.IO.Directory;

namespace P42.IO
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            builder.AddCustomAttributes(typeof(CreateFolder), new DesignerAttribute(typeof(CreateFolderDesigner)));
            builder.AddCustomAttributes(typeof(CreateFolder), new HelpKeywordAttribute(""));

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
