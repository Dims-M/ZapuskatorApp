using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZapuskatorWindowsFormsApp
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Список всех текущих процессов на момент обновления
        /// </summary>
        private List<Process> processors = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void GetProcesses()
        {
            processors.Clear();
            processors = Process.GetProcesses().ToList<Process>(); // получаем все процессы и приводим к типу лист
        }

        /// <summary>
        /// Взаимодествие с листом
        /// </summary>
        private void RefreshProcesserList()
        {
            listView1.Items.Clear(); // Очищаем страрые данные

            //Размер в мегобайтах
            double memSize = 0;

            foreach (var pr in processors)
            {
                memSize = 0;

                PerformanceCounter pc = new PerformanceCounter();
                pc.CategoryName = "Process";
                pc.CounterName = "Woring Set - Privat";
                pc.InstanceName = pr.ProcessName;

                memSize = (double)pc.NextValue() / (1000 * 1000); //вычитываем точный размер памяти.

                //массив строк для хранения колонок
                string[] row = new string[] { pr.ProcessName.ToString(), Math.Round(memSize, 1).ToString() };//Округляет значение с плавающей запятой двойной точности до заданного числа
                listView1.Items.Add(new ListViewItem(row));
                pc.Close();
                pc.Dispose();
            }
            Text = $"Запущенно процессов {processors.Count.ToString()}";
        }

       /// <summary>
       /// Поиск нужного процесса в списке текущих запущеных процессов
       /// </summary>
       /// <param name="processes">Лист со списком текущих процессов</param>
       /// <param name="keyword">Ключевое слово процесса</param>
        private void RefreshProcesserList(List<Process> processes, string keyword)
        {
            listView1.Items.Clear(); // Очищаем страрые данные
            //Размер в мегобайтах
            double memSize = 0;

            foreach (var pr in processors)
            {
                memSize = 0;
                PerformanceCounter pc = new PerformanceCounter();
                pc.CategoryName = "Process";
                pc.CounterName = "Woring Set - Privat";
                pc.InstanceName = pr.ProcessName;

                memSize = (double)pc.NextValue() / (1000 * 1000); //вычитываем точный размер памяти.

                //массив строк для хранения колонок
                string[] row = new string[] { pr.ProcessName.ToString(), Math.Round(memSize, 1).ToString() };//Округляет значение с плавающей запятой двойной точности до заданного числа
                listView1.Items.Add(new ListViewItem(row));
                pc.Close();
                pc.Dispose();
            }
            Text = $"Запущенно процессов '{keyword}': {processors.Count.ToString()}";
        }

        /// <summary>
        /// Зарытие нужного процесса
        /// </summary>
        /// <param name="process">имя процесса готорый нужно закрыть</param>
        private void KillProssec(Process process)
        {
            process.Kill();
            process.WaitForExit(); // Ждем выполнения всех связанных процессов
        }

        private void KillProssecAndChildren(int pid)
        {
            try
            {
            if (pid==0)
            {
                return; // вых. из метода
            }
            //получаем список процессов по id(pid)
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process ParentProcessID"+ pid);
            ManagementObjectCollection objectCollection = searcher.Get(); // Выполнить запрос. Получить результат.
               
                foreach (ManagementObject obj in objectCollection)
                {
                    KillProssecAndChildren(Convert.ToInt32(obj["ProcessID"]));
                }

                Process p = Process.GetProcessById(pid);
                p.Kill();
                p.WaitForExit(); // ожидаем завершение всех процессов.
            }

            catch (Exception ex)
            {

            }
        }
    }
}
