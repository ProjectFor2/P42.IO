using P42.IO.Directory;
using System;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.ComponentModel;
using System.Windows;

namespace P42.IO.App
{
    /// <summary>
    /// Interaction logic for RehostingWFDesigner.xaml
    /// </summary>
    public partial class RehostingWFDesigner : Window
    {
        public RehostingWFDesigner() => InitializeComponent();

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            // Register metadata.
            (new System.Activities.Core.Presentation.DesignerMetadata()).Register();
            RegisterCustomMetadata();
            // Add custom activity to toolbox.
            Toolbox.Categories.Add(new ToolboxCategory("Custom activities"));
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(CreateFolder)));
            Toolbox.Categories[1].Add(new ToolboxItemWrapper(typeof(DeleteFolder)));

            // Create the workflow designer.
            var wd = new WorkflowDesigner();
            wd.Load(new Sequence());
            DesignerBorder.Child = wd.View;
            PropertyBorder.Child = wd.PropertyInspectorView;

        }

        static void RegisterCustomMetadata()
        {
            var builder = new AttributeTableBuilder();
            builder.AddCustomAttributes(typeof(CreateFolder), new DesignerAttribute(typeof(CreateFolderDesigner)));
            builder.AddCustomAttributes(typeof(DeleteFolder), new DesignerAttribute(typeof(DeleteFolderDesigner)));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
