using Bb.Galileo.Files.Viewpoints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bb.Galileo.Viewpoints.Cooperations
{
    public partial class SelectReferential : Form
    {
        public SelectReferential()
        {
            InitializeComponent();
        }

        private void SelectReferential_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        public void SetViewpoint(CooperationViewpoint config)
        {

            foreach (var item in config.Concepts)
            {
                var c = new ConceptItem(item, 0, config);
                ConceptsTreeView.Nodes.Add(c);
            }

            foreach (var item in config.Elements)
            {
                var c = new ConceptItem(item, 0, config);
                ConceptsTreeView.Nodes.Add(c);
            }

            var m = config.File.Parent.Models;

            foreach (var item in config.References)
            {
                var r = m.GetRelationshipDefinition(item.Name);
                if (r != null)
                    foreach (ConceptItem concept in this.ConceptsTreeView.Nodes)
                        concept.AddReference(r);
            }

        }

    }

    public class ReferenceItem
    {
        public Files.Schemas.EntityDefinition SourceDefinition { get; internal set; }
        public Files.Schemas.EntityDefinition Target { get; internal set; }
    }

}
