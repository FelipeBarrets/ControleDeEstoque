using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRUDFelipe
{
    public partial class cadastro_produtos : Form
    {
        public cadastro_produtos()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            MDIParent1 inicio = new MDIParent1();
            inicio.Show();
        }

        private void cadastro_produtos_Load(object sender, EventArgs e)
        {

        }
        //Botão para adicionar valores em um produto
        private void button1_Click(object sender, EventArgs e)
        {
            inserir();
            txtnomep.Text = "";
            txtpreco.Text = "";            
            quantia.Value = 0;
            mini.Value = 0;
        }
        //Função que adiciona
        public void inserir()
        {

            try
            {
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=distribuidora");
                objcon.Open();
                MySqlCommand objcomando = new MySqlCommand("insert into produto (nomeproduto, preco, quantidade, minimo) values (?,?,?,?)", objcon);
                objcomando.Parameters.Add("@nomeproduto", MySqlDbType.VarChar, 30).Value = txtnomep.Text;                
                objcomando.Parameters.Add("@preco", MySqlDbType.Double).Value = txtpreco.Text;
                objcomando.Parameters.Add("@quantia", MySqlDbType.Int32).Value = quantia.Value;
                objcomando.Parameters.Add("@mini", MySqlDbType.Int32).Value = mini.Value;

                objcomando.ExecuteNonQuery();
                objcon.Close();
            }
            catch
            {
                MessageBox.Show("Erro ao inserir produto");
            }


        }

        
    }
}
