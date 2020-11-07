using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using KReport.Engine;
using KReport.Controls;

namespace Teste
{
    public partial class Form1 : Form
    {
        KReport.Engine.Report report;

        public Form1()
        {
            //DAL.DataProvider provider = DAL.DataProvider.InstanceProvider();
            
            //provider.Login("sa", "");
            
            //provider.Open();
            //DataTable table = provider.ExecuteQuery("Select CrdCred0.Unidade,  CrdCred0.Credito, CrdCred0.DtLiberacao, CrdCred0.DtSolicitacao, CadProd0.Produto, CadFund0.Fundo, CrdClie0.VlAutorizado from crdcred0 inner join cadprod0 on " +
            //        "cadprod0.codigo = crdcred0.produto and cadprod0.unidade = crdcred0.unidade inner join "+
            //        "cadfund0 on CadFund0.Codigo = CrdCred0.Fundo inner join crdclie0 on crdclie0.unidade = crdcred0.unidade and crdclie0.credito = crdcred0.credito where crdcred0.unidade = 138 and crdcred0.fundo in (1,2,3) and crdcred0.status in (3,4) order by CadProd0.Produto, CadFund0.Fundo").Tables[0];
            //provider.Close();
            //table.TableName = "DataSourceTeste";
            

            DataTable table2 = new DataTable("DataSourceTeste2");
            table2.Columns.Add(new DataColumn("Codigo", typeof(int)));
            table2.Columns.Add(new DataColumn("Descricao", typeof(string)));
            table2.Columns.Add(new DataColumn("Valor", typeof(double)));
            table2.Columns.Add(new DataColumn("Data", typeof(DateTime)));
            table2.Columns.Add(new DataColumn("Fundo", typeof(string)));
            table2.Columns.Add(new DataColumn("Unidade", typeof(string)));

            for (int i = 0; i <= 100; i++)
            {
                DataRow row = table2.Rows.Add();
                row[0] = i;
                row[1] = "Isto ?apenas um teste" + i.ToString();
                row[2] = i + 1000.00 + 200.00;
                row[3] = DateTime.Now;
                if (i > 100)
                {
                    row[4] = "BNDES";
                    row[5] = "TERESINA";
                }
                else 
                {
                    row[4] = "CEAPE";
                    row[5] = "SÃO LUIZ";
                }
            }


            report = new KReport.Engine.Report();
            report.FileName = "D:\\Study\\Report\\AplicationSource\\Bin\\testreport";
            report.Load();
            report.AddSource(table2, "DataSourceTeste2");

            ArrayList temp = new ArrayList();
            for (int i=0; i < 10; i++) { 
                temp.Add(new Pessoa("Fernando" + i.ToString(), i));
            }
            ((Pessoa)temp[0]).BarCode = "7896472502172";
            ((Pessoa)temp[1]).BarCode = "7896004805368";
            ((Pessoa)temp[2]).BarCode = "7896472502172";
            ((Pessoa)temp[3]).BarCode = "7896004805368";
            ((Pessoa)temp[4]).BarCode = "7896472502172";
            ((Pessoa)temp[5]).BarCode = "7896472502172";
            ((Pessoa)temp[6]).BarCode = "7896472502172";
            ((Pessoa)temp[7]).BarCode = "7896472502172";
            ((Pessoa)temp[8]).BarCode = "7896472502172";
            ((Pessoa)temp[9]).BarCode = "7896472502172";
             Bitmap imagem = (Bitmap)Bitmap.FromFile(@"D:\\01.bmp");
             ((Pessoa)temp[0]).Foto = imagem;
            ((Pessoa)temp[1]).Foto = imagem;
            ((Pessoa)temp[2]).Foto = imagem;
            ((Pessoa)temp[3]).Foto = imagem;
            ((Pessoa)temp[4]).Foto = imagem;
            ((Pessoa)temp[5]).Foto = imagem;
            ((Pessoa)temp[6]).Foto = imagem;
            ((Pessoa)temp[7]).Foto = imagem;
            ((Pessoa)temp[8]).Foto = imagem;
            ((Pessoa)temp[9]).Foto = imagem;

            report.AddSource(temp, "ListaPessoa");

            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            report.ShowDesigner(true );
        }

        private void button2_Click(object sender, EventArgs e)
        {
           report.Show();
        }

        public class Pessoa {
            private int codigo;
            private string nome;
            private string barcode;
            private Bitmap foto;

            public Pessoa(string nome, int codigo) {
                this.nome = nome;
                this.codigo = codigo;
            }
            public int Codigo { get { return codigo; } set { codigo = value; } }
            public string Nome { get { return nome; } set { nome = value; } }
            public string BarCode { get { return barcode; } set { barcode = value; } }
            public Bitmap Foto { get { return foto; } set { foto = value; } }
        }
        
    }
}