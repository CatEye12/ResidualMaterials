namespace ResidualMaterials
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtWidthDim = new System.Windows.Forms.TextBox();
            this.lblWidthDimWP = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtH = new System.Windows.Forms.TextBox();
            this.txtLengthWP = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.lblWidthDim = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblH = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.txtWidthWP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblWidthWP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.CreateButton = new System.Windows.Forms.Button();
            this.cutOutBtn = new System.Windows.Forms.Button();
            this.CancelDeletingButton = new System.Windows.Forms.Button();
            this.DeleteResidualButton = new System.Windows.Forms.Button();
            this.EditMaterialButton = new System.Windows.Forms.Button();
            this.checkWPFormBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtWidthDim);
            this.groupBox1.Controls.Add(this.lblWidthDimWP);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtH);
            this.groupBox1.Controls.Add(this.txtLengthWP);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Controls.Add(this.lblWidthDim);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.lblH);
            this.groupBox1.Controls.Add(this.lblLength);
            this.groupBox1.Controls.Add(this.txtWidthWP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.lblWidthWP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 215);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Общие сведения";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(257, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(144, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Параметры заготовки:";
            // 
            // txtWidthDim
            // 
            this.txtWidthDim.Location = new System.Drawing.Point(141, 117);
            this.txtWidthDim.Name = "txtWidthDim";
            this.txtWidthDim.Size = new System.Drawing.Size(100, 20);
            this.txtWidthDim.TabIndex = 21;
            this.txtWidthDim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWidthDim_KeyPress);
            // 
            // lblWidthDimWP
            // 
            this.lblWidthDimWP.AutoSize = true;
            this.lblWidthDimWP.Location = new System.Drawing.Point(262, 90);
            this.lblWidthDimWP.Name = "lblWidthDimWP";
            this.lblWidthDimWP.Size = new System.Drawing.Size(95, 13);
            this.lblWidthDimWP.TabIndex = 24;
            this.lblWidthDimWP.Text = "Длина заготовки";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(141, 83);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 19;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtH_KeyPress);
            // 
            // txtH
            // 
            this.txtH.Location = new System.Drawing.Point(141, 181);
            this.txtH.Name = "txtH";
            this.txtH.Size = new System.Drawing.Size(100, 20);
            this.txtH.TabIndex = 19;
            this.txtH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtH_KeyPress);
            // 
            // txtLengthWP
            // 
            this.txtLengthWP.Location = new System.Drawing.Point(369, 83);
            this.txtLengthWP.Name = "txtLengthWP";
            this.txtLengthWP.Size = new System.Drawing.Size(100, 20);
            this.txtLengthWP.TabIndex = 18;
            this.txtLengthWP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(141, 151);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 20);
            this.txtLength.TabIndex = 20;
            this.txtLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLength_KeyPress);
            // 
            // lblWidthDim
            // 
            this.lblWidthDim.AutoSize = true;
            this.lblWidthDim.Location = new System.Drawing.Point(6, 124);
            this.lblWidthDim.Name = "lblWidthDim";
            this.lblWidthDim.Size = new System.Drawing.Size(121, 13);
            this.lblWidthDim.TabIndex = 13;
            this.lblWidthDim.Text = "Ширина листа/полосы";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 90);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(18, 13);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "№";
            // 
            // lblH
            // 
            this.lblH.AutoSize = true;
            this.lblH.Location = new System.Drawing.Point(6, 188);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(128, 13);
            this.lblH.TabIndex = 12;
            this.lblH.Text = "Толщина листа/полосы";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(6, 158);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(115, 13);
            this.lblLength.TabIndex = 14;
            this.lblLength.Text = "Длина листа/полосы";
            // 
            // txtWidthWP
            // 
            this.txtWidthWP.Location = new System.Drawing.Point(369, 117);
            this.txtWidthWP.Name = "txtWidthWP";
            this.txtWidthWP.Size = new System.Drawing.Size(100, 20);
            this.txtWidthWP.TabIndex = 17;
            this.txtWidthWP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWidthWP_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Параметры остатка:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Тело вращения",
            "Плоское"});
            this.comboBox1.Location = new System.Drawing.Point(83, 26);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(158, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblWidthWP
            // 
            this.lblWidthWP.AutoSize = true;
            this.lblWidthWP.Location = new System.Drawing.Point(262, 120);
            this.lblWidthWP.Name = "lblWidthWP";
            this.lblWidthWP.Size = new System.Drawing.Size(101, 13);
            this.lblWidthWP.TabIndex = 15;
            this.lblWidthWP.Text = "Ширина заготовки";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Вид изделия";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView);
            this.groupBox2.Location = new System.Drawing.Point(12, 244);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 361);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Остатки";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 16);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(584, 342);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(615, 244);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(590, 361);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "История по остатку";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 16);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(584, 342);
            this.dataGridView2.TabIndex = 0;
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(520, 29);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(131, 22);
            this.CreateButton.TabIndex = 5;
            this.CreateButton.Text = "Добавить остаток";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.Create_Click);
            // 
            // cutOutBtn
            // 
            this.cutOutBtn.Location = new System.Drawing.Point(695, 29);
            this.cutOutBtn.Name = "cutOutBtn";
            this.cutOutBtn.Size = new System.Drawing.Size(131, 22);
            this.cutOutBtn.TabIndex = 6;
            this.cutOutBtn.Text = "Вырезать заготовку";
            this.cutOutBtn.UseVisualStyleBackColor = true;
            this.cutOutBtn.Click += new System.EventHandler(this.CutOut_Click);
            // 
            // CancelDeletingButton
            // 
            this.CancelDeletingButton.Location = new System.Drawing.Point(695, 107);
            this.CancelDeletingButton.Name = "CancelDeletingButton";
            this.CancelDeletingButton.Size = new System.Drawing.Size(131, 41);
            this.CancelDeletingButton.TabIndex = 8;
            this.CancelDeletingButton.Text = "Отменить последнее вырезание";
            this.CancelDeletingButton.UseVisualStyleBackColor = true;
            this.CancelDeletingButton.Click += new System.EventHandler(this.CancelDeletingButton_Click);
            // 
            // DeleteResidualButton
            // 
            this.DeleteResidualButton.Location = new System.Drawing.Point(520, 68);
            this.DeleteResidualButton.Name = "DeleteResidualButton";
            this.DeleteResidualButton.Size = new System.Drawing.Size(131, 22);
            this.DeleteResidualButton.TabIndex = 9;
            this.DeleteResidualButton.Text = "Удалить остаток";
            this.DeleteResidualButton.UseVisualStyleBackColor = true;
            this.DeleteResidualButton.Click += new System.EventHandler(this.DeleteResidualButton_Click);
            // 
            // EditMaterialButton
            // 
            this.EditMaterialButton.Location = new System.Drawing.Point(520, 108);
            this.EditMaterialButton.Name = "EditMaterialButton";
            this.EditMaterialButton.Size = new System.Drawing.Size(131, 41);
            this.EditMaterialButton.TabIndex = 10;
            this.EditMaterialButton.Text = "Изменить параметры остатка";
            this.EditMaterialButton.UseVisualStyleBackColor = true;
            this.EditMaterialButton.Click += new System.EventHandler(this.EditMaterialButton_Click);
            // 
            // checkWPFormBtn
            // 
            this.checkWPFormBtn.Location = new System.Drawing.Point(696, 68);
            this.checkWPFormBtn.Name = "checkWPFormBtn";
            this.checkWPFormBtn.Size = new System.Drawing.Size(130, 22);
            this.checkWPFormBtn.TabIndex = 11;
            this.checkWPFormBtn.UseVisualStyleBackColor = true;
            this.checkWPFormBtn.Click += new System.EventHandler(this.checkWPFormBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 633);
            this.Controls.Add(this.checkWPFormBtn);
            this.Controls.Add(this.EditMaterialButton);
            this.Controls.Add(this.DeleteResidualButton);
            this.Controls.Add(this.CancelDeletingButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cutOutBtn);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWidthDim;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.TextBox txtH;
        private System.Windows.Forms.TextBox txtLengthWP;
        private System.Windows.Forms.TextBox txtWidthWP;
        private System.Windows.Forms.Label lblWidthWP;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblWidthDim;
        private System.Windows.Forms.Label lblH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button cutOutBtn;
        private System.Windows.Forms.Label lblWidthDimWP;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button CancelDeletingButton;
        private System.Windows.Forms.Button DeleteResidualButton;
        private System.Windows.Forms.Button EditMaterialButton;
        private System.Windows.Forms.Button checkWPFormBtn;
    }
}

