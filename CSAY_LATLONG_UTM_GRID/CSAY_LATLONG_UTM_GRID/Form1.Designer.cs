namespace CSAY_LATLONG_UTM_GRID
{
    partial class FrmMainGrid
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadRWYdataStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllLayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baseLineEqParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridCOORDParallelToBaselineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.CalcverticalGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kMLOfGridlinesParallelToNorthToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kMLCircleCenteredAtARPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baselineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.northToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baselineModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.northModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TxtInterceptBase = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtslopeBase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtNlines = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtGridInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtRadius = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtAirportCode = new System.Windows.Forms.TextBox();
            this.ComboBoxAirportCode = new System.Windows.Forms.ComboBox();
            this.TxtCM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.gridBoundaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblModestatus = new System.Windows.Forms.Label();
            this.lblGBstatus = new System.Windows.Forms.Label();
            this.ColSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLatitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLongitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEasting_X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNorthing_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.calculateToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.autoProcessToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadRWYdataStripMenuItem1,
            this.toolStripMenuItem2,
            this.clearAllLayersToolStripMenuItem,
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // LoadRWYdataStripMenuItem1
            // 
            this.LoadRWYdataStripMenuItem1.Name = "LoadRWYdataStripMenuItem1";
            this.LoadRWYdataStripMenuItem1.Size = new System.Drawing.Size(180, 24);
            this.LoadRWYdataStripMenuItem1.Text = "Load RWY data";
            this.LoadRWYdataStripMenuItem1.Click += new System.EventHandler(this.LoadRWYdataStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // clearAllLayersToolStripMenuItem
            // 
            this.clearAllLayersToolStripMenuItem.Name = "clearAllLayersToolStripMenuItem";
            this.clearAllLayersToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.clearAllLayersToolStripMenuItem.Text = "Clear All Layers";
            this.clearAllLayersToolStripMenuItem.Click += new System.EventHandler(this.clearAllLayersToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // calculateToolStripMenuItem
            // 
            this.calculateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.baseLineEqParameterToolStripMenuItem,
            this.gridCOORDParallelToBaselineToolStripMenuItem,
            this.toolStripMenuItem3,
            this.CalcverticalGridToolStripMenuItem});
            this.calculateToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calculateToolStripMenuItem.Name = "calculateToolStripMenuItem";
            this.calculateToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.calculateToolStripMenuItem.Text = "Calculate";
            // 
            // baseLineEqParameterToolStripMenuItem
            // 
            this.baseLineEqParameterToolStripMenuItem.Name = "baseLineEqParameterToolStripMenuItem";
            this.baseLineEqParameterToolStripMenuItem.Size = new System.Drawing.Size(290, 24);
            this.baseLineEqParameterToolStripMenuItem.Text = "Base line Eq parameter";
            this.baseLineEqParameterToolStripMenuItem.Click += new System.EventHandler(this.baseLineEqParameterToolStripMenuItem_Click);
            // 
            // gridCOORDParallelToBaselineToolStripMenuItem
            // 
            this.gridCOORDParallelToBaselineToolStripMenuItem.Name = "gridCOORDParallelToBaselineToolStripMenuItem";
            this.gridCOORDParallelToBaselineToolStripMenuItem.Size = new System.Drawing.Size(290, 24);
            this.gridCOORDParallelToBaselineToolStripMenuItem.Text = "Grid COORD Parallel to Baseline";
            this.gridCOORDParallelToBaselineToolStripMenuItem.Click += new System.EventHandler(this.gridCOORDParallelToBaselineToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(287, 6);
            // 
            // CalcverticalGridToolStripMenuItem
            // 
            this.CalcverticalGridToolStripMenuItem.Enabled = false;
            this.CalcverticalGridToolStripMenuItem.Name = "CalcverticalGridToolStripMenuItem";
            this.CalcverticalGridToolStripMenuItem.Size = new System.Drawing.Size(290, 24);
            this.CalcverticalGridToolStripMenuItem.Text = "Grid COORD Parallel to North";
            this.CalcverticalGridToolStripMenuItem.Click += new System.EventHandler(this.CalcverticalGridToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kMLOfGridlinesParallelToNorthToolStripMenuItem1,
            this.kMLCircleCenteredAtARPToolStripMenuItem});
            this.exportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(84, 24);
            this.exportToolStripMenuItem.Text = "Export To";
            // 
            // kMLOfGridlinesParallelToNorthToolStripMenuItem1
            // 
            this.kMLOfGridlinesParallelToNorthToolStripMenuItem1.Name = "kMLOfGridlinesParallelToNorthToolStripMenuItem1";
            this.kMLOfGridlinesParallelToNorthToolStripMenuItem1.Size = new System.Drawing.Size(291, 24);
            this.kMLOfGridlinesParallelToNorthToolStripMenuItem1.Text = "KML - Gridlines Centered at ARP";
            this.kMLOfGridlinesParallelToNorthToolStripMenuItem1.Click += new System.EventHandler(this.kMLOfGridlinesParallelToNorthToolStripMenuItem1_Click);
            // 
            // kMLCircleCenteredAtARPToolStripMenuItem
            // 
            this.kMLCircleCenteredAtARPToolStripMenuItem.Name = "kMLCircleCenteredAtARPToolStripMenuItem";
            this.kMLCircleCenteredAtARPToolStripMenuItem.Size = new System.Drawing.Size(291, 24);
            this.kMLCircleCenteredAtARPToolStripMenuItem.Text = "KML - Circle Centered at ARP";
            this.kMLCircleCenteredAtARPToolStripMenuItem.Click += new System.EventHandler(this.kMLCircleCenteredAtARPToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem,
            this.gridBoundaryToolStripMenuItem});
            this.settingToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.baselineToolStripMenuItem,
            this.northToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // baselineToolStripMenuItem
            // 
            this.baselineToolStripMenuItem.Checked = true;
            this.baselineToolStripMenuItem.CheckOnClick = true;
            this.baselineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.baselineToolStripMenuItem.Name = "baselineToolStripMenuItem";
            this.baselineToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.baselineToolStripMenuItem.Text = "Baseline";
            this.baselineToolStripMenuItem.Click += new System.EventHandler(this.baselineToolStripMenuItem_Click);
            // 
            // northToolStripMenuItem
            // 
            this.northToolStripMenuItem.CheckOnClick = true;
            this.northToolStripMenuItem.Name = "northToolStripMenuItem";
            this.northToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.northToolStripMenuItem.Text = "North";
            this.northToolStripMenuItem.Click += new System.EventHandler(this.northToolStripMenuItem_Click);
            // 
            // autoProcessToolStripMenuItem
            // 
            this.autoProcessToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.baselineModeToolStripMenuItem,
            this.northModeToolStripMenuItem});
            this.autoProcessToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoProcessToolStripMenuItem.Name = "autoProcessToolStripMenuItem";
            this.autoProcessToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.autoProcessToolStripMenuItem.Text = "Auto Process";
            // 
            // baselineModeToolStripMenuItem
            // 
            this.baselineModeToolStripMenuItem.Name = "baselineModeToolStripMenuItem";
            this.baselineModeToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.baselineModeToolStripMenuItem.Text = "Baseline Mode";
            this.baselineModeToolStripMenuItem.Click += new System.EventHandler(this.baselineModeToolStripMenuItem_Click);
            // 
            // northModeToolStripMenuItem
            // 
            this.northModeToolStripMenuItem.Enabled = false;
            this.northModeToolStripMenuItem.Name = "northModeToolStripMenuItem";
            this.northModeToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.northModeToolStripMenuItem.Text = "North Mode";
            this.northModeToolStripMenuItem.Click += new System.EventHandler(this.northModeToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1370, 642);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1362, 611);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TxtInterceptBase);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.TxtslopeBase);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(769, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(295, 100);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Base line to which grid is parallel";
            // 
            // TxtInterceptBase
            // 
            this.TxtInterceptBase.Location = new System.Drawing.Point(162, 63);
            this.TxtInterceptBase.Name = "TxtInterceptBase";
            this.TxtInterceptBase.Size = new System.Drawing.Size(115, 24);
            this.TxtInterceptBase.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "Intercept of baseline";
            // 
            // TxtslopeBase
            // 
            this.TxtslopeBase.Location = new System.Drawing.Point(162, 33);
            this.TxtslopeBase.Name = "TxtslopeBase";
            this.TxtslopeBase.Size = new System.Drawing.Size(115, 24);
            this.TxtslopeBase.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "Slope of baseline";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtNlines);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.TxtGridInterval);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.TxtRadius);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(296, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(459, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grid Extent";
            // 
            // TxtNlines
            // 
            this.TxtNlines.Location = new System.Drawing.Point(292, 57);
            this.TxtNlines.Name = "TxtNlines";
            this.TxtNlines.Size = new System.Drawing.Size(134, 24);
            this.TxtNlines.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "No. of lines on one side";
            // 
            // TxtGridInterval
            // 
            this.TxtGridInterval.Location = new System.Drawing.Point(162, 63);
            this.TxtGridInterval.Name = "TxtGridInterval";
            this.TxtGridInterval.Size = new System.Drawing.Size(115, 24);
            this.TxtGridInterval.TabIndex = 3;
            this.TxtGridInterval.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Interval of grid (m)";
            // 
            // TxtRadius
            // 
            this.TxtRadius.Location = new System.Drawing.Point(162, 33);
            this.TxtRadius.Name = "TxtRadius";
            this.TxtRadius.Size = new System.Drawing.Size(115, 24);
            this.TxtRadius.TabIndex = 1;
            this.TxtRadius.Text = "4000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Radius from ARP (m)";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColSN,
            this.ColPoint,
            this.ColDescription,
            this.ColLatitude,
            this.ColLongitude,
            this.ColEasting_X,
            this.ColNorthing_Y});
            this.dataGridView1.Location = new System.Drawing.Point(8, 129);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1069, 474);
            this.dataGridView1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtAirportCode);
            this.groupBox1.Controls.Add(this.ComboBoxAirportCode);
            this.groupBox1.Controls.Add(this.TxtCM);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RWY";
            // 
            // TxtAirportCode
            // 
            this.TxtAirportCode.Location = new System.Drawing.Point(136, 60);
            this.TxtAirportCode.Name = "TxtAirportCode";
            this.TxtAirportCode.Size = new System.Drawing.Size(115, 24);
            this.TxtAirportCode.TabIndex = 4;
            this.TxtAirportCode.Text = "VNBW";
            // 
            // ComboBoxAirportCode
            // 
            this.ComboBoxAirportCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxAirportCode.FormattingEnabled = true;
            this.ComboBoxAirportCode.Location = new System.Drawing.Point(136, 23);
            this.ComboBoxAirportCode.Name = "ComboBoxAirportCode";
            this.ComboBoxAirportCode.Size = new System.Drawing.Size(115, 26);
            this.ComboBoxAirportCode.TabIndex = 2;
            this.ComboBoxAirportCode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAirportCode_SelectedIndexChanged);
            // 
            // TxtCM
            // 
            this.TxtCM.Location = new System.Drawing.Point(9, 57);
            this.TxtCM.Name = "TxtCM";
            this.TxtCM.Size = new System.Drawing.Size(115, 24);
            this.TxtCM.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Central Meridian";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1362, 611);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Map";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gMapControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1356, 598);
            this.splitContainer1.SplitterDistance = 295;
            this.splitContainer1.TabIndex = 0;
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(0, 0);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 25;
            this.gMapControl1.MinZoom = 5;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(1057, 598);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.Zoom = 0D;
            // 
            // gridBoundaryToolStripMenuItem
            // 
            this.gridBoundaryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.circularToolStripMenuItem,
            this.rectangularToolStripMenuItem});
            this.gridBoundaryToolStripMenuItem.Name = "gridBoundaryToolStripMenuItem";
            this.gridBoundaryToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.gridBoundaryToolStripMenuItem.Text = "Grid Boundary";
            // 
            // circularToolStripMenuItem
            // 
            this.circularToolStripMenuItem.Checked = true;
            this.circularToolStripMenuItem.CheckOnClick = true;
            this.circularToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.circularToolStripMenuItem.Name = "circularToolStripMenuItem";
            this.circularToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.circularToolStripMenuItem.Text = "Circular";
            this.circularToolStripMenuItem.Click += new System.EventHandler(this.circularToolStripMenuItem_Click);
            // 
            // rectangularToolStripMenuItem
            // 
            this.rectangularToolStripMenuItem.CheckOnClick = true;
            this.rectangularToolStripMenuItem.Name = "rectangularToolStripMenuItem";
            this.rectangularToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.rectangularToolStripMenuItem.Text = "Rectangular";
            this.rectangularToolStripMenuItem.Click += new System.EventHandler(this.rectangularToolStripMenuItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Enabled = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(295, 598);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblGBstatus);
            this.panel1.Controls.Add(this.lblModestatus);
            this.panel1.Location = new System.Drawing.Point(0, 671);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1385, 80);
            this.panel1.TabIndex = 1;
            // 
            // lblModestatus
            // 
            this.lblModestatus.AutoSize = true;
            this.lblModestatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModestatus.ForeColor = System.Drawing.Color.Red;
            this.lblModestatus.Location = new System.Drawing.Point(12, 14);
            this.lblModestatus.Name = "lblModestatus";
            this.lblModestatus.Size = new System.Drawing.Size(163, 18);
            this.lblModestatus.TabIndex = 0;
            this.lblModestatus.Text = "Mode Setting:  Baseline";
            // 
            // lblGBstatus
            // 
            this.lblGBstatus.AutoSize = true;
            this.lblGBstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGBstatus.ForeColor = System.Drawing.Color.Blue;
            this.lblGBstatus.Location = new System.Drawing.Point(257, 14);
            this.lblGBstatus.Name = "lblGBstatus";
            this.lblGBstatus.Size = new System.Drawing.Size(215, 18);
            this.lblGBstatus.TabIndex = 1;
            this.lblGBstatus.Text = "Grid Boundary Setting:  Circular";
            // 
            // ColSN
            // 
            this.ColSN.HeaderText = "SN";
            this.ColSN.Name = "ColSN";
            this.ColSN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColPoint
            // 
            this.ColPoint.HeaderText = "Point";
            this.ColPoint.Name = "ColPoint";
            this.ColPoint.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColDescription
            // 
            this.ColDescription.HeaderText = "Description";
            this.ColDescription.Name = "ColDescription";
            this.ColDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColDescription.Width = 200;
            // 
            // ColLatitude
            // 
            this.ColLatitude.HeaderText = "Latitude";
            this.ColLatitude.Name = "ColLatitude";
            this.ColLatitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColLatitude.Width = 150;
            // 
            // ColLongitude
            // 
            this.ColLongitude.HeaderText = "Longitude";
            this.ColLongitude.Name = "ColLongitude";
            this.ColLongitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColLongitude.Width = 150;
            // 
            // ColEasting_X
            // 
            this.ColEasting_X.HeaderText = "Easting_X";
            this.ColEasting_X.Name = "ColEasting_X";
            this.ColEasting_X.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColEasting_X.Width = 150;
            // 
            // ColNorthing_Y
            // 
            this.ColNorthing_Y.HeaderText = "Northing_Y";
            this.ColNorthing_Y.Name = "ColNorthing_Y";
            this.ColNorthing_Y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColNorthing_Y.Width = 150;
            // 
            // FrmMainGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMainGrid";
            this.Text = "CSAY LATLONG UTM GRID";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMainGrid_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtCM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox TxtAirportCode;
        private System.Windows.Forms.ComboBox ComboBoxAirportCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TxtGridInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtRadius;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TxtInterceptBase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtslopeBase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem LoadRWYdataStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem calculateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baseLineEqParameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CalcverticalGridToolStripMenuItem;
        private System.Windows.Forms.TextBox TxtNlines;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem gridCOORDParallelToBaselineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kMLOfGridlinesParallelToNorthToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearAllLayersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kMLCircleCenteredAtARPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem northToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baselineToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem autoProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baselineModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem northModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridBoundaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangularToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGBstatus;
        private System.Windows.Forms.Label lblModestatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLatitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLongitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEasting_X;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNorthing_Y;
    }
}

