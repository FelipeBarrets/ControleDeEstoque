using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDFelipe
{
    public partial class MDIParent1 : Form
    {
        

        public MDIParent1()
        {
            InitializeComponent();
        }

        

        private void MDIParent1_Load(object sender, EventArgs e)
        {

        }
        //Botão abre o formulario de cadastro de produtos
        private void produtosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            cadastro_produtos cadastrop = new cadastro_produtos();
            cadastrop.ShowDialog();
        }

        private void Estoque_Click(object sender, EventArgs e)
        {

        }

        
        //Botão abre o form referente a lista de produtos cadastrados
        private void saidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            lista_produtos listap = new lista_produtos();
            listap.ShowDialog();

        }
        //Botão para voltar ao menu principal
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
