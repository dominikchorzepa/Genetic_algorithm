namespace Program
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxLiczbaIteracji = new TextBox();
            label1LiczbaIteracji = new Label();
            label2ZDMin = new Label();
            label3ZDMax = new Label();
            labelLiczbaBitowNaParam = new Label();
            label5LiczbaParam = new Label();
            label6LiczbaOsobnikow = new Label();
            label7RozmiarTurnieju = new Label();
            textBoxZDMin = new TextBox();
            textBoxZDMax = new TextBox();
            textBoxLiczbaOsobnikow = new TextBox();
            textBoxLiczbaParam = new TextBox();
            textBoxLiczbaBitowNaParam = new TextBox();
            textBoxRozmiarTurnieju = new TextBox();
            label8FunkcjaPrzystosow = new Label();
            comboBoxFunkcjaPrzystosow = new ComboBox();
            buttonGenerujAlgorytm = new Button();
            WynikiAlgorytmu = new ListBox();
            SuspendLayout();
            // 
            // textBoxLiczbaIteracji
            // 
            textBoxLiczbaIteracji.Location = new Point(21, 69);
            textBoxLiczbaIteracji.Name = "textBoxLiczbaIteracji";
            textBoxLiczbaIteracji.Size = new Size(103, 27);
            textBoxLiczbaIteracji.TabIndex = 0;
            // 
            // label1LiczbaIteracji
            // 
            label1LiczbaIteracji.AutoSize = true;
            label1LiczbaIteracji.Location = new Point(21, 34);
            label1LiczbaIteracji.Name = "label1LiczbaIteracji";
            label1LiczbaIteracji.Size = new Size(103, 20);
            label1LiczbaIteracji.TabIndex = 1;
            label1LiczbaIteracji.Text = "Liczba iteracji:";
            // 
            // label2ZDMin
            // 
            label2ZDMin.AutoSize = true;
            label2ZDMin.Location = new Point(148, 34);
            label2ZDMin.Name = "label2ZDMin";
            label2ZDMin.Size = new Size(57, 20);
            label2ZDMin.TabIndex = 2;
            label2ZDMin.Text = "ZDMin:";
            // 
            // label3ZDMax
            // 
            label3ZDMax.AutoSize = true;
            label3ZDMax.Location = new Point(234, 34);
            label3ZDMax.Name = "label3ZDMax";
            label3ZDMax.Size = new Size(60, 20);
            label3ZDMax.TabIndex = 3;
            label3ZDMax.Text = "ZDMax:";
            // 
            // labelLiczbaBitowNaParam
            // 
            labelLiczbaBitowNaParam.AutoSize = true;
            labelLiczbaBitowNaParam.Location = new Point(631, 34);
            labelLiczbaBitowNaParam.Name = "labelLiczbaBitowNaParam";
            labelLiczbaBitowNaParam.Size = new Size(181, 20);
            labelLiczbaBitowNaParam.TabIndex = 4;
            labelLiczbaBitowNaParam.Text = "Liczba bitów na parametr:";
            labelLiczbaBitowNaParam.Click += label4_Click;
            // 
            // label5LiczbaParam
            // 
            label5LiczbaParam.AutoSize = true;
            label5LiczbaParam.Location = new Point(473, 34);
            label5LiczbaParam.Name = "label5LiczbaParam";
            label5LiczbaParam.Size = new Size(139, 20);
            label5LiczbaParam.TabIndex = 5;
            label5LiczbaParam.Text = "Liczba parametrów:";
            // 
            // label6LiczbaOsobnikow
            // 
            label6LiczbaOsobnikow.AutoSize = true;
            label6LiczbaOsobnikow.Location = new Point(317, 34);
            label6LiczbaOsobnikow.Name = "label6LiczbaOsobnikow";
            label6LiczbaOsobnikow.Size = new Size(130, 20);
            label6LiczbaOsobnikow.TabIndex = 6;
            label6LiczbaOsobnikow.Text = "Liczba osobników:";
            // 
            // label7RozmiarTurnieju
            // 
            label7RozmiarTurnieju.AutoSize = true;
            label7RozmiarTurnieju.Location = new Point(832, 34);
            label7RozmiarTurnieju.Name = "label7RozmiarTurnieju";
            label7RozmiarTurnieju.Size = new Size(121, 20);
            label7RozmiarTurnieju.TabIndex = 7;
            label7RozmiarTurnieju.Text = "Rozmiar turnieju:";
            // 
            // textBoxZDMin
            // 
            textBoxZDMin.Location = new Point(148, 69);
            textBoxZDMin.Name = "textBoxZDMin";
            textBoxZDMin.Size = new Size(57, 27);
            textBoxZDMin.TabIndex = 8;
            // 
            // textBoxZDMax
            // 
            textBoxZDMax.Location = new Point(234, 69);
            textBoxZDMax.Name = "textBoxZDMax";
            textBoxZDMax.Size = new Size(60, 27);
            textBoxZDMax.TabIndex = 9;
            // 
            // textBoxLiczbaOsobnikow
            // 
            textBoxLiczbaOsobnikow.Location = new Point(317, 69);
            textBoxLiczbaOsobnikow.Name = "textBoxLiczbaOsobnikow";
            textBoxLiczbaOsobnikow.Size = new Size(130, 27);
            textBoxLiczbaOsobnikow.TabIndex = 10;
            // 
            // textBoxLiczbaParam
            // 
            textBoxLiczbaParam.Location = new Point(473, 69);
            textBoxLiczbaParam.Name = "textBoxLiczbaParam";
            textBoxLiczbaParam.Size = new Size(139, 27);
            textBoxLiczbaParam.TabIndex = 11;
            // 
            // textBoxLiczbaBitowNaParam
            // 
            textBoxLiczbaBitowNaParam.Location = new Point(631, 69);
            textBoxLiczbaBitowNaParam.Name = "textBoxLiczbaBitowNaParam";
            textBoxLiczbaBitowNaParam.Size = new Size(181, 27);
            textBoxLiczbaBitowNaParam.TabIndex = 12;
            // 
            // textBoxRozmiarTurnieju
            // 
            textBoxRozmiarTurnieju.Location = new Point(832, 69);
            textBoxRozmiarTurnieju.Name = "textBoxRozmiarTurnieju";
            textBoxRozmiarTurnieju.Size = new Size(121, 27);
            textBoxRozmiarTurnieju.TabIndex = 13;
            // 
            // label8FunkcjaPrzystosow
            // 
            label8FunkcjaPrzystosow.AutoSize = true;
            label8FunkcjaPrzystosow.Location = new Point(21, 119);
            label8FunkcjaPrzystosow.Name = "label8FunkcjaPrzystosow";
            label8FunkcjaPrzystosow.Size = new Size(167, 20);
            label8FunkcjaPrzystosow.TabIndex = 14;
            label8FunkcjaPrzystosow.Text = "Funkcja przystosowania:";
            // 
            // comboBoxFunkcjaPrzystosow
            // 
            comboBoxFunkcjaPrzystosow.FormattingEnabled = true;
            comboBoxFunkcjaPrzystosow.Items.AddRange(new object[] { "Funkcja Zad1", "Funkcja Zad2", "Funkcja Zad3" });
            comboBoxFunkcjaPrzystosow.Location = new Point(210, 116);
            comboBoxFunkcjaPrzystosow.Name = "comboBoxFunkcjaPrzystosow";
            comboBoxFunkcjaPrzystosow.Size = new Size(117, 28);
            comboBoxFunkcjaPrzystosow.TabIndex = 15;
            // 
            // buttonGenerujAlgorytm
            // 
            buttonGenerujAlgorytm.Location = new Point(344, 116);
            buttonGenerujAlgorytm.Name = "buttonGenerujAlgorytm";
            buttonGenerujAlgorytm.Size = new Size(142, 29);
            buttonGenerujAlgorytm.TabIndex = 16;
            buttonGenerujAlgorytm.Text = "Generuj algorytm";
            buttonGenerujAlgorytm.UseVisualStyleBackColor = true;
            buttonGenerujAlgorytm.Click += buttonGenerujAlgorytm_Click;
            // 
            // WynikiAlgorytmu
            // 
            WynikiAlgorytmu.FormattingEnabled = true;
            WynikiAlgorytmu.Location = new Point(21, 169);
            WynikiAlgorytmu.Name = "WynikiAlgorytmu";
            WynikiAlgorytmu.Size = new Size(936, 244);
            WynikiAlgorytmu.TabIndex = 17;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1047, 450);
            Controls.Add(WynikiAlgorytmu);
            Controls.Add(buttonGenerujAlgorytm);
            Controls.Add(comboBoxFunkcjaPrzystosow);
            Controls.Add(label8FunkcjaPrzystosow);
            Controls.Add(textBoxRozmiarTurnieju);
            Controls.Add(textBoxLiczbaBitowNaParam);
            Controls.Add(textBoxLiczbaParam);
            Controls.Add(textBoxLiczbaOsobnikow);
            Controls.Add(textBoxZDMax);
            Controls.Add(textBoxZDMin);
            Controls.Add(label7RozmiarTurnieju);
            Controls.Add(label6LiczbaOsobnikow);
            Controls.Add(label5LiczbaParam);
            Controls.Add(labelLiczbaBitowNaParam);
            Controls.Add(label3ZDMax);
            Controls.Add(label2ZDMin);
            Controls.Add(label1LiczbaIteracji);
            Controls.Add(textBoxLiczbaIteracji);
            Name = "Form1";
            Text = "Algorytm Genetyczny";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxLiczbaIteracji;
        private Label label1LiczbaIteracji;
        private Label label2ZDMin;
        private Label label3ZDMax;
        private Label labelLiczbaBitowNaParam;
        private Label label5LiczbaParam;
        private Label label6LiczbaOsobnikow;
        private Label label7RozmiarTurnieju;
        private TextBox textBoxZDMin;
        private TextBox textBoxZDMax;
        private TextBox textBoxLiczbaOsobnikow;
        private TextBox textBoxLiczbaParam;
        private TextBox textBoxLiczbaBitowNaParam;
        private TextBox textBoxRozmiarTurnieju;
        private Label label8FunkcjaPrzystosow;
        private ComboBox comboBoxFunkcjaPrzystosow;
        private Button buttonGenerujAlgorytm;
        private ListBox WynikiAlgorytmu;
    }
}