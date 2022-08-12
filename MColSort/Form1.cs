using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MColSort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 在原表最后构造一列不可见的辅助列，辅助列=第2列&第3列 or 第3列&第2列，主排序列在前，次排序列在后，
        /// 再根据辅助列排序，即可实现点击列主排序另一列次排序
        /// 这样能实现固定两列排序，根据点击确定主次进行排序，后续可根据此再添加更多列
        /// 由于是根据不可见的辅助列排序，表头无升序降序的三角符号，需要手工添加
        /// </summary>
        bool sort1 = false;//sort1是记录第2列升序或降序的标志
        bool sort2 = false;//sort2是记录第3列升序或降序的标志
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //每当点击表头时，将表头内的上三角和下三角符号清空
            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {
                dataGridView1.Columns[j].HeaderText = dataGridView1.Columns[j].HeaderText.Replace("🔺", "").Replace("🔻", "");
            }
            //px是记录点击了第几列的表头
            int px = this.dataGridView1.SortedColumn.Index;
            if (px ==1)//点击第2列
            {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    dataGridView1.Rows[j].Cells[3].Value = Convert.ToInt32(dataGridView1.Rows[j].Cells[1].Value).ToString("d3") + Convert.ToInt32(dataGridView1.Rows[j].Cells[2].Value).ToString("d3");
                    //构造辅助列，第2列为主排序在前，第3列在后，注意行数列数需要在前补足0，否组影响排序大小判断
                }
                if (sort1==false)
                {
                    dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Ascending);//根据辅助列升序排序
                    dataGridView1.Columns[px].HeaderText += "🔺";//根据标志位添加升序符号
                    sort1 = true;//标志位取反
                }
                else
                {
                    dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Descending);//根据辅助列降序排序
                    dataGridView1.Columns[px].HeaderText += "🔻";//根据标志位添加降序符号
                    sort1 = false;//标志位取反
                }
            }
            if (px == 2)//点击第3列，基本原理同上，不再注释
            {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    dataGridView1.Rows[j].Cells[3].Value = Convert.ToInt32(dataGridView1.Rows[j].Cells[2].Value).ToString("D3") + Convert.ToInt32(dataGridView1.Rows[j].Cells[1].Value).ToString("D3");
                }
                if (sort2 == false)
                {
                    dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Ascending);
                    dataGridView1.Columns[px].HeaderText += "🔺";
                    sort2 = true;
                }
                else
                {
                    dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Descending);
                    dataGridView1.Columns[px].HeaderText += "🔻";
                    sort2 = false;
                }
            }
        }
    }
}
