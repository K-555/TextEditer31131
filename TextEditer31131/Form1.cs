using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31131
{
    public partial class Form1 : Form
    {
        //現在編集中のファイル名
        private string fileName = "";   //Camel形式（⇔Pascal形式）
        public Form1()
        {
            InitializeComponent();
            Form();
        }
        
        private void Form()
        {
            if (fileName == "")
            {
               
            }
        }

        //終了
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーション終了
            Application.Exit();
        }

        //名前を付けて保存
        private void SaveNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //[名前を付けて保存]ダイアログを表示
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
            }
        }

        //開く
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //[開く]ダイアログを表示
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                //StreamReaderクラスを使用してファイル読込み
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding
                                                         .GetEncoding("utf-8"), false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    //現在開いているファイル名を設定
                    this.fileName = ofdFileOpen.FileName;
                }
            }
        }

        //上書き保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (this.fileName != "")
            {
                //新規ファイルの場合は、名前を付けて保存の処理
                FileSave(fileName);
            }
            else
            {
                try
                {
                    //新規でないときは、ファイルに上書き
                    SaveNameToolStripMenuItem_Click(sender,e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "エラーメッセージ",
                                            MessageBoxButtons.OK, 
                                                MessageBoxIcon.Error);
                }
            }
        }

        //ファイル名を指定してデータを保存
        private void FileSave(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName,
                                                      false, 
                                                      Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(rtTextArea.Text);
            }
        }

    }
}
