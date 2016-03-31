using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kalman.Utilities;
using Kalman.Security;
using System.IO;
using System.Net;

namespace Kalman.Studio
{
    public partial class StringConvertor : DockableForm
    {
        public StringConvertor()
        {
            InitializeComponent();
        }

        #region 共享成员

        /// <summary>
        /// 原始字符串
        /// </summary>
        public string S1 { get { return rtb1.Text.Trim(); } }
        /// <summary>
        /// 转换后的字符串
        /// </summary>
        public string S2 { get { return rtb2.Text.Trim(); } set { rtb2.Text = value; } }

        /// <summary>
        /// 获取指定原始字符串的编码
        /// </summary>
        /// <returns></returns>
        public Encoding GetEncoding()
        {
            if (rbtnDefaultEncoding.Checked) return Encoding.Default;
            if (rbtnUnicodeEncoding.Checked) return Encoding.Unicode;
            if (rbtnUTF8Encoding.Checked) return Encoding.UTF8;
            if (rbtnGB2312Encoding.Checked) return Encoding.GetEncoding("gb2312");
            if (rbtnOtherEncoding.Checked)
            {
                try
                {
                    return Encoding.GetEncoding(txtEncoding.Text.Trim());
                }
                catch
                {
                    return Encoding.Default;
                }
            }
            return Encoding.Default;
        }

        #endregion

        private void btnToHash_Click(object sender, EventArgs e)
        {
            String_Hash convert = new String_Hash();
            convert.ShowDialog(this);
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            String_Convert convert = new String_Convert();
            convert.ShowDialog(this);
            //MessageBox.Show(convert.Visible.ToString());
        }

        private void btnToUpper_Click(object sender, EventArgs e)
        {
            this.S2 = this.S2.ToUpper();
        }

        private void btnToLower_Click(object sender, EventArgs e)
        {
            this.S2 = this.S2.ToLower();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            String_Replace replace = new String_Replace();
            replace.ShowDialog(this);
        }

        private void rtb1_TextChanged(object sender, EventArgs e)
        {
            gb1.Text = string.Format("内容[字数：{0}]",S1.Length);
        }

        private void rtb2_TextChanged(object sender, EventArgs e)
        {
            gb2.Text = string.Format("内容[字数：{0}]", S2.Length);
        }

        private void btnSecure_Click(object sender, EventArgs e)
        {
            String_SymmetricAlgorithm srcure = new String_SymmetricAlgorithm();
            srcure.ShowDialog(this);
        }

        private void btnRandomString_Click(object sender, EventArgs e)
        {
            String_Random random = new String_Random();
            random.ShowDialog(this);
        }

        private void btncopy_Click(object sender, EventArgs e)
        {
            if (rtb2.SelectedText != "")
                Clipboard.SetDataObject(rtb2.SelectedText); 
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            string codeStr = "stockid={0}&list=1";
            if (rtb1.Text.Trim() != null)
            {
                str = request(url, string.Format(codeStr, rtb1.Text.Trim()));
            }
            else
            {
                str = request(url, param);
            }
            rtb2.Text = str;
        }

        string url = "http://apis.baidu.com/apistore/stockservice/stock";
        string param = "stockid=sh600642,sh600747,sh600527,sz002716&list=1";
        // string result = request(url, param);

        /// <summary>
        /// 发送HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="param">请求的参数</param>
        /// <returns>请求结果</returns>
        public static string request(string url, string param)
        {
            string strURL = url + '?' + param;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", "b131f20b39ce75a23534e18bc026babd");
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }

        private void btnpastGP_Click(object sender, EventArgs e)
        {
           
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                rtb1.Text = (String)iData.GetData(DataFormats.Text);
            }
            else
            {
                this.rtb1.Text = "sh600642,sh600747,sh600527,sz002716";
            }
        }
    }
}
