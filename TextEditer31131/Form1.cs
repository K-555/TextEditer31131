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
        private bool HozonFlg;

        public Form1()
        {
            InitializeComponent();
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
                    Text = Path.GetFileName(ofdFileOpen.FileName);
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
                Text = Path.GetFileName(fileName);
            }
        }

        //新規作成
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveConfirmation(sender, e);
            rtTextArea.Text = "";
            Text = "無題";

        }

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           rtTextArea.Undo();
        }

        //やり直し
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
        }

        //切り取り
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Cut();
        }

        //コピー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();
        }

        //貼り付け
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Paste();
        }

        //削除
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.SelectedText = "";
        }

        //編集
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMaskCheck();
        }

        //色
        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cdCollar.ShowDialog() == DialogResult.OK)
            {
                //色を選択する
                rtTextArea.SelectionColor = cdCollar.Color;
            }
        }

        //フォント
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fdFont.ShowDialog() == DialogResult.OK)
            {
                //フォントを選択する
                rtTextArea.SelectionFont = fdFont.Font;
            }
        }

        private void rtTextArea_TextChanged(object sender, EventArgs e)
        {
            HozonFlg = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HozonFlg = false; //まずフラグを準備する
        }

        private void SaveConfirmation(object sender, EventArgs e)
        {
            {
                if (HozonFlg == true)
                {
                    DialogResult result = MessageBox.Show("テキストが保存されていません。保存しますか？",
                                                           "テキストを保存しますか？",
                                                           MessageBoxButtons.YesNoCancel,
                                                           MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        // 保存処理
                        SaveNameToolStripMenuItem_Click(sender, e);
                    }
                    else if (result == DialogResult.No)
                    {

                    }
                    else
                    {
                        // 保存キャンセル処理
                        
                    }
                }
            }
        }

        private void fcSave(object sender, FormClosingEventArgs e)
        {
            SaveConfirmation(sender, e);
        }

        //編集メニュー項目内のマスク処理
        private void EditMaskCheck()
        {
            DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Rtf);
            UndoToolStripMenuItem.Enabled = rtTextArea.CanUndo ? true : false;
            RedoToolStripMenuItem.Enabled = rtTextArea.CanRedo ? true : false;
            CutToolStripMenuItem.Enabled = rtTextArea.SelectionLength > 0 ? true : false;
            CopyToolStripMenuItem.Enabled = rtTextArea.SelectionLength > 0 ? true : false;
            DeleteToolStripMenuItem.Enabled = rtTextArea.SelectionLength > 0 ? true : false;
            PasteToolStripMenuItem.Enabled = rtTextArea.CanPaste(myFormat) ? true : false;
        }
    }
}
