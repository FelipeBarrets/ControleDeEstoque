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
    public partial class lista_produtos : Form
    {
        public lista_produtos()
        {
            InitializeComponent();
        }

        private void lista_produtos_Load(object sender, EventArgs e)
        {
            //Carrega a lista de produtos cadastrados
            listagrid();
            

        }


        //Função que carrega a lista de produtos cadastrados
        public void listagrid()
        {
            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=distribuidora");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("Select idprodutos, nomeproduto, preco, quantidade, minimo from produto", objcon);


            MySqlDataAdapter lista = new MySqlDataAdapter(objcomando);
            DataTable dtlista = new DataTable();
            lista.Fill(dtlista);
            dgdadosprodutos.DataSource = dtlista;




            objcon.Close();

            /* foreach (DataGridViewRow row in dgdadosprodutos.Rows)
             {
                 string cor;
                 cor = Convert.ToString(dgdadosprodutos.Rows[1]);
                 if (cor != "0" )
                 {
                     row.DefaultCellStyle.BackColor = Color.Green;
                 }
                 else
                 {
                     row.DefaultCellStyle.BackColor = Color.Red;
                 }
             }
            */
            mensagemdeaviso();
        }

        //Função que retira o valor selecionado do banco
        public void retirarproduto() {
            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=distribuidora");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("select quantidade from produto where idprodutos= ?", objcon);
            objcomando.Parameters.Add("@idprodutos", MySqlDbType.Int32).Value = ID.Text;
            objcomando.ExecuteNonQuery();
            objcon.Close();

            DataTable dataTable = new DataTable();
            MySqlDataAdapter log = new MySqlDataAdapter(objcomando);
            log.Fill(dataTable);

            foreach (DataRow list in dataTable.Rows)
            {
                int atual, subtrair, id;
                int total;
                atual = Convert.ToInt32(list.ItemArray[0]);
                subtrair = Convert.ToInt32(quantia.Text);
                id = Convert.ToInt32(ID.Text);
                if (subtrair != 0)
                {

                    total = atual - subtrair;
                    objcon.Open();
                    MySqlCommand objcomando2 = new MySqlCommand("UPDATE produto SET quantidade='" + total + "' WHERE idprodutos='" + id + "'", objcon);
                    objcomando2.ExecuteNonQuery();
                    objcon.Close();
                    MessageBox.Show("Foram retirados " + subtrair + " produtos");
                    listagrid();
                    venda();
                }
            }




            listagrid();
            ID.Text = "";
            quantia.Text = "";
        }
        public void venda() {
            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=distribuidora");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("select vendidos from produto where idprodutos= ?", objcon);
            objcomando.Parameters.Add("@idprodutos", MySqlDbType.Int32).Value = ID.Text;
            objcomando.ExecuteNonQuery();
            objcon.Close();

            DataTable dataTable = new DataTable();
            MySqlDataAdapter log = new MySqlDataAdapter(objcomando);
            log.Fill(dataTable);

            foreach (DataRow list in dataTable.Rows)
            {
                int atual, adicionar, id;
                int total;
                atual = Convert.ToInt32(list.ItemArray[0]);
                adicionar = Convert.ToInt32(quantia.Text);
                id = Convert.ToInt32(ID.Text);
                if (adicionar != 0)
                {

                    total = atual + adicionar;
                    objcon.Open();
                    MySqlCommand objcomando2 = new MySqlCommand("UPDATE produto SET vendidos='" + total + "' WHERE idprodutos='" + id + "'", objcon);
                    objcomando2.ExecuteNonQuery();
                    objcon.Close();
                    listagrid();
                }
            }




            listagrid();
            ID.Text = "";
            quantia.Text = "";
        }
        //Função que informa quando o valor atingiu o limite minimo no banco
        public void mensagemdeaviso()
        {


            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=distribuidora");

            MySqlCommand objcomando = new MySqlCommand("select max(idprodutos) from produto", objcon);
            objcon.Open();

            MySqlDataAdapter log = new MySqlDataAdapter(objcomando);
            DataTable dataTable = new DataTable();
            log.Fill(dataTable);
            objcon.Close();
            foreach (DataRow list in dataTable.Rows)
            {
                int max = Convert.ToInt32(list.ItemArray[0]);

                for (int x = 0; x != max;x++)
                {
                    objcon.Open();
                    MySqlCommand objcomando2 = new MySqlCommand("select quantidade, minimo from produto where idprodutos='" + x + "'", objcon);                                       
                    objcomando2.CommandType = CommandType.Text;
                    MySqlDataReader dr;
                    dr = objcomando2.ExecuteReader();
                    dr.Read();
                    textBox1.Text = dr.GetString(0);
                    objcon.Close();
                    /*string m, q;
                    m = dr.GetString(0);
                    q = dr.GetString(1);
                    MessageBox.Show("exibir "+q+" ");*/
                }
            }

                    /*foreach (DataRow list in dataTable.Rows)
                    {
                        int max = Convert.ToInt32(list.ItemArray[0]);

                        for (int x = 0; x != max; )
                        {

                            MySqlCommand objcomando2 = new MySqlCommand("select quantidade from produto where idprodutos='" + x + "'", objcon);
                            objcomando2.ExecuteNonQuery();
                            MySqlDataAdapter quant = new MySqlDataAdapter(objcomando2);
                            DataTable dataquant = new DataTable();
                            log.Fill(dataquant);


                            foreach (DataRow listaquanti in dataquant.Rows)
                            {

                                MySqlCommand objcomando3 = new MySqlCommand("select minimo from produto where idprodutos='" + x + "'", objcon);
                                objcomando3.ExecuteNonQuery();
                                MySqlDataAdapter minimin = new MySqlDataAdapter(objcomando3);
                                DataTable minimun = new DataTable();
                                log.Fill(minimun);



                                foreach (DataRow listami in minimun.Rows)
                                {

                                    if (Convert.ToInt32(listaquanti.ItemArray[0]) == Convert.ToInt32(listami.ItemArray[0]))
                                    {
                                        MessageBox.Show("O produto do id "+x+" atingiu a quantidade minima");
                                        break; 
                                    }

                                }
                            }
                            x++;

                        }
                    }

                    objcon.Close();*/
                }
        

            private void button2_Click(object sender, EventArgs e)
            {
                this.Close();
                MDIParent1 inicio = new MDIParent1();
                inicio.Show();
            }
            //Botão que deleta o id indicado
            private void button1_Click(object sender, EventArgs e)
            {
            if (DialogResult.Yes == MessageBox.Show("Certeza que deseja deletar o id selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                try
                {
                    MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=distribuidora");
                    objcon.Open();
                    MySqlCommand objcomando = new MySqlCommand("delete from produto where idprodutos= ?", objcon);
                    objcomando.Parameters.Add("@idprodutos", MySqlDbType.Int32).Value = ID.Text;
                    objcomando.ExecuteNonQuery();
                    listagrid();
                    MessageBox.Show("Produto deletado");
                    ID.Text = "";
                }
                catch
                {
                    MessageBox.Show("Id não existe");

                }
            }
            }
            //Botão para adicionar uma quantia de produtos no id indicado
            private void adicionar_Click(object sender, EventArgs e)
            {
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=distribuidora");
                objcon.Open();
                MySqlCommand objcomando = new MySqlCommand("select quantidade from produto where idprodutos= ?", objcon);
                objcomando.Parameters.Add("@idprodutos", MySqlDbType.Int32).Value = ID.Text;
                objcomando.ExecuteNonQuery();
                objcon.Close();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter log = new MySqlDataAdapter(objcomando);
                log.Fill(dataTable);

                foreach (DataRow list in dataTable.Rows)
                {
                    int atual, adicao, id;
                    int total;
                    atual = Convert.ToInt32(list.ItemArray[0]);
                    adicao = Convert.ToInt32(quantia.Text);
                    id = Convert.ToInt32(ID.Text);
                    if (adicao != 0)
                    {

                        total = atual + adicao;

                        objcon.Open();
                        MySqlCommand objcomando2 = new MySqlCommand("UPDATE produto SET quantidade='" + total + "' WHERE idprodutos='" + id + "'", objcon);
                        objcomando2.ExecuteNonQuery();
                        objcon.Close();
                        listagrid();
                    }
                }





                listagrid();
                ID.Text = "";
                quantia.Text = "";
            }


            //Botão para a retirada de produtos
            private void button4_Click(object sender, EventArgs e)
            {
                retirarproduto();
            }

            private void quantia_ValueChanged(object sender, EventArgs e)
            {

            }

            private void label3_Click(object sender, EventArgs e)
            {

            }

            private void label2_Click(object sender, EventArgs e)
            {

            }

            private void ID_ValueChanged(object sender, EventArgs e)
            {

            }
        }
    }

