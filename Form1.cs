using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppNotas
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CadastroDeNotas;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt; int ID = 0;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void Calcular_Click(object sender, EventArgs e)
        {
            float nota1 = float.Parse(Nota1.Text);
            float nota2 = float.Parse(Nota2.Text);
            float nota3 = float.Parse(Nota3.Text);
            float nota4 = float.Parse(Nota4.Text);

            maior.Text = MaiorNota(nota1, nota2, nota3, nota4).ToString();
            menor.Text = MenorNota(nota1, nota2, nota3, nota4).ToString();
            md.Text = Media(nota1, nota2, nota3, nota4).ToString("f1");
            somap.Text = SomaNotasPares(nota1, nota2, nota3, nota4).ToString();
            somaImpares.Text = SomaNotasImpares(nota1, nota2, nota3, nota4).ToString();
            maior7.Text = NotaasMaioresOuIgualSete(nota1, nota2, nota3, nota4).ToString();

          //  LimparSemDelete();

        }

        public float MaiorNota(float nota1, float nota2, float nota3, float nota4)
        {
            List<float> lista = new List<float> { nota1, nota2, nota3, nota4 };
            float maiorValor = lista.Max();

            return maiorValor;
        }

        public float MenorNota(float nota1, float nota2, float nota3, float nota4)
        {
            List<float> lista = new List<float> { nota1, nota2, nota3, nota4 };
            float maiorValor = lista.Min();

            return maiorValor;
        }


        public float Media(float nota1, float nota2, float nota3, float nota4)
        {
            float media = (nota1 + nota2 + nota3 + nota4) / 4;
            return media;
        }

        public float SomaNotasPares(float nota1, float nota2, float nota3, float nota4)
        {
            List<float> lista = new List<float> { nota1, nota2, nota3, nota4 };
            float resultado = 0;
            foreach (float x in lista)
            {
                if (x % 2 == 0)
                    resultado += x;
            }

            return resultado;
        }
        public float SomaNotasImpares(float nota1, float nota2, float nota3, float nota4)
        {
            List<float> lista = new List<float> { nota1, nota2, nota3, nota4 };
            float resultado = 0;
            foreach (float x in lista)
            {
                if (x % 2 != 0)
                    resultado += x;
            }

            return resultado;
        }

        public float NotaasMaioresOuIgualSete(float nota1, float nota2, float nota3, float nota4)
        {
            List<float> lista = new List<float> { nota1, nota2, nota3, nota4 };
            float resultado = 0;
            foreach (float x in lista)
            {
                if (x >= 7)
                    resultado += 1;
            }

            return resultado;
        }

        private void Sair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Sair do programa ?", "AppNotas",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
            else
                Nota1.Focus();
        }

        private void Limpar_Click(object sender, EventArgs e)
        {

            Nota1.Text = "";
            Nota2.Text = "";
            Nota3.Text = "";
            Nota4.Text = "";

            maior.Text = "";
            menor.Text = "";
            md.Text ="";
            somap.Text ="";
            somaImpares.Text = "";
            maior7.Text = "";
            Deleta();

        }

        private void SalvarNotas_Click(object sender, EventArgs e)
        {

            if (Nota1.Text != "" && Nota2.Text != "" && Nota3.Text != "" && Nota4.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("INSERT INTO Notas(nota1,nota2,nota3,nota4) VALUES(@nota1,@nota2,@nota3,@nota4)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@nota1", Nota1.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@nota2", Nota2.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@nota3", Nota3.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@nota4", Nota4.Text.ToUpper());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Nota incluída com sucesso...");

                    LimparSemDelete();

                }

                catch (Exception ex)
                {
                    MessageBox.Show("ops não conseguimos calcular");
                }

                finally
                {
                    con.Close();

                }
            }
            else
            {
                MessageBox.Show("Informe todos os dados requeridos");
            }

        }

        private void LerNotas_Click(object sender, EventArgs e)
        {
            try
            {
                string comando = "SELECT * FROM Notas";
                con.Open();
                DataTable dt = new DataTable();
                adapt = new SqlDataAdapter(comando , con);
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }

        }


        private void LimparSemDelete()
        {
            Nota1.Text = "";
            Nota2.Text = "";
            Nota3.Text = "";
            Nota4.Text = "";

           
        }

        private void Deleta()
        {

             try
             {
              cmd = new SqlCommand("DELETE Notas ", con);   
              con.Open();
              cmd.ExecuteNonQuery();
              }
             catch (Exception ex)
             {
                MessageBox.Show("Erro : " + ex.Message);
             }
             finally
             {
              con.Close();
                       
             }
        }


    }

}

